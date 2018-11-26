using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportCapsuleOpener : MonoBehaviour {

    [SerializeField]
    private GameObject transportCapsuleOpener;

    [SerializeField]
    private Animator transportCapsuleAnimator;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag) & CTRLHub.inst.InteractionDown)
        {
            Activate();
        }
    }

    private void Activate()
    {
        transportCapsuleOpener.SetActive(false);
        transportCapsuleAnimator.SetBool("open", true);
    }
}
