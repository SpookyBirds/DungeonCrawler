using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicatorTank : NPCAnimationCommunicator
{
    private NPC_AI_Tank ai_tank;
    public new NPC_AI_Tank AI
    {
        get { return ai_tank; }
        set
        {
            ai_tank = value;
            Initialize();
        }
    }

    protected override void Initialize()
    {
        switch (state)
        {
            case States.Idle_baseState:
                fireEnter = AI.Idle_baseState_Enter;
                fireUpdate = AI.Idle_baseState_Update;
                break;

            case States.Aggro_baseState:
                fireEnter = AI.Idle_baseState_Enter;
                break;

            case States.Combat_Idle:
                fireUpdate = AI.CombatIdle_Update;
                break;

            case States.JumpAttack:
                fireEnter = AI.JumpAttack_Enter;
                break;

            case States.Attack:
                fireUpdate = AI.Attack_Update;
                break;

            case States.Run:
                fireEnter = AI.Run_Start;
                fireUpdate = AI.Run_Update;
                fireExit = AI.Run_End;
                break;
        }
    }
}
