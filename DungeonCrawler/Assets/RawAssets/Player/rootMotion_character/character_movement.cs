using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour {

    private Animator anim;
    private Rigidbody rigid;
    public float horizontalInput;
    public float verticalInput;

    public Vector3 ForwardDirection { get { return transform.forward; } }
    public Vector3 LeftDirection { get { return -transform.right; } }
    public Vector3 BackDirection { get { return -transform.forward; } }
    public Vector3 RightDirection { get { return transform.right; } }

    void Start () {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
	}
	
	
	void Update () {

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        anim.SetBool("running", verticalInput != 0 || horizontalInput != 0);

        rigid.AddForce(ForwardDirection * verticalInput);
        rigid.AddForce(LeftDirection * horizontalInput);

        anim.SetFloat("verticalVelocity", verticalInput);
        anim.SetFloat("horizontalVelocity", horizontalInput);

	}
}
