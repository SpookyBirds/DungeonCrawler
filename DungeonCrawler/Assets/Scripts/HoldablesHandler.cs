using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldablesHandler : MonoBehaviour {

    [SerializeField] [Tooltip("Holdble 1 in enum. (0 = none)")]
    private GameObject swordPrefab;

    [SerializeField] [Tooltip("Holdble 2 in enum")]
    private GameObject gunPrefab;

    [SerializeField] [Tooltip("Holdble 3 in enum")]
    private GameObject shieldPrefab;

    [Space]

    [SerializeField] [Tooltip("Position and Rotation of the Left Holdable. Armature Bone under Left Hand")]
    private Transform leftHoldableTransform;

    [SerializeField] [Tooltip("Position and Rotation of the Left Holdable. Armature Bone under Left Hand")]
    private Transform rightHoldableTransform;

    [Space]

    [SerializeField] [Tooltip("Number of the left Holdable in the enum")]
    private int LeftHoldableNumber  = 2;

    [SerializeField] [Tooltip("Number of the right Holdable in the enum")]
    private int RightHoldableNumber = 1;

    [Space]

    [SerializeField] [Tooltip("Drag Player Armature in here")]
    private Animator animator;


    private GameObject LeftHoldable;
    private GameObject RightHoldable;
    
    private GameObject holdablePrefab;

    void Awake ()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        animator.SetInteger("itemHandRight", RightHoldableNumber);
        animator.SetInteger("itemHandLeft", LeftHoldableNumber);

        InstantiateHoldable(LeftHoldable, LeftHoldableNumber, leftHoldableTransform);
        InstantiateHoldable(RightHoldable, RightHoldableNumber, rightHoldableTransform);
    }
	
	void Update ()
    {
        SwapHoldable();
        animator.SetBool("weaponSwap", CTRLHub.inst.SwapHoldable);
    }

    private void SwapHoldable()
    {
        if (CTRLHub.inst.SwapHoldable)
        {
            if (CTRLHub.inst.LeftAttackDown)
            {
                LeftHoldableNumber = animator.GetInteger("itemHandLeft");
                LeftHoldableNumber++;

                if (LeftHoldableNumber == System.Enum.GetValues(typeof(Holdable)).Length)
                    LeftHoldableNumber = 0;

                animator.SetInteger("itemHandLeft", LeftHoldableNumber);

                if (leftHoldableTransform.childCount > 0)
                {
                    Destroy(leftHoldableTransform.GetChild(0).gameObject);
                }
                InstantiateHoldable(LeftHoldable, LeftHoldableNumber, leftHoldableTransform);
                
            }
            else if (CTRLHub.inst.RightAttackDown)
            {
                RightHoldableNumber = animator.GetInteger("itemHandRight");
                RightHoldableNumber++;
                
                if (RightHoldableNumber == System.Enum.GetValues(typeof(Holdable)).Length)
                    RightHoldableNumber = 0;

                animator.SetInteger("itemHandRight", RightHoldableNumber);

                if (rightHoldableTransform.childCount > 0)
                {
                    Destroy(rightHoldableTransform.GetChild(0).gameObject);
                }
                
                InstantiateHoldable(RightHoldable, RightHoldableNumber, rightHoldableTransform);
            }
        }  
    }

    private void InstantiateHoldable(GameObject Holdable, int holdableNumber, Transform holdableTransform)
    {
        switch (holdableNumber)
        {
            case 0: 
                return;
            case 1: holdablePrefab = swordPrefab; Holdable = Instantiate(holdablePrefab, holdableTransform.position, holdableTransform.rotation, holdableTransform);
                return;
            case 2: holdablePrefab = gunPrefab; Holdable = Instantiate(holdablePrefab, holdableTransform.position, holdableTransform.rotation, holdableTransform);
                return;
            case 3: holdablePrefab = shieldPrefab; Holdable = Instantiate(holdablePrefab, holdableTransform.position, holdableTransform.rotation, holdableTransform);
                return;
        }   
    }

    public enum Holdable
    {
        none   = 0,
        sword  = 1,
        gun    = 2,
        shield = 3,
    }
}
