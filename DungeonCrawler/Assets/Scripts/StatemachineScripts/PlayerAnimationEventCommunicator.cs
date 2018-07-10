using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventCommunicator : MonoBehaviour {

    public string leftLongAttackState;
    private int   leftLongAttackID;
    public string leftShortFirstChainAttackState;
    private int   leftShortFirstChainAttackID;
    public string leftShortSecondChainAttackState;
    private int   leftShortSecondChainAttackID;
    public string leftShortThirdChainAttackState;
    private int   leftShortThirdChainAttackID;

    public string rightLongAttackState;
    private int   rightLongAttackID;
    public string rightShortFirstChainAttackState;
    private int   rightShortFirstChainAttackID;
    public string rightShortSecondChainAttackState;
    private int   rightShortSecondChainAttackID;
    public string rightShortThirdChainAttackState;
    private int   rightShortThirdChainAttackID;



    public int layer;

    [SerializeField] [Tooltip("The controller of this animated player. If not supplied the script will search it's transform")]
    private ControllerPlayer controller;
    private Animator animator;

    private ValueWrapper<bool> leftAttackHasStarted  = new ValueWrapper<bool>(false);
    private ValueWrapper<bool> rightAttackHasStarted = new ValueWrapper<bool>(false);

    private void Awake()
    {
        leftLongAttackID             = Animator.StringToHash(leftLongAttackState            );
        leftShortFirstChainAttackID  = Animator.StringToHash(leftShortFirstChainAttackState );
        leftShortSecondChainAttackID = Animator.StringToHash(leftShortSecondChainAttackState);
        leftShortThirdChainAttackID  = Animator.StringToHash(leftShortThirdChainAttackState );

        rightLongAttackID             = Animator.StringToHash(rightLongAttackState            );
        rightShortFirstChainAttackID  = Animator.StringToHash(rightShortFirstChainAttackState );
        rightShortSecondChainAttackID = Animator.StringToHash(rightShortSecondChainAttackState);
        rightShortThirdChainAttackID  = Animator.StringToHash(rightShortThirdChainAttackState );

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
    public void UseAttack(int currentChainLink)
    {
        Debug.Log("useAttack " + currentChainLink + "  "+ rightShortSecondChainAttackID + "  "+ animator.GetCurrentAnimatorStateInfo(layer).fullPathHash + "  " + animator.GetCurrentAnimatorStateInfo(layer).shortNameHash);
        switch (GetCurrentState())                                                              
        {
            case AttackState.Left_long:
                Debug.Log("left long");
                leftAttackHasStarted.Value = true;
                controller.UseLeft(UseType.longAttack, currentChainLink);
                break;

            case AttackState.Left_chain_1:
            case AttackState.Left_chain_2:
            case AttackState.Left_chain_3:
                Debug.Log("left chain " + currentChainLink);
                leftAttackHasStarted.Value = true;
                controller.UseLeft(UseType.shortAttack, currentChainLink);
                break;


            case AttackState.Right_long:
                Debug.Log("right long");
                rightAttackHasStarted.Value = true;
                controller.UseRight(UseType.longAttack, currentChainLink);
                break;

            case AttackState.Right_chain_1:
            case AttackState.Right_chain_2:
            case AttackState.Right_chain_3:
                Debug.Log("right chain " + currentChainLink);
                rightAttackHasStarted.Value = true;
                controller.UseRight(UseType.shortAttack, currentChainLink);
                break;
        }
    }

    private AttackState GetCurrentState()
    {
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == leftLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == leftLongAttackID   )
        {
            return AttackState.Left_long;
        }
        else
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == leftShortFirstChainAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == leftShortFirstChainAttackID   )
        {
            return AttackState.Left_chain_1;
        }
        else
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == leftShortSecondChainAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == leftShortSecondChainAttackID   )
        {
            return AttackState.Left_chain_2;
        }
        else
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == leftShortThirdChainAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == leftShortThirdChainAttackID   )
        {
            return AttackState.Left_chain_3;
        }

        else 
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == rightLongAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == rightLongAttackID   )
        {
            return AttackState.Right_long;
        }
        else
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == rightShortFirstChainAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == rightShortFirstChainAttackID   )
        {
            return AttackState.Right_chain_1;
        }
        else
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == rightShortSecondChainAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == rightShortSecondChainAttackID   )
        {
            return AttackState.Right_chain_2;
        }
        else
        if (animator.GetCurrentAnimatorStateInfo(layer).fullPathHash  == rightShortThirdChainAttackID ||
            animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == rightShortThirdChainAttackID   )
        {
            return AttackState.Right_chain_3;
        }

        return AttackState.None;
    }
    
    private enum AttackState
    {
        None,

        Left_long,
        Left_chain_1,
        Left_chain_2,
        Left_chain_3,

        Right_long,
        Right_chain_1,
        Right_chain_2,
        Right_chain_3,
    }

}
