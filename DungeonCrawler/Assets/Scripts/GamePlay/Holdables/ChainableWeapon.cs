using UnityEngine;

public class ChainableWeapon : Holdable {

    public AnimationClip chain_2_Attack;
    public AnimationClip chain_3_Attack;
    [Space]
    [SerializeField]
    protected Substance attackingSubstance;
    [SerializeField]
    protected float damagePerHit;

    public float AttackRange { get { return influenceCollider.bounds.extents.z; } }

    /// <summary>
    /// Initialize an attack. Returns whether the use is successfull
    /// </summary>
    public override bool UseShort(Controller controller)
    {
        return Attack(controller);
    }

    public override bool UseLong(Controller controller)
    {
        return Attack(controller);  
    }

    private bool Attack(Controller controller)
    {
        return CombatManager.ColliderAttackBox(
            influenceCollider, damagePerHit, attackingSubstance, controller.EnemyTypes);
    }           
}