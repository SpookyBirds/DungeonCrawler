using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveMent : MonoBehaviour {

    public float Speed = 1.0f;
    public Vector3 movement;

    private Rigidbody Rigid;


	// Use this for initialization
	void Start () {
        Rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        movement = new Vector3(Horizontal, 0, Vertical);

        Rigid.AddForce(movement * Speed);



	}
}
