using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private bool verticalInvertCamera = true;
    [Space]
    [SerializeField] private float rotationSpeed = 1f;
    [Space]
    [SerializeField] private float verticalRotationLowerLimit;
    [SerializeField] private float verticalRotationUpperLimit;
    [Space]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomLimit;

    [Space]
    [SerializeField] private float aimingCameraTopOffset = 0.5f;
    [SerializeField] private float aimingCameraZoomOffset = 3f;
    [SerializeField] private Image crossHair;

    private Vector3 forwardPoint;
    private Vector3 initialCameraPosition;

    public Transform PitchRotation { get; private set; }
    public Transform MainCamera { get; private set; } 
    public Transform RotationCenterPoint { get; private set; }

    public bool GamePaused { get; set; }

    private void Awake()
    {
        PitchRotation = transform.GetChild(0);
        MainCamera = PitchRotation.GetChild(0);
        RotationCenterPoint = transform.parent;
        initialCameraPosition = MainCamera.localPosition;

        Cursor.lockState = CursorLockMode.Locked;

        crossHair.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        if (GamePaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else if (!GamePaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
       

        //// Handle cursor behaviour
        //if (Input.GetKeyDown(redisplayCursor))
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}
        //else if (Input.GetKeyUp(redisplayCursor))
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}


        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Handle horizontal camera rotation
            transform.Rotate(transform.up, CalculateRotationStep("Mouse X"), Space.World);

            // Handle vertical camera rotation
            PitchRotation.Rotate(
                PitchRotation.right, 
                CalculateRotationStep("Mouse Y") * (verticalInvertCamera ? -1 : 1), 
                Space.World);

            PitchRotation.rotation = Quaternion.Euler(
                CheckClampBounds(
                    PitchRotation.rotation.eulerAngles.x,
                    verticalRotationLowerLimit,
                    verticalRotationUpperLimit),
                PitchRotation.rotation.eulerAngles.y,
                0);
        }
    }

    /// <summary>
    /// Check if value falls inbetween set bounds
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    private float CheckClampBounds(float value, float lowerBound, float upperBound)
    {

        if (value > 300 || value < lowerBound)      //fixing overflow
            return lowerBound;

        if (value > upperBound)
            return upperBound;

        return value;
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

    //private Vector3 SnapPlayerInCameraDirection()
    //{
    //    SaveDirection();
    //    Vector3 lookDirection = transform.position + GetCameraDirection();
    //    RestoreDirection();

    //    return lookDirection;
    //}

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
