using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPlayer : Entity {

    [SerializeField]
    private Text healthDisplay;

    [SerializeField]
    private Image healthbar;

    [SerializeField]
    private int maxHealth = 100;

    public override float Health
    {
        get { return base.Health; }

        protected set
        {
            base.Health = value;
            UpdateHealthDisplay();
        }
    }

    public void RestoreHealth(int amount)
    {
        Health += amount;
        if (Health > maxHealth)
            Health = maxHealth;
    }

    protected override void Awake()
    {
        base.Awake();
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        healthbar.fillAmount = Health / startingHealth;
        healthDisplay.text = "Health: " + Health;
    }

    protected override void KillEntity()
    {
        animator.SetBool("Death", true);
    }
}
