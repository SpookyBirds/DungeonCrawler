using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftParenting : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Global.PlayerTag))
            other.transform.parent = transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
            other.transform.parent = Global.inst.GameLogic.transform;
    }

}
