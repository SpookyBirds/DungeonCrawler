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

    private Transform pitchRotation;
    private Transform mainCamera;
    private Transform rotationCenterPoint;

    public bool GamePaused { get; set; }

    private void Awake()
    {
        pitchRotation = transform.GetChild(0);
        mainCamera = pitchRotation.GetChild(0);
        rotationCenterPoint = transform.parent;
        initialCameraPosition = mainCamera.localPosition;

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
            pitchRotation.Rotate(
                pitchRotation.right, 
                CalculateRotationStep("Mouse Y") * (verticalInvertCamera ? -1 : 1), 
                Space.World);
            pitchRotation.rotation = Quaternion.Euler(
                CheckClampBounds(
                    pitchRotation.rotation.eulerAngles.x,
                    verticalRotationLowerLimit,
                    verticalRotationUpperLimit),
                pitchRotation.rotation.eulerAngles.y,
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
    /// Get the direction the player should walk to
    /// </summary>
    public Vector3 GetCameraDirection()
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
            mainCamera.localPosition += new Vector3(0, 0, aimingCameraZoomOffset);
            rotationCenterPoint.localPosition += new Vector3(0, aimingCameraTopOffset, 0);
            crossHair.gameObject.SetActive(true);
        }
        else
        {
            rotationCenterPoint.localPosition -= new Vector3(0, aimingCameraTopOffset, 0);
            mainCamera.localPosition -= new Vector3(0, 0, aimingCameraZoomOffset);
            crossHair.gameObject.SetActive(false);
        }
    }
}
