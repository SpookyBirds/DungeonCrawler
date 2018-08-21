using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimBoolSetter : StateMachineBehaviour {

    [SerializeField] private string parameterNameOfBool;
    [SerializeField] private bool   exitDeactivate = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool(parameterNameOfBool, true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(exitDeactivate)
            animator.SetBool(parameterNameOfBool, false);
    }
}
