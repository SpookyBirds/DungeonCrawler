using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPassage : MonoBehaviour {

    [SerializeField]
    private EnemySpawner[] enemySpawnersToClear;

    [SerializeField]
    private float waitToOpenBack;

    private bool frontOpen;

    private bool backOpen;

    private bool allCleared;

	void Update ()
    {
        if(!allCleared)
            CheckEnemySpawners();

        if (allCleared)
            frontOpen = true;

	}

    private void CheckEnemySpawners()
    {
        int clearedSpawners = 0;

        for (int index = enemySpawnersToClear.Length; index > 0; index--)
        {
            if(enemySpawnersToClear[index].Cleared)
                clearedSpawners++;
        }

        if(clearedSpawners == enemySpawnersToClear.Length)
            allCleared = true;
    }
}
