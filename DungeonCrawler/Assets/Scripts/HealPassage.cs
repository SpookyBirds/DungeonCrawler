using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPassage : MonoBehaviour {

    [SerializeField]
    private EnemySpawner[] enemySpawnersToClear;

    [SerializeField]
    private float openSpeed = 1f;

    [SerializeField]
    private float timeToOpenBack = 1f;

    [SerializeField]
    private GameObject frontCollider;

    [SerializeField]
    private GameObject backCollider;

    private float frontOpenBlendShape;

    private float backOpenBlendShape;

    private bool frontOpen;

    private bool backOpen;

    private bool allCleared;

    private bool initiateOpenBack;

    void Awake()
    {
        frontOpenBlendShape = GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0);
        backOpenBlendShape = GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(1);
    }

	void Update ()
    {
        if(!allCleared)
            CheckEnemySpawners();

        if (allCleared)
            frontOpen = true;

        if (initiateOpenBack && timeToOpenBack > 0)
            timeToOpenBack -= openSpeed * Time.deltaTime;

        if (initiateOpenBack && timeToOpenBack < 0)
            backOpen = true;

        if (frontOpen)
            frontCollider.SetActive(false);
        else
            frontCollider.SetActive(true);

        if (backOpen)
            backCollider.SetActive(false);
        else
            backCollider.SetActive(true);

        ManageFrontDoor();
        ManageBackDoor();


	}

    private void CheckEnemySpawners()
    {
        int clearedSpawners = 0;

        for (int index = enemySpawnersToClear.Length; index > 0; index--)
        {
            if(enemySpawnersToClear[index-1].cleared)
                clearedSpawners++;
        }

        if(clearedSpawners == enemySpawnersToClear.Length)
        {
            allCleared = true;
        }
            
    }

    private void ManageFrontDoor()
    {
        if(frontOpen == true && frontOpenBlendShape<100f)
        {
            frontOpenBlendShape += openSpeed * Time.deltaTime;
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, frontOpenBlendShape);
        }
        if(frontOpen == false && frontOpenBlendShape>0f)
        {
            frontOpenBlendShape -= openSpeed * Time.deltaTime;
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, frontOpenBlendShape);
        }
    }
    private void ManageBackDoor()
    {
        if (backOpen == true && backOpenBlendShape < 100f)
        {
            backOpenBlendShape += openSpeed * Time.deltaTime;
            //Debug.Log(backOpenBlendShape);
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, backOpenBlendShape);
        }
        if (backOpen == false && backOpenBlendShape > 0f)
        {
            backOpenBlendShape -= openSpeed * Time.deltaTime;
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, backOpenBlendShape);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
        {
            frontOpen = false;
            initiateOpenBack = true;
        }   
    }
}
