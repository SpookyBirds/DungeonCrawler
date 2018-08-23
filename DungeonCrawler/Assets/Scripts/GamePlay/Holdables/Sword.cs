using UnityEngine;

public class Sword : Holdable
{
    [SerializeField]
    protected float damagePerHit;

    public float AttackRange { get { return influenceCollider.bounds.extents.z; } }

    public bool Attack(Substance substance, int[] enemyTypes)
    {
        return CombatManager.ColliderAttackBox(
            influenceCollider, damagePerHit, substance, enemyTypes);
    }
}