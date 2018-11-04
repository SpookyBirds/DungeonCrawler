using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionConsole : MonoBehaviour {


    [SerializeField]
    private GameObject script;




    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
        {
            if (CTRLHub.inst.InteractionDown)
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        script.GetComponent<FollowCurve>().active = true;
    }
}
