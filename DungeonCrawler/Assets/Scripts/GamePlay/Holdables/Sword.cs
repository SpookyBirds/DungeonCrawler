using System;
using UnityEngine;

public class Sword : Holdable
{
    [SerializeField]
    protected float damagePerHit;

    [SerializeField]
    private int substanceConsumtionAmount = 10;

    public bool IsInfused { get; set; }

    public float AttackRange { get { return influenceCollider.bounds.extents.z; } }

    public bool Attack( PlayerSubstanceManager playerSubstanceManager, Substance substance, int[] enemyTypes)
    {
        return CombatManager.ColliderAttackBox(influenceCollider, damagePerHit, UseSubstance(playerSubstanceManager, substance), enemyTypes);
    }

    private Substance UseSubstance( PlayerSubstanceManager playerSubstanceManager, Substance substance)
    {
        // if the sword is infused and the player has enough substance left of the required type, the attack will use the substance
        if (IsInfused && (playerSubstanceManager.TryUsingSubstance(substance, substanceConsumtionAmount) == false))
            return substance;
        else
            return Substance.none_physical;
    }
}