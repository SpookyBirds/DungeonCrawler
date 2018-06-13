using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public float startingHealth;

    private float health;
    public float Health
    {
        get { return health; }
        protected set { health = value; }
    }

    private void Awake()
    {
        Health = startingHealth;
    }

    private void LateUpdate()
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
    /// Deal damage to this entity
    /// </summary>
    /// <param name="demageDelt">The amount of damage this attack delt this entity. Must be positive</param>
    /// <returns>returns remaining health</returns>
    public float Damage(float damageDelt)
    {
        if (damageDelt < 0)
            return Health;

        return (Health -= damageDelt);
    }
}
