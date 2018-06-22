using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : InheritanceSimplyfier {

    public float startingHealth;

    private float health;
    public virtual float Health
    {
        get { return health; }
        protected set { health = value; }
    }

    protected override void Awake()
    {
        Health = startingHealth;
    }

    protected override void LateUpdate()
    {
        HandleHealthDepletion();
    }

    protected virtual void HandleHealthDepletion()
    {
        if (Health <= 0)
        {
            KillEntity();
        }
    }

    protected virtual void KillEntity()
    {
        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// Deal damage to this entity. Returns remaining health
    /// </summary>
    /// <param name="demageDelt">The amount of damage this attack delt this entity. Must be positive</param>
    public float Damage(float damageDelt)
    {
        if (damageDelt < 0)
            return Health;

        return (Health -= damageDelt);
    }
}


[System.Flags]
public enum Entities
{
    None,
    Player,
    Friendly_NPC,
    Enemy_NPC
}
