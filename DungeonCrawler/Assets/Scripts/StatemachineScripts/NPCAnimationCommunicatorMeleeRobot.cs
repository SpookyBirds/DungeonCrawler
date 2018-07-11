using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationCommunicatorMeleeRobot : NPCAnimationCommunicator {

    private NPC_AI_MeleeRobot ai;
    public new NPC_AI_MeleeRobot AI
    {
        get { return ai; }
        set
        {
            ai = value;
            Initialize();
        }
    }

    protected override void Initialize()
    {
        //switch (state)
        //{
        //    case States.Idle_baseState:
        //        fireUpdate = AI.Idle_baseState_Update;
        //        fireEnter = AI.Idle_baseState_Enter;
        //        break;
        //    case States.Aggro_baseState:
        //        fireEnter = AI.Idle_baseState_Enter;
        //        break;
        //    case States.Combat_Idle:
        //        fireUpdate = AI.CombatIdle_Update;
        //        break;
        //    case States.Attack:
        //        fireUpdate = AI.Attack_Update;
        //        break;
        //    case States.Run:
        //        fireEnter = AI.Run_Start;
        //        fireUpdate = AI.Run_Update;
        //        fireExit = AI.Run_End;
        //        break;
        //}
    }

}
