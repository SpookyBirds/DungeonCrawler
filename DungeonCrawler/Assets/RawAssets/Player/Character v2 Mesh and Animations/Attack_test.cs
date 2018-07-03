using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_test : MonoBehaviour {

    Animator anim;

    

	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        anim.SetBool("attack_chain", Input.GetKeyDown(KeyCode.Mouse0));
	}
}
