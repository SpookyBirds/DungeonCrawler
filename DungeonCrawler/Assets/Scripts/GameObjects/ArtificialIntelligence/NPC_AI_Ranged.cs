using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI_Ranged : NPC_AI {

    [Space]
    [SerializeField] private float overshotAttackRange = 2;
    [SerializeField] private float attackRange = 20;
    protected override float AttackRange { get { return attackRange; } }

    protected override bool CalculateAttackStart()
    {
        return Vector3.Distance(AttackCenter, opponent.transform.position) < (AttackRange - overshotAttackRange);
    }

    public override void Attack()
    {
        // Get all collider in shoot distance
        RaycastHit[] hits = Physics.RaycastAll(
            AttackCenter, opponent.transform.position - AttackCenter, AttackRange);

        Debug.Log(hits.Length + " hits");

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
                Vector3.Distance(hits[index].transform.position, AttackCenter);
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(AttackCenter, opponent.transform.position - AttackCenter);
    }

}
