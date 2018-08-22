using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstancePickUp : MonoBehaviour {

    [SerializeField] [Tooltip("Optic rotation")]
    private int opticRotationAmount;

    [SerializeField] [Tooltip("Amount of Substance this vial will restore")]
    private int restortionValue;

    [SerializeField] [Tooltip("Substance this Vial will restore")]
    private Substance substance;

	void Update () {
        OpticRotation();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
        {
            FillPlayerSubstanceStorage();
            Destroy(transform.gameObject);
        }
    }

    private void FillPlayerSubstanceStorage()
    {
        
    }

    private void OpticRotation()
    {
        transform.Rotate(0, opticRotationAmount * Time.deltaTime, 0);
    }
}
