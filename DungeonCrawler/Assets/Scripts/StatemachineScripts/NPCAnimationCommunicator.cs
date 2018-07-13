using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicator : StateMachineBehaviour {

    protected delegate void Fire();
    protected Fire fireEnter  = PlaceHolder;
    protected Fire fireUpdate = PlaceHolder;
    protected Fire fireExit   = PlaceHolder;

    [SerializeField]
    protected States state;

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

    protected virtual void Initialize()
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

    protected enum States
    {
        None = 0,

        Idle_baseState = 1,

        Aggro_baseState = 2,
        Combat_Idle = 3,
        Spot_Player = 4,
        Aim = 13,
        Shoot = 14,
        Attack = 5,
        Run_Attack = 6,
        AttackChain_1 = 7,
        AttackChain_2 = 8,
        AttackChain_3 = 9,
        Run = 10,
        Jump = 11,
        Landing = 12,
    }
}
