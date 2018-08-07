using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_character : MonoBehaviour {

    Animator anim;
    bool runForward;
    float verticalInput;
    float horizontalInput;

	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	

	void Update () {

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        anim.SetFloat("verticalInput", verticalInput);
        anim.SetFloat("horizontalInput", horizontalInput);

        anim.SetBool("runForward", verticalInput > 0 || horizontalInput != 0 && verticalInput == 0);
        anim.SetBool("runBackward", verticalInput < 0);
        anim.SetBool("Testbool", Input.GetKeyDown(KeyCode.LeftControl));

    }
}
