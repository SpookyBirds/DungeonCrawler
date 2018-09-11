using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemyList;
 
    public bool cleared = false;

    void Update()
    {
        enemyList.RemoveAll(gameObject => gameObject == null);
        if (enemyList.Count == 0)
            cleared = true;
    }
}
