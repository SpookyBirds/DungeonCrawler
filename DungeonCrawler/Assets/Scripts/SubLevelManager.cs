using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLevelManager : MonoBehaviour {

    private GameObject nextCheckpoint;

    private int currentSubLevel = 0;

    public Vector3 currentSpawnPoint;

    private bool currendCheckpointTriggerd;

    [SerializeField]
    private Transform subLevelHolder;

    [SerializeField]
    private GameObject[] subLevelPrefabs;

    [SerializeField] 
    private GameObject[] checkpoints;

    private GameObject[] subLevelInstance;

    [SerializeField]
    private Animator playerAnimator;

    void Awake()
    {
        int instantiateIndex = 0;

        subLevelInstance = new GameObject[subLevelPrefabs.Length];

        for(int numberOfSubLevelsToSpawn = subLevelPrefabs.Length ; numberOfSubLevelsToSpawn > 0; numberOfSubLevelsToSpawn--)
        {
            subLevelInstance[instantiateIndex] = Instantiate(subLevelPrefabs[instantiateIndex],subLevelHolder);
            subLevelPrefabs[instantiateIndex].SetActive(false);
            instantiateIndex++;
        }

        currentSpawnPoint = checkpoints[currentSubLevel].transform.GetChild(0).transform.position;
        nextCheckpoint = checkpoints[currentSubLevel+1];
    }

    void Update()
    {
        if(nextCheckpoint.transform.GetChild(0).gameObject.activeSelf && !currendCheckpointTriggerd)
            activateNextSublevel();
    }

    private void activateNextSublevel()
    {
        currentSubLevel++;
        currentSpawnPoint = checkpoints[currentSubLevel].transform.GetChild(0).transform.position;
        currendCheckpointTriggerd = true;

        if(currentSubLevel < subLevelInstance.Length-1)
        {
            nextCheckpoint = checkpoints[currentSubLevel+1];
            currendCheckpointTriggerd = false;
        }
    }

    public void resetSubLevel()
    {
        Destroy(subLevelInstance[currentSubLevel]);
        subLevelInstance[currentSubLevel] = Instantiate(subLevelPrefabs[currentSubLevel],subLevelHolder);
        subLevelInstance[currentSubLevel].SetActive(true);

        for(int number= Global.inst.Drops.transform.childCount; number > 0; number--)
            Destroy(Global.inst.Drops.transform.GetChild(number-1).gameObject);
    }
}
