using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ControllerNPC))]
public class NPC_AI : InheritanceSimplyfier
{
    [SerializeField]
    protected float damagePerHit;

    [SerializeField]
    private BoxCollider attackCollider;
    [SerializeField] [Space]
    private float destinationOvershootDistance = 2.5f;

    protected virtual float AttackRange { get { return attackCollider.bounds.extents.z; } }
    protected Vector3 AttackCenter { get { return attackCollider.transform.position; } }
    public Controller Controller { get; private set; }
    private FieldOfView FieldOfView { get; set; }
    protected NavMeshAgent NavMeshAgent { get; private set; }

    private float elapsedTimeSinceLastNavUpdate = 0f;
    private float timeIntervallToUpdateNavDestinationInSeconds = 1f;

    protected Entity opponent;

    protected override void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Controller = GetComponent<Controller>();
        FieldOfView = GetComponent<FieldOfView>();

        InitializeCommunicator();
    }

    protected virtual void InitializeCommunicator()
    {
        foreach (NPCAnimationCommunicator communicator in Controller.Animator.GetBehaviours<NPCAnimationCommunicator>())
            communicator.AI = this;
    }

    public void Idle_baseState_Update()
    {
        if (FieldOfView.FindEnemy(Controller.EnemyTypes, out opponent))    //search for enemies
        {
            transform.LookAt(opponent.transform);
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

            if (opponent == null)
            {
                Controller.Animator.SetTrigger("IdleBaseStateSwitch");
                return;
            }

            NavMeshAgent.SetDestination(opponent.transform.position + ((opponent.transform.position - transform.position).normalized * destinationOvershootDistance));
        }

        RunOrAttack();
    }

    public void Run_End()
    {
        NavMeshAgent.isStopped = true;
    }

    public void Attack_Update()
    {
        RunOrAttack();
    }

    protected virtual void RunOrAttack()
    {
        if (opponent == null)
        {
            Controller.Animator.SetTrigger("IdleBaseStateSwitch");
            return;
        }

        bool opponentIsInAttackRange = CalculateAttackStart();

        Controller.Animator.SetBool("Run", !opponentIsInAttackRange);
        Controller.Animator.SetBool("Attack", opponentIsInAttackRange);
    } 

    protected virtual bool CalculateAttackStart()
    {
        return Vector3.Distance(AttackCenter, opponent.transform.position) < AttackRange;
    }

    public virtual void Attack()
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