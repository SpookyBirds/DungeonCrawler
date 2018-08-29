using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceSelector : MonoBehaviour {

    [SerializeField] private float greenY;
    [SerializeField] private float redY;
    [SerializeField] private float silverY;

    public Substance CurrentSelected { get; private set; }

    public GameObject SubstanceSelectorParts { get { return transform.parent.gameObject; } }

    private void Awake()
    {
        CurrentSelected = Substance.red;
        transform.localPosition = new Vector3(transform.localPosition.x, redY, transform.localPosition.z);
    }

    public void ScrollUp()
    {
        switch (CurrentSelected)
        {
            default:
                return;

            case Substance.green:
                CurrentSelected = Substance.silver;
                transform.localPosition = new Vector3(transform.localPosition.x, silverY, transform.localPosition.z);
                break;

            case Substance.red:
                CurrentSelected = Substance.green;
                transform.localPosition = new Vector3(transform.localPosition.x, greenY, transform.localPosition.z);
                break;

            case Substance.silver:
                CurrentSelected = Substance.red;
                transform.localPosition = new Vector3(transform.localPosition.x, redY, transform.localPosition.z);
                break;
        }
    }

    [ExposeMethodInEditor]
    public void SetToGreen()
    {
        CurrentSelected = Substance.green;
        transform.localPosition = new Vector3(transform.localPosition.x, greenY, transform.localPosition.z);
    }

    public void ScrollDown()
    {
        switch (CurrentSelected)
        {
            default:
                return;

            case Substance.green:
                CurrentSelected = Substance.red;
                transform.localPosition = new Vector3(transform.localPosition.x, redY,    transform.localPosition.z);
                break;

            case Substance.red:
                CurrentSelected = Substance.silver;
                transform.localPosition = new Vector3(transform.localPosition.x, silverY, transform.localPosition.z);
                break;

            case Substance.silver:
                CurrentSelected = Substance.green;
                transform.localPosition = new Vector3(transform.localPosition.x, greenY,  transform.localPosition.z);
                break;
        }
    }
}