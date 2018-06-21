using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNPC : Controller {

    private AIStatemachine aI;

    protected override void Start()
    {
        aI = GetComponent<AIStatemachine>();
        EnemyTypes = aI.HostileTypes;
    }
}
