using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCommunicator : MonoBehaviour {

    public string leftAttackState  = "Base Layer.UseLeft_state";
    private int leftAttackID;
    public string rightAttackState = "Base Layer.UseRight_state";
    private int rightAttackID;

    public Controller controller;
    private Animator animator;

    private ValueWrapper<bool> leftAttackHasStarted  = new ValueWrapper<bool>(false);
    private ValueWrapper<bool> rightAttackHasStarted = new ValueWrapper<bool>(false);

    private void Awake()
    {
        leftAttackID  = Animator.StringToHash(leftAttackState);
        rightAttackID = Animator.StringToHash(rightAttackState);

        animator = GetComponent<Animator>();

        if (controller == null)
            controller = transform.parent.GetComponent<Controller>();
    }

    private void Start()
    {
        AnimationQuitTrigger[] quitTrigger = animator.GetBehaviours<AnimationQuitTrigger>();
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
        Debug.Log("trigger");
        Debug.Log("fullPathHash: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        Debug.Log("shortNameHash: " + animator.GetCurrentAnimatorStateInfo(0).shortNameHash);
        Debug.Log("idle id: "+ Animator.StringToHash("Base Layer.Idle"));

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(leftAttackState))
        {
            Debug.Log("useAttack left");
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
