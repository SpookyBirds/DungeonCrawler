using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemyList;
 
    private bool cleared = false;
    public bool Cleared{ get; private set; }

    void Update()
    {
        enemyList.RemoveAll(gameObject => gameObject == null);
        if (enemyList.Count == 0)
            cleared = true;
    }
}
