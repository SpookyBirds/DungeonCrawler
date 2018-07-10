using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Holdable {

    [Space]
    public float absorptionGenerationPerFrame;
    [SerializeField]
    private float maxAbsorptionValue;
    private float absorptonValue;
    public float AbsorptionValue
    {
        get { return absorptonValue; }
        protected set
        {
            absorptonValue = Mathf.Clamp(value, 0, maxAbsorptionValue);
            //absorptionBar.fillAmount = absorptonValue / maxAbsorptionValue;  // TODO: reimplement this, that broke randomly
        }
    }

    private bool isBlocking;
    public bool IsBlocking
    {
        get { return isBlocking; }
        protected set
        {
            isBlocking = value;
            influenceCollider.gameObject.SetActive(value);
        }
    }

    private Image absorptionBar;

    protected override void Awake()
    {
        base.Awake();
        absorptionBar = GameObject.Find("AbsorptionBar").GetComponent<Image>(); //TODO: clean up this shit here
        AbsorptionValue = maxAbsorptionValue;
        HoldableMode = HoldableMode.Hold;
        isBlocking = false;
    }

    protected override void FixedUpdate()
    {
        if (IsBlocking == false)
            AbsorptionValue += absorptionGenerationPerFrame;
    }

    public override bool UseLong(Controller controller)
    {
        Debug.Log("started block");
        IsBlocking = true;
        controller.Entity.AddInterruptAction = Block;
        return true;
    }

    public override void UpdateUse(Controller controller, bool quit)
    {
        if (quit)
        {
            Debug.Log("ended block");
            controller.Entity.RemoveInterruptAction = Block;
            IsBlocking = false;
        }
    }

    /// <summary>
    /// Blocking incoming damage by consuming the AbsorptionValue of the shield.
    /// </summary>
    /// <param name="remainingDamageToDeal">The so far unabsorped damage of the attack. The absopted value will get subtracted</param>
    /// <returns>Whether the block was able to fully block the remaining damage</returns>
    private bool Block(ref float remainingDamageToDeal)
    {
        Debug.Log("block");

        float remainingValue = AbsorptionValue -= remainingDamageToDeal;

        if (remainingValue >= 0)
        {
            remainingDamageToDeal = 0;
            return true;
        }

        remainingDamageToDeal = -remainingValue;
        return false;
    }

    public override bool UseShort(Controller controller)
    {
        return false;
    }
}
