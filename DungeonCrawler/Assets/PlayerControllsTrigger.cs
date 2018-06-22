using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllsTrigger : StateMachineBehaviour {

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (CTRLHub.GM.RightAttackDown && (animator.GetBool("UseLeft") == false))
            animator.SetBool("UseRight", true);
        if (CTRLHub.GM.LeftAttackDown && (animator.GetBool("UseRight") == false))
            animator.SetBool("UseLeft", true);
        animator.SetBool("Run", CTRLHub.GM.Forward);
        animator.SetBool("Jump", CTRLHub.GM.JumpDown);
    }
}
