using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventCommunicator : MonoBehaviour {

    public string leftShortAttackState;
    private int leftShortAttackID;     
    public string rightShortAttackState;
    private int rightShortAttackID;    
    public string leftLongAttackState ;
    private int leftLongAttackID;
    public string rightLongAttackState ;
    private int rightLongAttackID;

    public int layer;

    [SerializeField] [Tooltip("The controller of this animated player. If not supplied the script will search it's transform")]
    private ControllerPlayer controller;
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
            controller = transform.GetComponent<ControllerPlayer>();
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
        Debug.Log("attack, but how?  "+ animator.GetCurrentAnimatorStateInfo(0).fullPathHash + "  leftShortAttackState");
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == leftShortAttackID || 
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == leftShortAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).IsName(leftShortAttackState))
        {
            Debug.Log("left short");
            leftAttackHasStarted.Value = true;
            controller.UseLeft(UseType.shortAttack);
        }
        else if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash == leftLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == leftLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).IsName(leftLongAttackState))
        {
            Debug.Log("left long");
            leftAttackHasStarted.Value = true;
            controller.UseLeft(UseType.longAttack);
        }
        else if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash == rightShortAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == rightShortAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).IsName(rightShortAttackState))
        {
            Debug.Log("right short");
            rightAttackHasStarted.Value = true;
            controller.UseRight(UseType.shortAttack);
        }
        else if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash == rightLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == rightLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).IsName(rightLongAttackState))
        {
            Debug.Log("right long");
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
