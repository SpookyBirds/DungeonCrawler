using UnityEngine;

public class UISubstanceManager : MonoBehaviour {

    [SerializeField] [Tooltip("Amount of active vials, first x vials are taken from the following array")]
    private int amountOfActiveVials = 3;

    [SerializeField] [Tooltip("All possible substance vials, active and unactive")]
    private SubstanceVial[] substanceVials;

    [SerializeField]
    private Substance leftHandSubstance;

    [SerializeField]
    private Substance rightHandSubstance;

    private void Awake()
    {
        for (int index = 0; index < substanceVials.Length; index++)
            substanceVials[ index ].gameObject.SetActive(index < amountOfActiveVials);
    }

    /// <summary>
    /// Returns the total amount of the given substance available in all active vials
    /// </summary>
    public int GetCurrentTotalAmount(Substance substance)
    {
        int currentAmount = 0;

        for (int index = 0; index < amountOfActiveVials; index++)
        {
            if (substanceVials[ index ].CurrentSubstance.Equals(substance))
                currentAmount += substanceVials[ index ].CurrentAmount;
        }

        return currentAmount;
    }

    /// <summary>
    /// Tries to subtract given amount of given substance from it's vials. Returns true if the required amount was removed
    /// </summary>
    public bool TryUsingOrGainingSubstance(Substance substanceToUse, int amount)
    {
        if (amount <= 0)
            return false;

        for (int index = 0; index < amountOfActiveVials; index++)
        {
            if (substanceVials[ index ].CurrentSubstance.Equals( substanceToUse))
            {
                if ((amount = substanceVials[ index ].TryToRemoveAmount(amount)) == 0)
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Tries to add given amount of given substance from it's vials. Returns true if any amount was removed
    /// </summary>
    public bool TryGainingSubstance(Substance substanceToGain, int amount)
    {
        Debug.Log("cTA: " + GetCurrentTotalAmount(substanceToGain));
        if (amount <= 0)
            return false;

        int startingAmount = amount;

        for (int index = 0; index < amountOfActiveVials; index++)
        {
            if (substanceVials[ index ].CurrentSubstance.Equals(substanceToGain))
            {
                if ((amount = substanceVials[ index ].TryToGainAmount(amount)) == 0)
                    return true;
            }
        }

        for (int index = 0; index < amountOfActiveVials; index++)
        {
            if (substanceVials[ index ].CurrentSubstance.Equals(Substance.none_physical))
            {
                substanceVials[ index ].CurrentSubstance = substanceToGain;
                if ((amount = substanceVials[ index ].TryToGainAmount(amount)) == 0)
                    return true;
            }
        }

        Debug.Log("sV" + startingAmount + ",   am " + amount);
        return startingAmount != amount;
    }

    /// <summary>
    /// Activates another vile from substanceVials if possible
    /// </summary>
    public void AddVial()
    {
        if (substanceVials.Length < amountOfActiveVials + 1)
            return;

        substanceVials[ amountOfActiveVials].gameObject.SetActive(true);
        amountOfActiveVials++;
    }
}
