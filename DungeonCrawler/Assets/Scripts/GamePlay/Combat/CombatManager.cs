﻿using UnityEngine;

public class CombatManager : MonoBehaviour {

    public static bool ColliderAttack(BoxCollider attackingCollider, float damagePerHit, Substance attackingSubstance, int[] enemyTypes)
    {
        Collider[] colliderInAttackRange =
            Physics.OverlapBox(attackingCollider.bounds.center, attackingCollider.bounds.extents);

        for (int index = 0; index < colliderInAttackRange.Length; index++)
        {
            if (colliderInAttackRange[ index ].IsAnyTagEqual(enemyTypes))
            {
                colliderInAttackRange[ index ].GetComponent<Entity>().TryToDamage(damagePerHit, attackingSubstance);
                return true;
            }
            else if (colliderInAttackRange[ index ].IsTagNeutral())
            {
                colliderInAttackRange[ index ].GetComponent<Entity>().TryToDamage(damagePerHit, attackingSubstance);
            }
        }

        return false;
    }

    public static bool Shoot(
        Vector3 shotStartingPosition, 
        Vector3 direction, 
        float maxReach, 
        float damage, 
        Substance ammunitionLoad,
        int[] ememyTypes) 
    {
        // Get all collider in shoot distance
        RaycastHit[] hits = Physics.RaycastAll( shotStartingPosition, direction, maxReach);

        // Return if no one was found
        if (hits.Length <= 0)
            return false;

        // Hold the distance of everyone hit in regard to the shooter 
        float[] distancesToHit = new float[ hits.Length ];

        // Evaluate if the hit was an opponent for everyone, save it's distance if it was, save int.MaxValue if not
        for (int index = 0; index < hits.Length; index++)
        {
            if (hits[ index ].collider.IsAnyTagEqual(ememyTypes) ||
                hits[ index ].collider.IsTagNeutral())
            {
                distancesToHit[ index ] = Vector3.Distance(hits[index].transform.position, shotStartingPosition);
                continue;
            }
            distancesToHit[ index ] = int.MaxValue;
        }

        // The variable to save at which index the nearest opponent from the hits array lies
        int indexOfNearestOpponent = 0;

        // Evaluating which is the nearest and saving it's index in the variable a line above
        for (int index = 1; index < hits.Length; index++)
        {
            if (distancesToHit[ index ] < distancesToHit[ indexOfNearestOpponent ])
                indexOfNearestOpponent = index;
        }

        // If the "nearest" still is int.MaxValue, return because no enemy was hit (details above)
        if (distancesToHit[ indexOfNearestOpponent ] == int.MaxValue)
            return false;

        // Get the evaluated entity, then check null, just for the sake of safety, and finally damage it
        Entity entityToDamage;
        if ((entityToDamage = hits[ indexOfNearestOpponent ].collider.GetComponent<Entity>()) != null)
        {
            entityToDamage.TryToDamage(damage, ammunitionLoad);
            return true;
        }

        return false;
    }
}
