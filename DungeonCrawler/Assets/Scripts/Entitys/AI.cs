using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : AIStatemachine {

    private FieldOfView fieldOfView;

    private Transform spottedOpponent;

    private float elapsedTimeSinceLastNavUpdate;
    private float timeIntervallToUpdateNavDestinationInSeconds = 1f;

    protected new void Awake()
    {
        base.Awake();

        fieldOfView = GetComponent<FieldOfView>();
        Debug.Log("AI awakend");
    }

    protected override void DoIdle()
    {
        Transform opponent;
        if (fieldOfView.FindEnemy(out opponent))
        {
            spottedOpponent = opponent;
            ChangeState(AIStates.Aggro);
        }
    }

    protected override void DoAggro()
    {
        elapsedTimeSinceLastNavUpdate += Time.deltaTime;
        if(elapsedTimeSinceLastNavUpdate > timeIntervallToUpdateNavDestinationInSeconds)
        {
            elapsedTimeSinceLastNavUpdate -= timeIntervallToUpdateNavDestinationInSeconds;

            navMeshAgent.SetDestination(spottedOpponent.position);
        }
    }
}
