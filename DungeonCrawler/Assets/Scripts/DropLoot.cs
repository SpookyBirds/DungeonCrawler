using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField] [Tooltip("Chance that one of the random items will be dropt")]
    private float dropChance;

    [SerializeField]
    private GameObject[] drops;

    [SerializeField] [Tooltip("position where the loot will drop")]
    private Transform dropTransform;

    private bool dropped;

    void Update()
    {
        if (animator.GetBool("Death")&& !dropped)
        {
            DroppingLoot();
        }
    }

    private void DroppingLoot()
    {
        float randomDropNumber = Random.Range(0f, 100f);

        if(randomDropNumber <= dropChance)
        {
            int index = Random.Range(0, drops.Length);
            Instantiate(drops[index], dropTransform.position, Quaternion.identity, Global.inst.Drops.transform);
        }
        dropped = true;
    }
}
