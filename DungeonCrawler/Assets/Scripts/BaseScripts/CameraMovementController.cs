using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    private bool  verticalInvertCamera = true;

    [Space]
    [SerializeField]
    private Image crossHair;

    [SerializeField]
    private float aimingCameraTopOffset = 0.5f;

    [SerializeField]
    private float aimingCameraZoomOffset = 3f;

    [Space]
    [SerializeField]
    private float rotationSpeed = 1f;

    [Space]
    [SerializeField] [Tooltip("The minimum zoom possible (x) and the maximum (y)")]
    private Vector2 zoomLimit = new Vector2(1, 10);

    [SerializeField]
    private float zoomSpeed = 0.5f;

    [Space]
    [SerializeField]
    private float collisionZoomSpeed = 0.5f;

    [SerializeField]
    private float cameraCollisionRadius = 1f;

    [SerializeField]
    private LayerMask cameraCollisionLayerMask;

    public Transform PitchRotation { get; private set; }
    public Transform MainCamera { get; private set; } 
    public Transform RotationCenterPoint { get; private set; }

    private bool gamePaused;
    public bool GamePaused
    {
        get { return gamePaused; }
        set
        {
            gamePaused = value;
            HandleCursor(value);
        }
    }

    private float GetCurrentZoom { get { return MainCamera.localPosition.z; } }


    private float playerZoomCache;

    private Vector3 forwardPoint;
    private Vector3 initialCameraPosition;

    private static void HandleCursor(bool value)
    {
        if (value)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Awake()
    {
        PitchRotation = transform.GetChild(0);
        MainCamera = PitchRotation.GetChild(0);
        RotationCenterPoint = transform.parent;
        initialCameraPosition = MainCamera.localPosition;

        Cursor.lockState = CursorLockMode.Locked;

        crossHair.gameObject.SetActive(false);

        playerZoomCache = GetCurrentZoom;
    }

    private void Update()
    {
        if (GamePaused == false)
        {
            HandleCameraRotations();
            HandlePlayerDrivenZooming();
            HandleCameraCollision();
        }
    }

    /// <summary>
    /// Handle camera rotations via user input
    /// </summary>
    private void HandleCameraRotations()
    {
        // Handle horizontal camera rotation
        transform.Rotate(transform.up, CalculateRotationStep("Mouse X"), Space.World);

        // Handle vertical camera rotation
        PitchRotation.Rotate(
            PitchRotation.right,
            CalculateRotationStep("Mouse Y") * (verticalInvertCamera ? -1 : 1),
            Space.World);
    }

    /// <summary>
    /// Handle zooming initialized by the player
    /// </summary>
    private void HandlePlayerDrivenZooming()
    {
        if(CTRLHub.inst.ScrollValue != 0)
        {
            DoZoom(CTRLHub.inst.ScrollValue * zoomSpeed);

            if (GetCurrentZoom == playerZoomCache ||
                GetCurrentZoom < -zoomLimit.x)
            {
                playerZoomCache = GetCurrentZoom;
            }
        }
    }

    /// <summary>
    /// Zoom depending on the collision and vincinity to walls, floors and other colliders spezified in the 'cameraCollisionLayerMask'
    /// </summary>
    private void HandleCameraCollision()
    {
        CameraToPlayerWallCollisionRaycastingCheck();

        if (CameraVincinityCollider().Length != 0)
        {
            DoZoom(collisionZoomSpeed);
        }
        else
        {
            if (GetCurrentZoom != playerZoomCache)
            {
                Collider[] colliderNearFutureCameraPosition = Physics.OverlapSphere(
                    MainCamera.position + (MainCamera.position - RotationCenterPoint.position) * collisionZoomSpeed, 
                    cameraCollisionRadius, 
                    cameraCollisionLayerMask);

                if(colliderNearFutureCameraPosition.Length == 0)
                    DoZoom(-collisionZoomSpeed);

            }
        }
    }

    /// <summary>
    /// Get all colliders spezified in the 'cameraCollisionLayerMask' near the camera
    /// </summary>
    private Collider[] CameraVincinityCollider()
    {
        return Physics.OverlapSphere(MainCamera.position, cameraCollisionRadius, cameraCollisionLayerMask);
    }

    /// <summary>
    /// Check if anything is between the camera and the player (needed if the player moves the camera quickly)
    /// </summary>
    private void CameraToPlayerWallCollisionRaycastingCheck()
    {
        Vector3 fromPlayerToCamera = MainCamera.position - RotationCenterPoint.position;
        RaycastHit hit;
        if (Physics.Raycast(
            RotationCenterPoint.position,
            fromPlayerToCamera,
            out hit,
            fromPlayerToCamera.magnitude,
            cameraCollisionLayerMask,
            QueryTriggerInteraction.Ignore))
        {
            //Zoom by the distance of the collided point of the wall plus the collisionRadius as offset and the camera position
            DoZoom(Vector3.Distance(MainCamera.position, hit.point + (fromPlayerToCamera.normalized * cameraCollisionRadius)));
        }
    }

    /// <summary>
    /// Zoom towards 'RotationCenterPoint' or away from it depending on zoomStrength (negative values to change direction).
    /// Validates the zoom to not exceed the 'zoomLimit'
    /// </summary>
    private void DoZoom(float zoomStrength)
    {
        MainCamera.localPosition += new Vector3(0, 0, zoomStrength);
        if (GetCurrentZoom > -zoomLimit.x)
            MainCamera.localPosition = new Vector3(0, 0, -zoomLimit.x);
        else if (GetCurrentZoom< -zoomLimit.y)
            MainCamera.localPosition = new Vector3(0, 0, -zoomLimit.y);
    }

    /// <summary>
    /// Get the amount the camera is supposed to rotate
    /// </summary>
    /// <param name="axis">"Mouse X" for horizontal mouse, "Mouse Y" for vertical mouse</param>
    private float CalculateRotationStep(string axis)
    {
        return Input.GetAxis(axis) * rotationSpeed;
    }

    /// <summary>
    /// Store the current camera direction to restore it later
    /// </summary>
    public void SaveDirection()
    {
        forwardPoint = transform.position + transform.forward;
    }

    /// <summary>
    /// Restore saved camera position
    /// </summary>
    public void RestoreDirection()
    {
        transform.LookAt(forwardPoint);
    }

    /// <summary>
    /// Get the direction the player should walk to. You need to save before and restore after you used it. 
    /// Use the functions Save() and Restore() for that
    /// </summary>
    public Vector3 GetStraightCameraDirection()
    {
        Quaternion currentRotation = transform.rotation;
        transform.Rotate(-currentRotation.eulerAngles.x, 0, 0);
        Vector3 cameraDirection = transform.forward;
        transform.rotation = currentRotation;

        return cameraDirection;
    }

    public void ToggleCameraAimingPosition(bool doAim)
    {
        if (doAim)
        {
            MainCamera.localPosition += new Vector3(0, 0, aimingCameraZoomOffset);
            RotationCenterPoint.localPosition += new Vector3(0, aimingCameraTopOffset, 0);
            crossHair.gameObject.SetActive(true);
        }
        else
        {
            RotationCenterPoint.localPosition -= new Vector3(0, aimingCameraTopOffset, 0);
            MainCamera.localPosition -= new Vector3(0, 0, aimingCameraZoomOffset);
            crossHair.gameObject.SetActive(false);
        }
    }
}

/*
 * 
    [Space]
    [SerializeField] private float verticalRotationLowerLimit;
    [SerializeField] private float verticalRotationUpperLimit;
 * 
 * 
    /// <summary>
    /// Check if value falls inbetween set bounds
    /// </summary>
    /// <returns></returns>
    private float CheckClampBounds(float value, float lowerBound, float upperBound)
    {
        if (value > 300 || value < lowerBound)      //fixing overflow
            return lowerBound;

        if (value > upperBound)
            return upperBound;

        return value;
    }


            PitchRotation.rotation = Quaternion.Euler(
                CheckClampBounds(
                    PitchRotation.rotation.eulerAngles.x,
                    verticalRotationLowerLimit,
                    verticalRotationUpperLimit),
                PitchRotation.rotation.eulerAngles.y,
                0);
 */

      //  kcechgnitsacyarnoisillocllawreyalpotaremac
