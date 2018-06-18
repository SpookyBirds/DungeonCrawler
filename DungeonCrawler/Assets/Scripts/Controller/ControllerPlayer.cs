using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : Controller {

    public KeyCode attackingKey = KeyCode.Mouse0;

    private AttackerPlayer attacker;

    protected override void Awake()
    {
        attacker = GetComponent<AttackerPlayer>();

        enemyTypes = new int[]{
            (int)Entities.Enemy_NPC
        };

        base.Awake();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(attackingKey))
        {
            attacker.StartAttack();
        }

        base.Update();
    }
}
