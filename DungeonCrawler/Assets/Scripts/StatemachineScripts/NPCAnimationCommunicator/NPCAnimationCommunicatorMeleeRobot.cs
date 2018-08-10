public class NPCAnimationCommunicatorMeleeRobot : NPCAnimationCommunicator {

    private NPC_AI_MeleeRobot ai_melee;
    public new NPC_AI_MeleeRobot AI
    {
        get { return ai_melee; }
        set
        {
            ai_melee = value;
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
                fireUpdate = AI.CombatIdle_Update;
                break;
            case States.Run_Attack:
                fireEnter  = AI.Run_Attack_Enter;
                fireUpdate = AI.Run_Attack_Update;
                fireExit   = AI.Run_Attack_Exit;
                break;
            case States.AttackChain_1:
            case States.AttackChain_2:
            case States.AttackChain_3:
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
}
