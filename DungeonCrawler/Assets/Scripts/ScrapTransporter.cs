using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapTransporter : MonoBehaviour {

    [SerializeField]
    private GameObject inner;

    [SerializeField]
    private GameObject particelSystem;

    [SerializeField]
    private GameObject scrapCollider;

    [SerializeField]
    private GameObject scripti;

    private Animator animator;
	
    void Start()
    {
        animator = GetComponent<Animator>();
        scripti.GetComponent<scripti>().substance = GetComponent<Entity>().InfusedSubstance;

    }

	void Update () {
        if (animator.GetBool("Death"))
            ReleaseScrap();
	}

    private void ReleaseScrap()
    {
        Destroy(inner);
        particelSystem.SetActive(true);
        scrapCollider.SetActive(true);
    }
}
