using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstancePickUp : MonoBehaviour {

    [SerializeField] [Tooltip("Amount of Substance this vial will restore")]
    private int restortionValue;

    [SerializeField] [Tooltip("Substance this Vial will restore")]
    private Substance substance;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
        {
            if (other.GetComponent<UISubstanceManager>().TryGainingSubstance(substance, restortionValue))
                Destroy(gameObject);
        }
    }
}
