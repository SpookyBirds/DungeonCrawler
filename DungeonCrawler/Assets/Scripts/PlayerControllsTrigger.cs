using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllsTrigger : StateMachineBehaviour {

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        Debug.Log("Is key down? " + CTRLHub.inst.RightAttackDown);

        if (CTRLHub.inst.RightAttackDown)
            animator.SetBool("UseRight", true);
        if (CTRLHub.inst.LeftAttackDown)
            animator.SetBool("UseLeft", true);

        animator.SetBool("Run", CTRLHub.inst.Forward);
        animator.SetBool("Jump", CTRLHub.inst.JumpDown);
    }
}
