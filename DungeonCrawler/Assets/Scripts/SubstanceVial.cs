using UnityEngine;
using UnityEngine.UI;

public class SubstanceVial : MonoBehaviour {

    public static GameObject testThingy;

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
            currentAmount = Mathf.Clamp(value, 0, fillCapacity);
            if (currentAmount == 0)
                currentSubstance = Substance.none_physical;
            UpdateFillAmountDisplay();
        }
    }

    [SerializeField] [Tooltip("currentSubstance")]
    private Substance currentSubstance;
    public Substance CurrentSubstance
    {
        get { return currentSubstance; }

        set
        {
            currentSubstance = value;
            UpdateSubstanceDisplay();
        }
    }

    private void Awake()
    {
        UpdateFillAmountDisplay();
        UpdateSubstanceDisplay();
    }

    private void UpdateSubstanceDisplay()
    {
        switch (CurrentSubstance)
        {
            case Substance.none_physical:
                substanceBar.material = Global.inst.SubstanceMaterial_Default;
                break;
            case Substance.green:
                substanceBar.material = Global.inst.SubstanceMaterial_Green;
                break;
            case Substance.red:
                substanceBar.material = Global.inst.SubstanceMaterial_Red;
                break;
            case Substance.silber:
                substanceBar.material = Global.inst.SubstanceMaterial_Silver;
                break;
        }
    }

    private void UpdateFillAmountDisplay()
    {
        substanceBar.fillAmount = currentAmount / (float)fillCapacity;
    }

    public int TryToGainAmount(int amount)
    {
        if (amount <= 0)
            return 0;

        int currentAmountBeforeAdding = CurrentAmount;
        if((CurrentAmount += amount) > fillCapacity)
        {
            CurrentAmount = fillCapacity;
            return amount - (CurrentAmount - currentAmountBeforeAdding);
        }

        int finalFill = CurrentAmount += amount;

        if (finalFill <= fillCapacity)
            return 0;

        return finalFill - fillCapacity;
    }

    /// <summary>
    /// Tries to remove given amount from itself. Returns leftover if amount was more than available
    /// </summary>
    public int TryToRemoveAmount(int amount)
    {
        if (amount <= 0)
            return 0;

        int remainingAmount = CurrentAmount -= amount;

        if (remainingAmount >= 0)
        {
            return 0;
        }

        return -remainingAmount;
    }



}
