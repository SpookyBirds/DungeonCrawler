using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicator : StateMachineBehaviour {

    private delegate void Fire();
    private Fire fire;

    [SerializeField] private States state;
    public NPC_AI AI { get; set; }

    private void Awake()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Combat_Idle:
                fire = AI.CombatIdle_Update;
                break;
            case States.Attack:
                break;
            case States.Run:
                break;
            case States.Jump:
                break;
            case States.Landing:
                break;
        }
    }



    private enum States
    {
        None,

        Idle_baseState,

        Aggro_baseState,
        Combat_Idle,
        Attack,
        Run,
        Jump,
        Landing,
    }
}
