using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour {

    Rigidbody RB;
    Time time;

    public float forwardSpeed = 0.1f;
    public float leftSpeed = 0.1f;
    public float backSpeed = 0.1f;
    public float rightSpeed = 0.1f;

    private float curSpeed = 0.0f;
    private float ForwardcurSpeed = 0.0f;
    public float acceleration = 0.1f;
    public float Deacceleration = 0.1f;
    public float MaxSpeed = 0.5f;
    public float MaxForwardSpeed = 1.0f;
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

    public void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
    }

    private void FixedUpdate()
    {
        curSpeed += acceleration * backSpeed * Time.deltaTime;
        ForwardcurSpeed += acceleration  * forwardSpeed * Time.deltaTime;


        if (ForwardcurSpeed > MaxForwardSpeed)
            ForwardcurSpeed = MaxForwardSpeed;

        if (curSpeed > MaxSpeed)
            curSpeed = MaxSpeed;

        if (ForwardKeyDown)
        {
            SnapPlayerInCameraDirection();
            transform.position += ForwardDirection * ForwardcurSpeed * forwardSpeed;

           
        }
        if (!Input.anyKey)
        {
            ForwardcurSpeed -= Deacceleration * forwardSpeed * Time.deltaTime;
            if (ForwardcurSpeed == 0)
                ForwardcurSpeed = 0;
            
        }


        if (LeftKeyDown)
        {
            SnapPlayerInCameraDirection();
            //transform.position += LeftDirection * leftSpeed;
            transform.position += LeftDirection * curSpeed * leftSpeed;
        

        }
        if (BackKeyDown)
        {
            SnapPlayerInCameraDirection();
            //transform.position += BackDirection * backSpeed;
            transform.position += BackDirection * curSpeed * backSpeed;
         
        }
        if (RightKeyDown)
        {
            SnapPlayerInCameraDirection();
            //transform.position += RightDirection * rightSpeed;
            transform.position += RightDirection * curSpeed * rightSpeed;
           
        }
    }

    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}


