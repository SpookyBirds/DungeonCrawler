﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Holdable {

    [Space]
    public float damagePerHit;

    public float AttackRange { get { return influenceCollider.bounds.extents.z; } }

    protected override void Awake()
    {
        base.Awake();
        HoldableMode = HoldableMode.SingleClick;
    }

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
            if (colliderInAttackRange[index].IsAnyTagEqual(controller.EnemyTypes))
            {
                colliderInAttackRange[index].GetComponent<Entity>().TryToDamage(damagePerHit);
                Debug.Log("hit");
                didHit = true;
            }
        }

        return didHit;
    }
}
