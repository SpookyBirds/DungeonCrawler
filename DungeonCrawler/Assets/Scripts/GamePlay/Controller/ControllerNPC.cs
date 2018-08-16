using UnityEngine;

[RequireComponent(typeof(NPC_AI))]
public class ControllerNPC : Controller {

    private NPC_AI NPC_AI { get; set; }

    protected override void Awake()
    {
        base.Awake();

        NPC_AI = GetComponent<NPC_AI>();
    }

    /// <summary>
    /// Halt player movement and inputs
    /// </summary>
    public override void Freeze(float duration)
    {
        base.Freeze(duration);

        NPC_AI.NavMeshAgent.isStopped = true;
    }

    /// <summary>
    /// Removes freezed and resumes player movement and inputs
    /// </summary>
    public override void UnFreeze()
    {
        base.UnFreeze();

        NPC_AI.NavMeshAgent.isStopped = false;
    }        
}
