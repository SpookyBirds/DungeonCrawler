using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    [SerializeField]
    private GameObject playerGUI;

    [SerializeField]
    private GameObject DeathMenuGUI;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int respawnHealth;

    [SerializeField]
    private GameObject subLevelManager;

	void Update () {
		if(animator.GetBool("Death"))
        {
            playerGUI.SetActive(false);
            DeathMenuGUI.SetActive(true);
            CursorHandler.inst.activateCourser(true);
        }
	}

    public void Respawn()
    {
        CursorHandler.inst.activateCourser(false);
        animator.SetBool("Death", false);
        playerGUI.SetActive(true);
        DeathMenuGUI.SetActive(false);

        GetComponent<EntityPlayer>().RestoreHealth(respawnHealth);

        subLevelManager.GetComponent<SubLevelManager>().resetSubLevel();
        transform.position= subLevelManager.GetComponent<SubLevelManager>().currentSpawnPoint;
    }
}
