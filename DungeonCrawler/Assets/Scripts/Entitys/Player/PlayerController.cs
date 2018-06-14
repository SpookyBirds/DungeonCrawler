using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public KeyCode attackingKey = KeyCode.Mouse0;

    private AttackerPlayer attacker;

    private void Awake()
    {
        attacker = GetComponent<AttackerPlayer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(attackingKey))
        {
            attacker.StartAttack();
        }

    }
}
