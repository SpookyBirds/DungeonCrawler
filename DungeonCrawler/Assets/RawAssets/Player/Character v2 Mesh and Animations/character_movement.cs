using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour {

    private Animator anim;
    private Rigidbody rigid;
    public float horizontalInput;
    public float verticalInput;
    public int speed = 5;
    private CameraMovementController cameraMovementController;

    public Vector3 ForwardDirection { get { return transform.forward; } }
    public Vector3 LeftDirection { get { return -transform.right; } }
    public Vector3 BackDirection { get { return -transform.forward; } }
    public Vector3 RightDirection { get { return transform.right; } }

    void Start () {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
    }
	
	
	void Update () {

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        anim.SetBool("testbool", Input.GetKeyDown(KeyCode.Space));

        anim.SetBool("running", !(verticalInput == 0 && horizontalInput == 0));

        rigid.AddForce(ForwardDirection * speed * verticalInput);
        rigid.AddForce(RightDirection * speed * horizontalInput);

        anim.SetFloat("verticalVelocity", verticalInput);
        anim.SetFloat("horizontalVelocity", horizontalInput);
        if(!(verticalInput == 0 && horizontalInput == 0))
        {
            SnapPlayerInCameraDirection();
        }
	}

    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}
