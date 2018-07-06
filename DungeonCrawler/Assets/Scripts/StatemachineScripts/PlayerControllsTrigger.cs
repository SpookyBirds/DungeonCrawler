using System;
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

        animator.SetBool("Attack", AnyAttackBool(animator));
    }

    private bool AnyAttackBool(Animator animator)
    {
        return
            animator.GetBool("UseLeft_short" ) ||
            animator.GetBool("UseLeft_long"  ) ||
            animator.GetBool("UseRight_short") ||
            animator.GetBool("UseRight_long" );
    }
}
