using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralAttackScript : MonoBehaviour {


    [SerializeField]
    private BoxCollider damageCollider;

    [SerializeField]
    private int damageToDeal;
    
    public Substance substance;

    [EnumFlags] [SerializeField] 
    private Entities hostileEntities;

    private int[] enemyTypes;

    void Start()
    {
        enemyTypes = Global.GetSelectedEntries(hostileEntities);
    }

    void OnTriggerEnter(Collider other)
    {
        CombatManager.ColliderAttackBox(damageCollider, damageToDeal, substance, enemyTypes);
    }
}
