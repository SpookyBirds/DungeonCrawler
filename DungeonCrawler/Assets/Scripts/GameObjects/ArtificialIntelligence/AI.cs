using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
public class NPC_AI : InheritanceSimplyfier
{
    [SerializeField] [Tooltip("Weapon prefab containing the weapon info used by this NPC")]
    private Holdable     weapon;

    private Controller Controller   { get; set; }
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
    }

    public void Idle_baseState_Update()
    {
        if (FieldOfView.FindEnemy(out opponent))    //search for enemies
            Controller.Animator.SetTrigger("SwitchBaseState");
    }

    public void Aggro_baseState_Update()
    {

    }


    public void CombatIdle_Update()
    {
        if (opponent.Health <= 0)   //check if the enemy is already dead
            Controller.Animator.SetTrigger("SwitchBaseState");
    }

    public void Attack_Update()
    {
    }

    public void Attack()
    {
        weapon.Use(Controller);
    }


    public void Run_Start()
    {
        NavMeshAgent.isStopped = false;
    }

    public void Run_Update()
    {
        elapsedTimeSinceLastNavUpdate += Time.deltaTime;
        if (elapsedTimeSinceLastNavUpdate > timeIntervallToUpdateNavDestinationInSeconds)
        {
            elapsedTimeSinceLastNavUpdate -= timeIntervallToUpdateNavDestinationInSeconds;

            NavMeshAgent.SetDestination(opponent.transform.position);
        }
    }

    public void Run_End()
    {
        NavMeshAgent.isStopped = true;
    }
}