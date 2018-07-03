﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventCommunicator : MonoBehaviour {

    public string leftAttackState  = "Base Layer.UseLeft_state";
    private int leftAttackID;
    public string rightAttackState = "Base Layer.UseRight_state";
    private int rightAttackID;

    public ControllerPlayer controller;
    private Animator animator;

    private ValueWrapper<bool> leftAttackHasStarted  = new ValueWrapper<bool>(false);
    private ValueWrapper<bool> rightAttackHasStarted = new ValueWrapper<bool>(false);

    private void Awake()
    {
        leftAttackID  = Animator.StringToHash(leftAttackState);
        rightAttackID = Animator.StringToHash(rightAttackState);

        animator = GetComponent<Animator>();

        if (controller == null)
            controller = transform.parent.GetComponent<ControllerPlayer>();
    }

    private void Start()
    {
        PlayerAnimationQuitTrigger[] quitTrigger = animator.GetBehaviours<PlayerAnimationQuitTrigger>();
        for (int index = 0; index < quitTrigger.Length; index++)
        {
            quitTrigger[index].Controller = controller;
            if (quitTrigger[index].state == State.UseLeft)
                quitTrigger[index].hasStarted = leftAttackHasStarted;
            else if (quitTrigger[index].state == State.UseRight)
                quitTrigger[index].hasStarted = rightAttackHasStarted;
        }
    }

    public void UseAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(leftAttackState))
        {
            leftAttackHasStarted.Value = true;
            controller.UseLeft();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(rightAttackState))
        {
            rightAttackHasStarted.Value = true;
            controller.UseRight();
        }
    }

    public void QuitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == leftAttackID)
        {
            leftAttackHasStarted.Value = false;
            controller.QuitLeft();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == rightAttackID)
        {
            rightAttackHasStarted.Value = false;
            controller.QuitRight();
        }
    }

}
