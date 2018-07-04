using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
public class NPC_AI : InheritanceSimplyfier
{
    [SerializeField]
    private float damagePerHit;

    [SerializeField]
    private BoxCollider attackCollider;
    public float AttackRange { get { return attackCollider.bounds.extents.z; } }

    public  Controller   Controller   { get; private set; }
    private FieldOfView  FieldOfView  { get; set; }
    private NavMeshAgent NavMeshAgent { get; set; }

    private float elapsedTimeSinceLastNavUpdate = 0f;
    private float timeIntervallToUpdateNavDestinationInSeconds = 1f;

    private Entity opponent;

    protected override void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Controller   = GetComponent<Controller>();
        FieldOfView  = GetComponent<FieldOfView>();

        foreach (NPCAnimationCommunicator communicator in Controller.Animator.GetBehaviours<NPCAnimationCommunicator>())
            communicator.AI = this;      
    }

    public void Idle_baseState_Update()
    {
        if (FieldOfView.FindEnemy(out opponent))    //search for enemies
        {
            Debug.Log("Found enemy, going aggro!");
            Controller.Animator.SetTrigger("AggroBaseStateSwitch");
        }
    }

    public void Idle_baseState_Enter()
    {
        Controller.Animator.ResetTrigger("IdleBaseStateSwitch");
    }

    public void Aggro_baseState_Enter()
    {
        Controller.Animator.ResetTrigger("AggroBaseStateSwitch");
    }

    public void CombatIdle_Update()
    {
        /// Check if the enemy is already dead
        if (opponent == null || opponent.Health <= 0)
        {
            Debug.Log("Enemy is dead. Start relaxing again");
            Controller.Animator.SetTrigger("IdleBaseStateSwitch");
            return;
        }

        RunOrAttack();
    }



    public void Run_Start()
    {
        NavMeshAgent.isStopped = false;
        elapsedTimeSinceLastNavUpdate = timeIntervallToUpdateNavDestinationInSeconds;
    }

    public void Run_Update()
    {
        elapsedTimeSinceLastNavUpdate += Time.deltaTime;
        if (elapsedTimeSinceLastNavUpdate >= timeIntervallToUpdateNavDestinationInSeconds)
        {
            elapsedTimeSinceLastNavUpdate -= timeIntervallToUpdateNavDestinationInSeconds;

            if(opponent == null)
            {
                Controller.Animator.SetTrigger("IdleBaseStateSwitch");
                return;
            }

            NavMeshAgent.SetDestination(opponent.transform.position);
        }

        RunOrAttack();
    }

    public void Run_End()
    {
        NavMeshAgent.isStopped = true;
    }

    private void RunOrAttack()
    {
        if (opponent == null)
        {
            Controller.Animator.SetTrigger("IdleBaseStateSwitch");
            return;
        }

        bool opponentIsInAttackRange =
            Vector3.Distance(transform.position, opponent.transform.position) < AttackRange;

        Controller.Animator.SetBool("Run", !opponentIsInAttackRange);
        Controller.Animator.SetBool("Attack", opponentIsInAttackRange);
    }


    public void Attack_Update()
    {
        RunOrAttack();
    }

    public void Attack()
    {
        Collider[] colliderInAttackRange =
            Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.extents);

        for (int index = 0; index < colliderInAttackRange.Length; index++)
        {
            if (colliderInAttackRange[index].IsAnyTagEqual(Controller.EnemyTypes))
            {
                colliderInAttackRange[index].GetComponent<Entity>().TryToDamage(damagePerHit);
            }
        }
    }
}