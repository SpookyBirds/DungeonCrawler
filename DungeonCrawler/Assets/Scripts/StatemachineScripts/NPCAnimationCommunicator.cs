using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicator : StateMachineBehaviour {

    private delegate void Fire();
    private Fire fireEnter;
    private Fire fireUpdate;
    private Fire fireExit;

    [SerializeField]
    private States state;

    private NPC_AI ai;
    public NPC_AI AI
    {
        get { return ai; }
        set
        {
            ai = value;
            Initialize();
        }
    }

    private void Initialize()
    {
        switch (state)
        {
            case States.Combat_Idle:
                fireEnter  = delegate { };
                fireUpdate = AI.CombatIdle_Update;
                fireExit   = delegate { };
                break;
            case States.Attack:
                fireEnter  = delegate { };
                fireUpdate = AI.Attack_Update;
                fireExit   = delegate { };
                break;
            case States.Run:
                fireEnter  = AI.Run_Start;
                fireUpdate = AI.Run_Update;
                fireExit   = AI.Run_End;
                break;

            default:
                fireEnter  = delegate { };
                fireUpdate = delegate { };
                fireExit   = delegate { };
                break;
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireEnter();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireExit();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireUpdate();
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
