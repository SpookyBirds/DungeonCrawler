using System.Collections.Generic;
using UnityEngine;

public class Entity : InheritanceSimplyfier {

    public delegate bool InterruptAction(ref float remainingDamageToDeal);
    private List<InterruptAction> interruptActions;

    public float startingHealth;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    private Substance infusedSubstance;
    public Substance UseInfusedSubstance
    {
        get
        {
            Substance temp = infusedSubstance;
            infusedSubstance = Substance.none_physical;
            return temp;
        }

        set
        {
            if (infusedSubstance != Substance.none_physical)
            {
                Debug.LogWarning(name + "'s substance was set while it was still wet. Reaction was triggered.");
                SubstanceManager.ReactSubstances(UseInfusedSubstance, value, transform);
                return;
            }

            infusedSubstance = value;
        }
    }

    private float health;

    public virtual float Health
    {
        get { return health; }
        protected set { health = value; }
    }

    protected override void Awake()
    {
        health = startingHealth;
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
        animator.SetBool("Death", true);
    }

    /// <summary>
    /// Deal damage to this entity. Returns remaining health
    /// </summary>
    /// <param name="demageDelt">The amount of damage the attack delt this entity. Must be positive</param>
    /// 

    //Sebi du hund, wenn du die funktion ersetzt dann lösch sie oder schreib n comment, aber lass sie nich einfach stehn als würde sie benutzt

    //public float Damage(float damageDelt)
    //{
    //    if (damageDelt < 0)
    //        return Health;

    //    if (damageDelt >= Health)
    //        return Health = 0;

    //    return (Health -= damageDelt);
    //}

    /// <summary>
    /// Deal damage to this entity. Returns whether the attack delt damage
    /// </summary>
    /// <param name="damageToDeal">The amount of damage the attack delt this entity. Must be positive</param>
    public bool TryToDamage(float damageToDeal, Substance attackedSubstance = Substance.none_physical)
    {
        if (infusedSubstance == Substance.none_physical)
            infusedSubstance = attackedSubstance;
        else if(infusedSubstance != attackedSubstance)  // if the substances are the same, nothing has to be done
            SubstanceManager.ReactSubstances(UseInfusedSubstance, attackedSubstance, transform);

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

        if (remainingDamageToDeal >= Health)
        {
            Health = 0;
            remainingDamageToDeal = 0;
        }

        Health -= remainingDamageToDeal;
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
    Neutral,
    Player,
    Friendly_NPC,
    Enemy_NPC
}