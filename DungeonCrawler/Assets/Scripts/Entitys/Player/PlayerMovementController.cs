using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float forwardSpeed = 0.1f;
    public float leftSpeed    = 0.1f;
    public float backSpeed    = 0.1f;
    public float rightSpeed = 0.1f;
    public float jumpforce = 1f;

    public bool Groundcheck;

    private Rigidbody rigid;

    
    public Vector3 ForwardDirection { get { return transform.forward;  } }
    public Vector3 LeftDirection    { get { return - transform.right;  } }
    public Vector3 BackDirection    { get { return -transform.forward; } }
    public Vector3 RightDirection   { get { return transform.right;    } }

    private CameraMovementController cameraMovementController;

    private void Awake()
    {
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(CTRLHub.GM.ForwardKeyCode))
        {
            SnapPlayerInCameraDirection();
            transform.position += ForwardDirection * forwardSpeed;
        }
        if (Input.GetKey(CTRLHub.GM.LeftKeyCode))
        {
            SnapPlayerInCameraDirection();
            transform.position += LeftDirection    * leftSpeed;
        }
        if (Input.GetKey(CTRLHub.GM.BackwardKeyCode))
        {
            SnapPlayerInCameraDirection();
            transform.position += BackDirection    * backSpeed;
        }
        if (Input.GetKey(CTRLHub.GM.RightKeyCode))
        {
            SnapPlayerInCameraDirection();
            transform.position += RightDirection * rightSpeed;
        }
    }

    public void Update()
    {
        if(Input.GetKey(CTRLHub.GM.JumpKeyCode))
        {
            // Let the player jump if the groundcheck is true
            if (Groundcheck == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    rigid.AddForce(0, jumpforce, 0);
            }
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        Groundcheck = true;
    }


    private void OnTriggerExit(Collider other)
    {
        Groundcheck = false;
    }

    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}
