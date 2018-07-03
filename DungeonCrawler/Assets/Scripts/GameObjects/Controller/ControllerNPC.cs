using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPC_AI))]
public class ControllerNPC : Controller {

    private NPC_AI aI;

    protected override void Start()
    {
        aI = GetComponent<NPC_AI>();
    }
}
