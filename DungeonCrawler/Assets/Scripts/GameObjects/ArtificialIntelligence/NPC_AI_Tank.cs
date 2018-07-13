using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPC_AI_Tank : NPC_AI
{
    [SerializeField] [Space] [Tooltip("The minimal distance to jump attack (x) and the maximal (y)")]
    private Vector2 minAndMaxDistanceToJumpAttack;
    [SerializeField]
    private Vector3 jumpForce;

    private Rigidbody rigid;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
    }
    protected override void InitializeCommunicator()
    {
        foreach (NPCAnimationCommunicatorTank communicator in Controller.Animator.GetBehaviours<NPCAnimationCommunicatorTank>())
            communicator.AI = this;
    }

    protected override void RunOrAttack()
    {
        if (opponent == null)
        {
            Controller.Animator.SetTrigger("IdleBaseStateSwitch");
            return;
        }

        float distanceToOpponent = Vector3.Distance(new Vector3(AttackCenter.x, transform.position.y, AttackCenter.z), opponent.transform.position);
        bool opponentIsInAttackRange = distanceToOpponent < AttackRange;

        if (distanceToOpponent >= minAndMaxDistanceToJumpAttack.x &&
            distanceToOpponent <= minAndMaxDistanceToJumpAttack.y   )
        {
            Controller.Animator.SetTrigger("JumpAttack");
        }
        else
        {
            Controller.Animator.SetBool("Run", !opponentIsInAttackRange);
            Controller.Animator.SetBool("Attack", opponentIsInAttackRange);
        }
    }

    public void JumpAttack_Enter()
    {
        Debug.Log(transform.forward.MultipliedBy(jumpForce));
        rigid.AddForce(transform.up * jumpForce.y);
        rigid.AddForce(transform.forward * jumpForce.z);
    }

    private void OnValidate()
    {
        if (minAndMaxDistanceToJumpAttack.y < minAndMaxDistanceToJumpAttack.x)
            minAndMaxDistanceToJumpAttack.y = minAndMaxDistanceToJumpAttack.x + 0.05f;
    }

}
