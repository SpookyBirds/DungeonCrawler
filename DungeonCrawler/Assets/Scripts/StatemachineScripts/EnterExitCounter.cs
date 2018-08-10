using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitCounter : StateMachineBehaviour {

    [SerializeField]
    private string parameterName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger(parameterName, animator.GetInteger(parameterName) + 1);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger(parameterName, animator.GetInteger(parameterName) - 1);
    }
}
