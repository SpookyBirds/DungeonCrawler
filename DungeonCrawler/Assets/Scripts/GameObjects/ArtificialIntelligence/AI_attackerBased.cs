using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class AI_attackerBased : AIStatemachine {

    private FieldOfView fieldOfView;

    private Attacker attacker;
    private Entity opponent;

    private float elapsedTimeSinceLastNavUpdate;
    private float timeIntervallToUpdateNavDestinationInSeconds = 1f;

    protected new void Awake()
    {
        base.Awake();

        fieldOfView = GetComponent<FieldOfView>();
        attacker    = GetComponent<Attacker   >();
    }

    protected override void DoIdle()
    {
        if (fieldOfView.FindEnemy(out opponent))
        {
            ChangeState(AIStates.Aggro);
        }
    }

    protected override void DoAggro()
    {
        ///Handle state
        if (opponent.Health <= 0)
        {
            ChangeState(AIStates.Idle);
            return;
        }


        /// Handle movement
        elapsedTimeSinceLastNavUpdate += Time.deltaTime;
        if(elapsedTimeSinceLastNavUpdate > timeIntervallToUpdateNavDestinationInSeconds)
        {
            elapsedTimeSinceLastNavUpdate -= timeIntervallToUpdateNavDestinationInSeconds;

            navMeshAgent.SetDestination(opponent.transform.position);
        }

        /// Handle attacking
        if (attacker.AlreadyAttacking)
            return;

        // Easy check if attack is allowed
        if(Vector3.Distance(opponent.transform.position, attacker.AttackColliderPosition) < attacker.AttackRange)
        {
            attacker.StartAttack();
        }
        else
        {
            // Advanced check for bigger as well as further away targets
            RaycastHit hit;
            if (opponent.GetComponent<Collider>().Raycast(
                new Ray(transform.position, (opponent.transform.position - transform.position)),
                out hit,
                attacker.AttackRange))
            {
                if (hit.distance <= attacker.AttackRange)
                {
                    attacker.StartAttack();
                }
            }
        }
    }
}
