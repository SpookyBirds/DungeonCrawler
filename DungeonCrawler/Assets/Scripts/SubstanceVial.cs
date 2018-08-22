using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubstanceVial : MonoBehaviour {
   
    [SerializeField] [Tooltip("Bar of Substance")]
    private Image substanceBar;

    [SerializeField] [Tooltip("fillCapacity")]
    private int fillCapacity;

    [SerializeField] [Tooltip("amount of current")]
    private int currentAmount;
    public int CurrentAmount
    {
        get { return currentAmount; }

        private set
        {
            currentAmount = value;
            UpdateFillAmountDisplay();
        }
    }

    [SerializeField] [Tooltip("currentSubstance")]
    private Substance currentSubstance;
    public Substance CurrentSubstance
    {
        get { return currentSubstance; }

        private set
        {
            currentSubstance = value;
            UpdateSubstanceDisplay();
        }
    }

    private void UpdateSubstanceDisplay()
    {

    }

    private void UpdateFillAmountDisplay()
    {
        substanceBar.fillAmount = currentAmount / fillCapacity;
    }

    public int AddAmount(int amount)
    {
        if (amount <= 0)
            return 0;

        int currentAmountBeforeAdding = CurrentAmount;
        if((CurrentAmount += amount) > fillCapacity)
        {
            CurrentAmount = fillCapacity;
            return amount - (CurrentAmount - currentAmountBeforeAdding);
        }

        return 0;
    }

    public bool RemoveAmount(int amount)
    {
        if (amount <= 0)
            return false;

        return false;
    }


}
