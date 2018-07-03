using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNPC : Controller {

    private NPC_AI aI;

    protected override void Start()
    {
        aI = GetComponent<NPC_AI>();
    }
}
