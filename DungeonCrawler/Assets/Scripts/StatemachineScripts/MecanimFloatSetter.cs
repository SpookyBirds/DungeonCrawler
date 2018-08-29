using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimFloatSetter : StateMachineBehaviour
{
    [SerializeField]
    private string nameOfFloat;

    [SerializeField] [Tooltip("Value of Float")]
    private float value;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetFloat(nameOfFloat, value); 
    }
}
