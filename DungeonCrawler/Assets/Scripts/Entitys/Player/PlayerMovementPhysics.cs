using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementPhysics : MonoBehaviour
{

    public float forwardSpeed = 0.1f;
    public float leftSpeed = 0.1f;
    public float backSpeed = 0.1f;
    public float rightSpeed = 0.1f;

    [Space]
    public KeyCode forwardKeyCode = KeyCode.W;
    public KeyCode leftKeyCode = KeyCode.A;
    public KeyCode backKeyCode = KeyCode.S;
    public KeyCode rightKeyCode = KeyCode.D;

    public bool ForwardKeyDown { get { return Input.GetKey(forwardKeyCode); } }
    public bool LeftKeyDown { get { return Input.GetKey(leftKeyCode); } }
    public bool BackKeyDown { get { return Input.GetKey(backKeyCode); } }
    public bool RightKeyDown { get { return Input.GetKey(rightKeyCode); } }

    public Vector3 ForwardDirection { get { return transform.forward; } }
    public Vector3 LeftDirection { get { return -transform.right; } }
    public Vector3 BackDirection { get { return -transform.forward; } }
    public Vector3 RightDirection { get { return transform.right; } }

    private CameraMovementController cameraMovementController;

    private void Awake()
    {
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
    }

    private void Update()
    {
        if (ForwardKeyDown)
        {
            SnapPlayerInCameraDirection();
            transform.position += ForwardDirection * forwardSpeed;
        }
        if (LeftKeyDown)
        {
            SnapPlayerInCameraDirection();
            transform.position += LeftDirection * leftSpeed;
        }
        if (BackKeyDown)
        {
            SnapPlayerInCameraDirection();
            transform.position += BackDirection * backSpeed;
        }
        if (RightKeyDown)
        {
            SnapPlayerInCameraDirection();
            transform.position += RightDirection * rightSpeed;
        }
    }

    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}

