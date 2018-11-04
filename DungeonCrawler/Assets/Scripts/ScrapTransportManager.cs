using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapTransportManager : MonoBehaviour {

    [SerializeField]
    private GameObject prefabScrapTransporter;

    [SerializeField]
    private float timeBetweenSpawns;

    private float spawnTimeCounter;

    [SerializeField]
    private float speedOfScrapTransporters;

    [SerializeField][Tooltip("Chance that ScrapTranporter contains Substance")]
    private int substanceChance;

    [SerializeField]
    private Transform spawnPosition;

    void Update()
    {
        if(spawnTimeCounter >= timeBetweenSpawns)
        {
            SpawnScrapTransporter();
            spawnTimeCounter = 0;
        }
        else
            spawnTimeCounter += Time.deltaTime;
    }


    private void SpawnScrapTransporter()
    {
        GameObject scrapTransporter = Instantiate(prefabScrapTransporter, spawnPosition.position, spawnPosition.rotation, transform);
        scrapTransporter.GetComponent<FollowCurve>().curve = gameObject;
        scrapTransporter.GetComponent<FollowCurve>().speed = speedOfScrapTransporters;

        float randomChanceNumber = Random.Range(0f, 100f);

        if (randomChanceNumber <= substanceChance)
        {
            int randomSubstanceNumber = Random.Range(0, 3);

            switch(randomSubstanceNumber)
            {
                case 0:
                    scrapTransporter.GetComponent<Entity>().InfusedSubstance = Substance.green;
                    break;
                case 1:
                    scrapTransporter.GetComponent<Entity>().InfusedSubstance = Substance.red;
                    break;
                case 2:
                    scrapTransporter.GetComponent<Entity>().InfusedSubstance = Substance.silver;
                    break;
            }
        }
        else
        {
            scrapTransporter.GetComponent<Entity>().InfusedSubstance = Substance.none_physical;
        }
    }
}
