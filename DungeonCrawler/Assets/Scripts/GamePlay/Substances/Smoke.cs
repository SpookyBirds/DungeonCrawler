using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    private List<Controller> blindedOnes;

    private void Awake()
    {
        blindedOnes = new List<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.IsAnyTag(SubstanceManager.inst.Effected))
        {
            Controller blindable = other.GetComponent<Controller>();
            blindable.Blind();
            blindedOnes.Add(blindable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.IsAnyTag(SubstanceManager.inst.Effected))
        {
            Controller blindable = other.GetComponent<Controller>();
            blindable.UnBlind();
            blindedOnes.Remove(blindable);
        }
    }

    public void RemoveAfterTime(float smokeDuration)
    {
        Invoke("Remove", smokeDuration);
    }

    private void Remove()
    {
        GetComponent<Collider>().enabled = false;

        for (int index = 0; index < blindedOnes.Count; index++)
        {
            blindedOnes[ index ].UnBlind();
        }

        Destroy(transform.parent.gameObject, 0.1f);
    }
}
