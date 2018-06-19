using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMoveMent : MonoBehaviour {

    public float Speed = 1.0f;
    public float Jumpforce = 1000.0f;

    private Vector3 movement;
    private Rigidbody Rigid;

    private bool Groundcheck = true;

    public CameraMovementController cameraMovementController;


    
    void Start () {
        Rigid = GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate () {
        SnapPlayerInCameraDirection();

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        movement = new Vector3(Horizontal,0 , Vertical);
        // adds Force relative to the position
        Rigid.AddRelativeForce(movement * Speed);

        // Let the player jump if the groundcheck is true
        if (Input.GetKeyDown(KeyCode.Space) && Groundcheck == true)
        Rigid.AddForce(0, Jumpforce, 0);
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
