using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallConveyor : MonoBehaviour {

    private NPC_AI ai;

    private void Awake()
    {
        ai = transform.parent.GetComponentInChildren<NPC_AI>();
    }

    public void UseAttack()
    {
        ai.Attack();
    }
}
