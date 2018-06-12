using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour {

    public float rotationSpeed = 1f;
    public KeyCode rotateButton = KeyCode.Mouse1;

    private Vector3 forwardPoint;


    private void Update()
    {
        if (Input.GetKey(rotateButton))
        {
            //Handle camera side movement
            transform.Rotate(Vector3.up, CalculateRotationStep("Mouse X"));

            //Handle camera up and down movement
            transform.Rotate(Vector3.right, CalculateRotationStep("Mouse Y"));
        }
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
