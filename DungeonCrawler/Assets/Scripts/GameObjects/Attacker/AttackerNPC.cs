using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerNPC : Attacker {

    private AIStatemachine aI;

    protected override void Start()
    {
        aI = GetComponent<AIStatemachine>();
        hostileTypes = aI.HostileTypes;
    }

}
