using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
public class AI : InheritanceSimplyfier
{
    private NavMeshAgent NavMeshAgent { get; set; }
    private Controller   Controller   { get; set; }
    private FieldOfView  FieldOfView  { get; set; }

    private float elapsedTimeSinceLastNavUpdate = 0f;
    private float timeIntervallToUpdateNavDestinationInSeconds = 1f;


    private Entity opponent;

    protected override void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Controller   = GetComponent<Controller>();
        FieldOfView  = GetComponent<FieldOfView>();
    }

    public void Idle_baseState_Update()
    {
        if (FieldOfView.FindEnemy(out opponent))
            Controller.Animator.SetTrigger("SwitchBaseState");
    }

    public void Aggro_baseState_Update()
    {

    }

    public void CombatIdle_Update()
    {
        ///Handle state
        if (opponent.Health <= 0)
        {
            Controller.Animator.SetTrigger("SwitchBaseState");
            return;
        }


        /// Handle movement
        elapsedTimeSinceLastNavUpdate += Time.deltaTime;
        if (elapsedTimeSinceLastNavUpdate > timeIntervallToUpdateNavDestinationInSeconds)
        {
            elapsedTimeSinceLastNavUpdate -= timeIntervallToUpdateNavDestinationInSeconds;

            navMeshAgent.SetDestination(opponent.transform.position);
        }

        /// Handle attacking
        if (attacker.AlreadyAttacking)
            return;

        // Easy check if attack is allowed
        if (Vector3.Distance(opponent.transform.position, attacker.AttackColliderPosition) < attacker.AttackRange)
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

    public void UseRight_Update()
    {

    }



}