using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

    [SerializeField]
    private int healAmount = 50;

    [SerializeField]
    private GameObject particles;

    private bool consumed;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(Global.PlayerTag) && CTRLHub.inst.InteractionDown && !consumed)
        {
            other.GetComponent<EntityPlayer>().RestoreHealth(healAmount);
            consumed = true;
            particles.SetActive(true);

        }
    }
}
