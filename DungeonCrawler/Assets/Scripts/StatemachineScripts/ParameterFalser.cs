using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterFalser : StateMachineBehaviour {

    [SerializeField] private string[] parameters;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        for (int index = 0; index < parameters.Length; index++)
        {
            animator.SetBool(parameters[index], false);
        }
	}
}
