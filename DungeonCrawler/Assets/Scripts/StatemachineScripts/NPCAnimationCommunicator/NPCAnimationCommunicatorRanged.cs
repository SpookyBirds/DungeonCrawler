using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicatorRanged : NPCAnimationCommunicator
{
    private NPC_AI_Ranged ai_ranged;
    public new NPC_AI_Ranged AI
    {
        get { return ai_ranged; }
        set
        {
            ai_ranged = value;
            Initialize();
        }
    }

    protected override void Initialize()
    {
        switch (state)
        {
            case States.Idle_baseState:
                fireEnter  = AI.Idle_baseState_Enter;
                fireUpdate = AI.Idle_baseState_Update;
                break;

            case States.Aggro_baseState:
                fireEnter = AI.Idle_baseState_Enter;
                break;

            case States.Combat_Idle:
                fireUpdate = AI.CombatIdle_Update;
                break;

            case States.Aim:
                fireEnter  = AI.Aim_Enter;
                fireUpdate = AI.Aim_Update;
                fireExit   = AI.Aim_Exit;
                break;

            case States.Charge:
                fireEnter  = AI.Charge_Enter;
                fireUpdate = AI.Charge_Update;
                break;

            case States.Run:
                fireEnter  = AI.Run_Start;
                fireUpdate = AI.Run_Update;
                fireExit   = AI.Run_End;
                break;
        }
    }
}
