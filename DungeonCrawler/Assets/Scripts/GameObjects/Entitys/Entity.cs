using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : InheritanceSimplyfier {

    public delegate bool InterruptAction(ref float remainingDamageToDeal);
    private List<InterruptAction> interruptActions;

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
        interruptActions = new List<InterruptAction>();
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
    /// <param name="demageDelt">The amount of damage the attack delt this entity. Must be positive</param>
    public float Damage(float damageDelt)
    {
        if (damageDelt < 0)
            return Health;

        return (Health -= damageDelt);
    }

    /// <summary>
    /// Deal damage to this entity. Returns whether the attack delt damage
    /// </summary>
    /// <param name="damageToDeal">The amount of damage the attack delt this entity. Must be positive</param>
    public bool TryToDamage(float damageToDeal)
    {
        if (damageToDeal < 0)
            return false;

        float remainingDamageToDeal = damageToDeal;

        for (int index = 0; index < interruptActions.Count; index++)
        {
            //Debug.Log("start block");
            interruptActions[index](ref remainingDamageToDeal);
            if (remainingDamageToDeal <= 0)
                return false;
        }

        Health -= damageToDeal;
        return true;
    }
    
    public InterruptAction AddInterruptAction
    {
        set { interruptActions.Add(value); }
    }

    public InterruptAction RemoveInterruptAction
    {
        set { interruptActions.Remove(value); }
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