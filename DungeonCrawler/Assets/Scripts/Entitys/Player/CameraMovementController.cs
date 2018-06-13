using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour {


    public float rotationSpeed = 1f;
    public KeyCode rotateButton = KeyCode.Mouse1;
    [Space]
    public float verticalRotationLowerLimit;
    public float verticalRotationUpperLimit;
    [Space]
    public float zoomSpeed;
    public float zoomLimit;

    private Vector3 forwardPoint;
    private Transform pitchRotation;
    private Transform mainCamera;
    private Transform rotationCenterPoint;
    private Vector3 initialCameraPosition;

    private void Awake()
    {
        pitchRotation = transform.GetChild(0);
        mainCamera = pitchRotation.GetChild(0);
        rotationCenterPoint = transform.parent;
        initialCameraPosition = mainCamera.localPosition;
    }

    private void Update()
    {
        if (Input.GetKey(rotateButton))
        {
            transform.Rotate(transform.up, CalculateRotationStep("Mouse X"), Space.World);

            pitchRotation.Rotate(pitchRotation.right, CalculateRotationStep("Mouse Y"), Space.World);

            pitchRotation.rotation = Quaternion.Euler(
                CheckClampBounds(
                    pitchRotation.rotation.eulerAngles.x, 
                    verticalRotationLowerLimit, 
                    verticalRotationUpperLimit),
                pitchRotation.rotation.eulerAngles.y, 
                0);
        }

        /*
        if(Input.mouseScrollDelta.y != 0)
        {
            Vector3 newCamPos = 
                mainCamera.localPosition +  //initial position
                ((mainCamera.forward) *   //direction
                Input.mouseScrollDelta.y *  // scroll speed and direction
                zoomSpeed);     // zoom speed
            if (Vector3.Distance(newCamPos, initialCameraPosition) <= zoomLimit)
                mainCamera.localPosition = newCamPos;
        }
        */
    }

    private float CheckClampBounds(float value, float lowerBound, float upperBound)
    {
        
        if (value > 300 || value < lowerBound)      //fixing overflow
            return lowerBound;

        if (value > upperBound)
            return upperBound;

        return value;
    }

    private float CalculateRotationStep(string axis)
    {
        return Input.GetAxis(axis) * rotationSpeed;
    }

    public void SaveDirection()
    {
        forwardPoint = transform.position + transform.forward;
    }

    public void RestoreDirection()
    {
        transform.LookAt(forwardPoint);
    }

    public Vector3 GetCameraDirection()
    {
        Quaternion currentRotation = transform.rotation;
        transform.Rotate(-currentRotation.eulerAngles.x , 0, 0);
        Vector3 cameraDirection = transform.forward;
        transform.rotation = currentRotation;

        return cameraDirection;
    }
}
