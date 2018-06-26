using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Holdable {

    [Space]
    public float absorptionGenerationPerFrame;
    [SerializeField]
    private float maxAbsorptionValue;
    private float absorptonValue;
    public float AbsorptionValue
    {
        get { return absorptonValue; }
        protected set { absorptonValue = Mathf.Clamp(value, 0, maxAbsorptionValue); }
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

    protected override void Awake()
    {
        base.Awake();
        HoldableMode = HoldableMode.Hold;
        isBlocking = false;
    }

    protected override void FixedUpdate()
    {
        if (IsBlocking == false)
            AbsorptionValue += absorptionGenerationPerFrame;
    }

    public override bool Use(Controller controller)
    {
        IsBlocking = true;
        controller.Entity.AddInterruptAction = Block;
        return true;
    }

    public void UpdateUse(Controller controller, bool quit)
    {
        if(quit)
        {
            controller.Entity.RemoveInterruptAction = Block;
            IsBlocking = false;
        }
    }

    private bool Block(ref float remainingDamageToDeal)
    {
        Debug.Log("block");

        remainingDamageToDeal = AbsorptionValue -= remainingDamageToDeal;
        if (remainingDamageToDeal < 0)
            remainingDamageToDeal = 0;
        return true;

    }
}
