using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterParameterPassing : MonoBehaviour {

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        animator.SetBool("Running",           !(CTRLHub.inst.VerticalAxis == 0 && CTRLHub.inst.HorizontalAxis == 0));
        animator.SetFloat("HorizontalVelocity", CTRLHub.inst.HorizontalAxis);
        animator.SetFloat("VerticalVelocity",   CTRLHub.inst.VerticalAxis);
        animator.SetBool("Jump",                CTRLHub.inst.JumpDown);
        animator.SetBool("Roll",                CTRLHub.inst.Roll);
    }

    private bool AnyAttackBool()
    {
        return
            animator.GetBool("UseLeft_short") ||
            animator.GetBool("UseLeft_long") ||
            animator.GetBool("UseRight_short") ||
            animator.GetBool("UseRight_long");
    }
}
