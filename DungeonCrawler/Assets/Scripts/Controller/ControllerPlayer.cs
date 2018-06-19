using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : Controller {

    [EnumFlags]
    public Entities playerEnemies;

    public KeyCode attackingKey = KeyCode.Mouse0;

    private AttackerPlayer attacker;

    protected override void Awake()
    {
        attacker = GetComponent<AttackerPlayer>();

        enemyTypes = Global.GetSelectedEntries(playerEnemies);

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
