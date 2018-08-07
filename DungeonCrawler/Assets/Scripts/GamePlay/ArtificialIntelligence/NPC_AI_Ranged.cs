using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI_Ranged : NPC_AI {

    [Space]
    [SerializeField] private float overshotAttackRange = 2;
    [SerializeField] private float attackRange = 20;
    protected override float AttackRange { get { return attackRange; } }
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] [Space]
    private Transform aimRotationPoint;

    [Space]
    private bool showDebugRay = true;

    private bool isAttacking = false;
    private Vector3 lockedPosition;
    private Quaternion initialAimRotationPointRotation;

    protected override void Awake()
    {
        base.Awake();
        lockedPosition = transform.position;
        initialAimRotationPointRotation = transform.rotation;
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
        // Get all collider in shoot distance
        RaycastHit[] hits = Physics.RaycastAll(
            aimRotationPoint.position, aimRotationPoint.forward, AttackRange);

        // Return if no one was found
        if (hits.Length <= 0)
            return;

        // Hold the distance of everyone hit in regard to the shooter 
        float[] distancesToHit = new float[hits.Length];

        // Evaluate if the hit was an opponent for everyone, save it's distance if it was, save int.MaxValue if not
        for (int index = 0; index < hits.Length; index++)
        {
            if (hits[index].collider.IsAnyTagEqual(Controller.EnemyTypes) == false)
            {
                distancesToHit[index] = int.MaxValue;
                continue;
            }
            distancesToHit[index] =
                Vector3.Distance(hits[index].transform.position, aimRotationPoint.position);
        }

        // The variable to save at which index the nearest opponent from the hits array lies
        int indexOfNearestOpponent = 0;

        // Evaluating which is the nearest and saving it's index in the variable a line above
        for (int index = 1; index < hits.Length; index++)
        {
            if (distancesToHit[index] < distancesToHit[indexOfNearestOpponent])
                indexOfNearestOpponent = index;
        }

        // If the "nearest" still is int.MaxValue, return because no enemy was hit (details above)
        if (distancesToHit[indexOfNearestOpponent] == int.MaxValue)
            return;

        // Get the evaluated entity, then check null, just for the sake of safety, and finally damage it
        Entity entityToDamage;
        if ((entityToDamage = hits[indexOfNearestOpponent].collider.GetComponent<Entity>()) != null)
        {
            entityToDamage.TryToDamage(damagePerHit);
            return;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugRay)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(aimRotationPoint.position, aimRotationPoint.forward * AttackRange);
    }

    private enum ShootAttackStates
    {
        None,
        Step,
        Aim,
        Charge,
        Shoot,
    }
}
