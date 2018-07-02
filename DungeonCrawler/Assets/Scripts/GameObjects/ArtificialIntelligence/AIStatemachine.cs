using UnityEngine;
using UnityEngine.AI;

public class AIStatemachine : InheritanceSimplyfier {

    protected NavMeshAgent navMeshAgent;

    protected AIStates CurrentState { get; private set; }

    [EnumFlags]
    public Entities hostileEntities;

    protected int[] types;
    public int[] HostileTypes { get { return types; } }

    protected override void Awake()
    {
        types = Global.GetSelectedEntries(hostileEntities);

        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        switch (CurrentState)
        {
            case AIStates.Idle:
                DoIdle(); break;
            case AIStates.Aggro:
                DoAggro(); break;
        }
    }

    /// <summary>
    /// Changes the current state to the spezified one and trigger the corresponding initialize function
    /// </summary>
    /// <param name="newState"></param>
    protected void ChangeState(AIStates newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case AIStates.Idle:
                InitializeIdle(); break;
            case AIStates.Aggro:
                InitializeAggro(); break;
        }
    }

    /// <summary>
    /// Get's called when "currentState" is beeing set to "Idle"
    /// </summary>
    protected virtual void InitializeIdle() {}

    /// <summary>
    /// Get's called when "currentState" is beeing set to "Aggro"
    /// </summary>
    protected virtual void InitializeAggro() { }

    protected virtual void DoIdle() { }

    protected virtual void DoAggro() { }
}

public enum AIStates
{
    Idle,
    Aggro
}
