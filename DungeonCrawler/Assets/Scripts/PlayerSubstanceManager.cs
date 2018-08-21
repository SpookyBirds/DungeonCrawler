using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSubstanceManager : MonoBehaviour {

    [SerializeField] [Tooltip("Amount of Substance")]
    private int substanceAmount = 100;

    [SerializeField] [Tooltip("Max amount of Substance")]
    private int maxSubstanceAmount = 100;

    //[SerializeField] [Tooltip("Substance")]
    //private Substance substance = Substance.none_physical;

    [SerializeField] [Tooltip("Bar to display Substance")]
    private Image substanceBar;

	void Update ()
    {
        SubstanceBarUpdate();
	}

    private void SubstanceBarUpdate()
    {
        substanceBar.fillAmount = substanceAmount / maxSubstanceAmount;
    }
}
