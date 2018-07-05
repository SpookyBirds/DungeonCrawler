using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventCommunicator : MonoBehaviour {

    public string leftShortAttackState = "Base Layer.UseLeft_short_state";
    private int leftShortAttackID;
    public string rightShortAttackState = "Base Layer.UseRight_short_state";
    private int rightShortAttackID;
    public string leftLongAttackState = "Base Layer.UseLeft_long_state";
    private int leftLongAttackID;
    public string rightLongAttackState = "Base Layer.UseRight_long_state";
    private int rightLongAttackID;

    public ControllerPlayer controller;
    private Animator animator;

    private ValueWrapper<bool> leftAttackHasStarted  = new ValueWrapper<bool>(false);
    private ValueWrapper<bool> rightAttackHasStarted = new ValueWrapper<bool>(false);

    private void Awake()
    {
        leftShortAttackID  = Animator.StringToHash(leftShortAttackState);
        rightShortAttackID = Animator.StringToHash(rightShortAttackState);
        leftLongAttackID   = Animator.StringToHash(leftLongAttackState);
        rightLongAttackID  = Animator.StringToHash(rightLongAttackState);

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

    /// <summary>
    /// The method called by the attack animation event
    /// </summary>
    public void UseAttack()
    {
        //Debug.Log("attack, but how?  ");
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == leftShortAttackID || 
            animator.GetCurrentAnimatorStateInfo(0).IsName(leftShortAttackState))
        {
            //Debug.Log("left short");
            leftAttackHasStarted.Value = true;
            controller.UseLeft(UseType.shortAttack);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == leftLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(0).IsName(leftLongAttackState))
        {
            //Debug.Log("left long");
            leftAttackHasStarted.Value = true;
            controller.UseLeft(UseType.longAttack);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == rightShortAttackID ||
            animator.GetCurrentAnimatorStateInfo(0).IsName(rightShortAttackState))
        {
            //Debug.Log("right short");
            rightAttackHasStarted.Value = true;
            controller.UseRight(UseType.shortAttack);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == rightLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(0).IsName(rightLongAttackState))
        {
            //Debug.Log("right long");
            rightAttackHasStarted.Value = true;
            controller.UseRight(UseType.longAttack);
        }
    }
    
    //public void QuitAttack()
    //{
    //    if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == leftAttackID)
    //    {
    //        leftAttackHasStarted.Value = false;
    //        controller.QuitLeft();
    //    }
    //    else if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == rightAttackID)
    //    {
    //        rightAttackHasStarted.Value = false;
    //        controller.QuitRight();
    //    }
    //}

}
