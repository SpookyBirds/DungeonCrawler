using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllsTrigger : StateMachineBehaviour {

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (CTRLHub.inst.LeftFireNormal)
            animator.SetBool("UseLeft_short", true);
        else if (CTRLHub.inst.LeftFireHold)
            animator.SetBool("UseLeft_long", true);

        if (CTRLHub.inst.RightFireNormal)
            animator.SetBool("UseRight_short", true);
        else if (CTRLHub.inst.RightFireHold)
            animator.SetBool("UseRight_long", true);

        animator.SetBool("Run", CTRLHub.inst.Forward);
        animator.SetBool("Jump", CTRLHub.inst.JumpDown);
    }
}
