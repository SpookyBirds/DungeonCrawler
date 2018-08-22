using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ControllerNPC))]
public class NPC_AI : InheritanceSimplyfier
{
    [SerializeField]
    protected Substance infusedSubstance;
    [SerializeField]
    protected float damagePerHit;
    [SerializeField]
    private BoxCollider attackCollider;
    [Space]
    [SerializeField] 
    private float destinationOvershootDistance = 2.5f;

    [Space]
    [SerializeField]
    private float timeIntervallToUpdateNavDestinationInSeconds = 1f;
    [SerializeField]
    private float timeIntervallToCheckFieldOfViewInSeconds = 1f;

    public Controller Controller { get; private set; }
    public FieldOfView FieldOfView { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    protected virtual float AttackRange { get { return attackCollider.bounds.extents.z; } }
    protected Vector3 AttackCenter { get { return attackCollider.transform.position; } }

    private float elapsedTimeSinceLastNavUpdate = 0f;
    private float elapsedTimeSinceLastFieldOfViewCheck = 0f;

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

    public void Idle_baseState_Enter()
    {
        Controller.Animator.ResetTrigger("IdleBaseStateSwitch");
    }

    public void Idle_baseState_Update()
    {
        TryFindingAnOpponent();
    }

    private bool TryFindingAnOpponent()
    {
        if (FieldOfView.FindEnemy(Controller.EnemyTypes, out opponent))    //search for enemies
        {
            transform.LookAt(opponent.transform);
            SwitchToAggroBaseState();
            return true;
        }

        return false;
    }

    public void Aggro_baseState_Enter()
    {
        Controller.Animator.ResetTrigger("AggroBaseStateSwitch");
    }

    public void Aggro_baseState_Update()
    {
        elapsedTimeSinceLastFieldOfViewCheck += Time.deltaTime;
        if (elapsedTimeSinceLastFieldOfViewCheck >= timeIntervallToCheckFieldOfViewInSeconds)
        {
            elapsedTimeSinceLastFieldOfViewCheck -= timeIntervallToCheckFieldOfViewInSeconds;

            if (false == TryFindingAnOpponent())
            {
                SwitchToIdleBaseState();
            }
        }
    }

    public void CombatIdle_Update()
    {
        /// Check if the enemy is already dead
        if (opponent == null || opponent.Health <= 0)
        {
            Debug.Log("Enemy is dead. Start relaxing again");
            SwitchToIdleBaseState();
            return;
        }

        RunOrAttack();
    }

    public void SwitchToIdleBaseState()
    {
        Controller.Animator.SetTrigger("IdleBaseStateSwitch");
    }

    private void SwitchToAggroBaseState()
    {
        Controller.Animator.SetTrigger("AggroBaseStateSwitch");
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

            NavMeshAgent.SetDestination(
                opponent.transform.position + ((opponent.transform.position - transform.position).normalized * destinationOvershootDistance));
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
        CombatManager.ColliderAttackBox(attackCollider, damagePerHit, infusedSubstance, Controller.EnemyTypes);      
    }
}