﻿using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float visionRadius;

    private NPC_AI aI;

    private void Awake()
    {
        aI = GetComponent<NPC_AI>();
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
            if (colliderInVisionRange[index].IsAnyTagEqual(aI.Controller.EnemyTypes))
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
