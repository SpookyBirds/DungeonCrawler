using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstancePickUp : MonoBehaviour {

    [SerializeField] [Tooltip("Amount of Substance this vial will restore")]
    private int restortionValue;

    [SerializeField] [Tooltip("Substance this Vial will restore")]
    private Substance substance;

    [SerializeField] [Tooltip("Optic rotation")]
    private int opticRotationAmount;

    void Update () {
        OpticRotation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
        {
            FillPlayerSubstanceStorage(other);
            Destroy(gameObject);
        }
    }

    private void FillPlayerSubstanceStorage(Collider playerCollider)
    {
        playerCollider.GetComponent<UISubstanceManager>().TryGainingSubstance(substance, restortionValue);
    }

    private void OpticRotation()
    {
        transform.Rotate(0, opticRotationAmount * Time.deltaTime, 0);
    }
}
