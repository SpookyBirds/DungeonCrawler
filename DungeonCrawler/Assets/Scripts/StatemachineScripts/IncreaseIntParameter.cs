using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseIntParameter : StateMachineBehaviour {

    [SerializeField] private string parameterName;
    [SerializeField] private IncreaseCondition increaseCondition;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (increaseCondition == IncreaseCondition.OnEnter)
            animator.SetInteger(parameterName, animator.GetInteger(parameterName) + 1);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (increaseCondition == IncreaseCondition.OnUpdate)
            animator.SetInteger(parameterName, animator.GetInteger(parameterName) + 1);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (increaseCondition == IncreaseCondition.OnExit)
            animator.SetInteger(parameterName, animator.GetInteger(parameterName) + 1);
    }

    private enum IncreaseCondition
    {
        OnEnter,
        OnUpdate,
        OnExit
    }
}
