using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerPlayer : Attacker {

    protected override void Awake()
    {
        hostileTypes = new int[]{
            (int)Entities.Enemy_NPC
        };

        base.Awake();
    }
}
