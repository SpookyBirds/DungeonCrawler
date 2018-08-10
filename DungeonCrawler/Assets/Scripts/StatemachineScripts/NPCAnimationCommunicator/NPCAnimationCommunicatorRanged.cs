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
                fireEnter  = AI.Aggro_baseState_Enter;
                fireUpdate = AI.Aggro_baseState_Update;
                break;

            case States.Combat_Idle:
                fireEnter  = AI.CombatIdle_Enter;
                fireUpdate = AI.CombatIdle_Update;
                break;

            case States.Attack:
                fireUpdate = AI.Attack_Update;
                break;

            case States.Step:
                fireEnter = AI.Step_Enter;
                break;

            case States.Aim:
                fireUpdate = AI.Aim_Update;
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
