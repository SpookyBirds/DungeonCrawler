using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeftUseFire : StateMachineBehaviour {

    public static UnityAction Fired;

    private void Awake()
    {
        Fired += delegate { };
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Fired();
        Debug.Log("fired");
	}

    public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Debug.Log("StateIK");
    }

}
