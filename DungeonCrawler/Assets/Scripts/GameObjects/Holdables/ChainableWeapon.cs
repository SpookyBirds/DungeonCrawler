using UnityEngine;

public class ChainableWeapon : Holdable {

    public AnimationClip chain_2_Attack;
    public AnimationClip chain_3_Attack;
    [Space]
    public float damagePerHit;

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
        bool didHit = false;

        Collider[] colliderInAttackRange =
            Physics.OverlapBox(influenceCollider.bounds.center, influenceCollider.bounds.extents);

        //Debug.Log("Start attack "+ colliderInAttackRange.Length);
        for (int index = 0; index < colliderInAttackRange.Length; index++)
        {
            if (colliderInAttackRange[ index ].IsAnyTagEqual(controller.EnemyTypes))
            {
                DealDamage(colliderInAttackRange[ index ]);
                didHit = true;
            }
            else if(colliderInAttackRange[index].IsTagNeutral())
            {
                DealDamage(colliderInAttackRange[ index ]);
            }
        }

        return didHit;
    }

    private void DealDamage(Collider collider)
    {
        collider.GetComponent<Entity>().TryToDamage(damagePerHit);
    }              
}