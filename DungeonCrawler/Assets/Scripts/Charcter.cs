using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charcter : MonoBehaviour {

    private Animator anim;
    public KeyCode leftUseKey = KeyCode.Mouse0;
    public KeyCode rightUseKey = KeyCode.Mouse1;
    public KeyCode runKey = KeyCode.W;


    void Start () {
        anim = GetComponent<Animator>();
	}
	
	
	void Update () {
        anim.SetBool("LeftUse", Input.GetKey(leftUseKey));
        anim.SetBool("RightUse", Input.GetKeyDown(rightUseKey));
        anim.SetBool("Run", Input.GetKey(runKey));
	}
}
