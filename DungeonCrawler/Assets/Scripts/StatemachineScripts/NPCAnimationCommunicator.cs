using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicator : StateMachineBehaviour {

    private delegate void Fire();
    private Fire fireEnter  = PlaceHolder;
    private Fire fireUpdate = PlaceHolder;
    private Fire fireExit   = PlaceHolder;

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
            case States.Idle_baseState:
                fireUpdate = AI.Idle_baseState_Update;
                fireEnter  = AI.Idle_baseState_Enter;
                break;
            case States.Aggro_baseState:
                fireEnter = AI.Idle_baseState_Enter;
                break;
            case States.Combat_Idle:
                fireUpdate = AI.CombatIdle_Update;
                break;
            case States.Attack:
                fireUpdate = AI.Attack_Update;
                break;
            case States.Run:
                fireEnter  = AI.Run_Start;
                fireUpdate = AI.Run_Update;
                fireExit   = AI.Run_End;
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

    public static void PlaceHolder() { }

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
