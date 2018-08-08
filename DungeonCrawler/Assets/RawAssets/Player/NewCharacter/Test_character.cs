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
        anim.SetBool("dodgeroll", Input.GetKeyDown(KeyCode.LeftControl));
        anim.SetBool("jump", Input.GetKeyDown(KeyCode.Space));
        anim.SetBool("attackSword", Input.GetKeyDown(KeyCode.Mouse1));
        anim.SetBool("gunAim", Input.GetKey(KeyCode.Mouse0));
        anim.SetBool("gunShoot", Input.GetKeyUp(KeyCode.Mouse0));
        anim.SetBool("shieldAttack", Input.GetKeyDown(KeyCode.Mouse0));
        //anim.SetBool("shieldBlock", Input.GetKey(KeyCode.Mouse0));




    }
}
