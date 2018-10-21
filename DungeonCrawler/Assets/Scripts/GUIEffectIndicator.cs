using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIEffectIndicator : MonoBehaviour {


    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite greenSubstanceIcon;

    [SerializeField]
    private Sprite redSubstanceIcon;

    [SerializeField]
    private Sprite silverSubstanceIcon;

    private Substance infusedSubstance;


	void Update ()
    {
        infusedSubstance = GetComponent<EntityHealthbar>().InfusedSubstance;
        SwapIndicator();
	}

    private void SwapIndicator()
    {
        switch(infusedSubstance)
        {
            case Substance.none_physical:
                image.enabled = false;
                break;

            case Substance.green:
                image.enabled = true;
                image.sprite = greenSubstanceIcon;
                break;
            case Substance.red:
                image.enabled = true;
                image.sprite = redSubstanceIcon;
                break;
            case Substance.silver:
                image.enabled = true;
                image.sprite = silverSubstanceIcon;
                break;
        }
    }

}
