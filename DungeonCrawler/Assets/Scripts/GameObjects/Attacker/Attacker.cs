using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Do not attach this script. Use "AttackerPlayer" or "AttackerNPC" instead
/// </summary>
public abstract class Attacker : InheritanceSimplyfier {

    public float damagePerHit;
    public float castingTimeInSeconds;

    [SerializeField]
    private Collider attackCollider;
    public Vector3 AttackColliderPosition { get { return attackCollider.transform.position; } }
    public float AttackRange { get { return attackCollider.bounds.extents.z; } }

    private bool alreadyAttacking = false;
    public bool AlreadyAttacking { get { return alreadyAttacking; } }

    protected int[] hostileTypes;

    /// <summary>
    /// Initialize an attack. Returns whether the attack was successfully started
    /// </summary>
    public virtual bool StartAttack()
    {
        if (alreadyAttacking == false)
        {
            StartCoroutine(Attack());
            return true;
        }

        return false;
    }

    private IEnumerator Attack()
    {
        alreadyAttacking = true;

        yield return new WaitForSeconds(castingTimeInSeconds);

        Collider[] colliderInAttackRange = 
            Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.extents);

        for (int index = 0; index < colliderInAttackRange.Length; index++)
        {
            if (colliderInAttackRange[index].IsAnyTagEqual(hostileTypes))
            {
                float remainingHealth = colliderInAttackRange[index].GetComponent<Entity>().Damage(damagePerHit);
            }
        }

        alreadyAttacking = false;
    }
}
