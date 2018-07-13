using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI_MeleeRobot : NPC_AI {

    [SerializeField] private float destinationChargeOvershootDistance = 4f;
    [SerializeField] private float proximityChargeHitDistance = 4f;
    [SerializeField] private float stunTimeAfterChargeAttack = 4;
    [SerializeField] private float minDistanceToRunAttack = 10;
    [SerializeField] [Tooltip("How much faster is the Robot supposed to be whilst the run attack")]
    private float speedIncreaseToRunAttack = 5;

    private Vector3 chargePosition;
    private float elapsedTimeSinceStunEnter = 0;

    protected override void InitializeCommunicator()
    {
        foreach (NPCAnimationCommunicatorMeleeRobot communicator in 
            Controller.Animator.GetBehaviours<NPCAnimationCommunicatorMeleeRobot>())
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


        if (distanceToOpponent >= minDistanceToRunAttack)
        {
            Controller.Animator.SetInteger("RunAttackState", (int)RunAttackStates.Charge);
        }
        else
        {
            Controller.Animator.SetBool("Run", !opponentIsInAttackRange);
            Controller.Animator.SetBool("Attack", opponentIsInAttackRange);
        }
    }

    public void Run_Attack_Enter()
    {
        if (opponent == null || opponent.Health <= 0)
        {
            Debug.Log("Enemy is dead. Start relaxing again");
            Controller.Animator.SetTrigger("IdleBaseStateSwitch");
            return;
        }

        elapsedTimeSinceStunEnter = 0;

        chargePosition = opponent.transform.position + ((opponent.transform.position - transform.position).normalized * destinationChargeOvershootDistance);

        NavMeshAgent.SetDestination(chargePosition);
        NavMeshAgent.isStopped = false;
        Debug.Log("ALLOW MOVEMENT");
        NavMeshAgent.speed += speedIncreaseToRunAttack;
        NavMeshAgent.acceleration += 5;
    }

    public void Run_Attack_Update()
    {
        NavMeshAgent.isStopped = false;

        if (Controller.Animator.GetInteger("RunAttackState") == (int)RunAttackStates.Stun)
        {
            NavMeshAgent.isStopped = true;

            if (elapsedTimeSinceStunEnter < stunTimeAfterChargeAttack)
            {
                elapsedTimeSinceStunEnter += Time.deltaTime;
            }
            else
                Controller.Animator.SetInteger("RunAttackState", (int)RunAttackStates.None);
        }
        else if (Vector3.Distance(AttackCenter, chargePosition) <= proximityChargeHitDistance  ||
                 Vector3.Distance(AttackCenter, opponent.transform.position) <= proximityChargeHitDistance)
        {
            Controller.Animator.SetInteger("RunAttackState", (int)RunAttackStates.Attack);
            NavMeshAgent.isStopped = true;
        }
    }
    
    public void Run_Attack_Exit()
    {
        Debug.Log("run attack exit");

        NavMeshAgent.speed -= speedIncreaseToRunAttack;
        NavMeshAgent.acceleration -= 5;
        elapsedTimeSinceStunEnter = 0;
    }

    private enum RunAttackStates
    {
        None,
        Charge,
        Attack,
        Stun
    }
}
