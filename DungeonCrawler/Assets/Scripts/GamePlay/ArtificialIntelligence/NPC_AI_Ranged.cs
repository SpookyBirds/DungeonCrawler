using UnityEngine;

public class NPC_AI_Ranged : NPC_AI {

    [Space]
    [SerializeField] private float overshotAttackRange = 2;
    [SerializeField] private float attackRange = 20;
    protected override float AttackRange { get { return attackRange; } }
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] [Space]
    private Transform aimRotationPoint;

    private bool showDebugRay = true;

    private bool isAttacking = false;
    private Vector3 lockedPosition;
    //private Quaternion initialAimRotationPointRotation;

    protected override void Awake()
    {
        base.Awake();
        lockedPosition = transform.position;
        //initialAimRotationPointRotation = transform.rotation;
    }

    protected override void InitializeCommunicator()
    {
        foreach (NPCAnimationCommunicatorRanged communicator in Controller.Animator.GetBehaviours<NPCAnimationCommunicatorRanged>())
            communicator.AI = this;
    }

    protected override bool CalculateAttackStart()
    {
        return Vector3.Distance(AttackCenter, opponent.transform.position) < (AttackRange - overshotAttackRange);
    }

    public void Charge_Enter()
    {
        // TODO: implement particles
    }

    public void Charge_Update()
    {
    }

    public void Aim_Update()
    {
        TurnInOpponentDirection();
        TurnInOpponentDirection();
    }

    private void TurnInOpponentDirection()
    {
        Quaternion goalRotation =
            Quaternion.LookRotation(opponent.transform.position - aimRotationPoint.position, Vector3.up);

        aimRotationPoint.rotation =
            Quaternion.Lerp(aimRotationPoint.rotation.OnlyY(), goalRotation.OnlyY(), rotationSpeed * Time.deltaTime);
    }

    public void Step_Enter()
    {
        isAttacking = true;
        lockedPosition = transform.position;
    }

    public void CombatIdle_Enter()
    {
        isAttacking = false;
    }

    protected override void Update()
    {
        if (isAttacking)
        {
            transform.position = lockedPosition;
        }
        else
        {
            Vector3 currRot = aimRotationPoint.rotation.eulerAngles;

            aimRotationPoint.rotation =
                Quaternion.Lerp(aimRotationPoint.rotation.OnlyY(), transform.rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        CombatManager.Shoot(
            aimRotationPoint.position,
            aimRotationPoint.forward,
            AttackRange,
            damagePerHit,
            infusedSubstance,
            Controller.EnemyTypes); 
    }

    private void OnDrawGizmos()
    {
        if (showDebugRay)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(aimRotationPoint.position, aimRotationPoint.forward * AttackRange);
        }
    }
}
