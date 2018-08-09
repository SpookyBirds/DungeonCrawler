using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPlayer : Entity {

    public Text healthDisplay;
    public Image healthbar;

    public override float Health
    {
        get { return base.Health; }

        protected set
        {
            base.Health = value;
            UpdateHealthDisplay();
        }
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

}
