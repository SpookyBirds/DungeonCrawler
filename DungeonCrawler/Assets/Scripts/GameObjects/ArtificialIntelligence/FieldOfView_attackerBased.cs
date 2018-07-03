using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Obsolete("Attacker based stuff is outdated. Use the other field of view instead")]
public class FieldOfView_attackerBased : MonoBehaviour {

    public float visionRadius;

    private AI_attackerBased aI;

    private void Awake()
    {
        aI = GetComponent<AI_attackerBased>();
    }

    /// <summary>
    /// Checks for enemies in a sphere around it's transform with the radius "visionRadius"
    /// </summary>
    /// <param name="opponent">The transform of the opponent that was first found</param>
    /// <returns>Whether or not an enemy was found in the vision radius</returns>
    public bool FindEnemy(out Entity opponent)
    {
        Collider[] colliderInVisionRange = 
            Physics.OverlapSphere(transform.position, visionRadius);

        for (int index = 0; index < colliderInVisionRange.Length; index++)
        {
            if (colliderInVisionRange[index].IsAnyTagEqual(aI.HostileTypes))
            {
                opponent = colliderInVisionRange[index].GetComponent<Entity>();
                return true;
            }
        }

        opponent = null;
        return false;
    }

    /// <summary>
    /// Checks for enemies in a sphere around it's transform with the radius "visionRadius"
    /// </summary>
    /// <returns>Whether or not an enemy was found in the vision radius</returns>
    public bool FindEnemy()
    {
        Entity transform;
        return FindEnemy(out transform);
    }
}
