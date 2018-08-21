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
    private HoldableType LeftHoldableType  = HoldableType.gun;

    [SerializeField] [Tooltip("Number of the right Holdable in the enum")]
    private HoldableType RightHoldableType = HoldableType.sword;

    [Space]

    [SerializeField] [Tooltip("Drag Player Armature in here")]
    private Animator animator;
    [SerializeField]
    private Transform toolSnapingPoint;

    public Holdable LeftEquiped  { get; private set; }
    public Holdable RightEquiped { get; private set; }
    
    void Awake ()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        animator.SetInteger("itemHandRight", (int)RightHoldableType);
        animator.SetInteger("itemHandLeft",  (int)LeftHoldableType);
        InstantiateLeftHoldable();
        InstantialeRightHoldable();
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
                LeftHoldableType = (HoldableType)animator.GetInteger("itemHandLeft");
                LeftHoldableType++;

                if ((int)LeftHoldableType == System.Enum.GetValues(typeof(HoldableType)).Length)
                    LeftHoldableType = 0;

                animator.SetInteger("itemHandLeft", (int)LeftHoldableType);

                Destroy(LeftEquiped.model.gameObject);
                Destroy(LeftEquiped.gameObject);

                InstantiateLeftHoldable();

            }
            else if (CTRLHub.inst.RightAttackDown)
            {
                RightHoldableType = (HoldableType)animator.GetInteger("itemHandRight");
                RightHoldableType++;
                
                if ((int)RightHoldableType == System.Enum.GetValues(typeof(HoldableType)).Length)
                    RightHoldableType = 0;

                animator.SetInteger("itemHandRight", (int)RightHoldableType);

                Destroy(RightEquiped.model.gameObject);
                Destroy(RightEquiped.gameObject);

                InstantialeRightHoldable();
            }
        }  
    }


    private void InstantialeRightHoldable()
    {
        RightEquiped = InstantiateHoldable(RightHoldableType, rightHoldableTransform);
        RightEquiped.model.parent = rightHoldableTransform;
    }

    private void InstantiateLeftHoldable()
    {
        LeftEquiped = InstantiateHoldable(LeftHoldableType, leftHoldableTransform);
        LeftEquiped.model.parent = leftHoldableTransform;
    }

    private Holdable InstantiateHoldable(HoldableType holdableType, Transform holdableTransform)
    {
        switch (holdableType)
        {
            case HoldableType.sword:
                return InstantiateHoldableAndExtractScript( swordPrefab, holdableTransform);

            case HoldableType.gun:
                return InstantiateHoldableAndExtractScript( gunPrefab,   holdableTransform);

            case HoldableType.shield:
                return InstantiateHoldableAndExtractScript(shieldPrefab, holdableTransform);

            default:
                return InstantiateHoldableAndExtractScript(Global.inst.emptyHandFist, holdableTransform);
        }   
    }

    private Holdable InstantiateHoldableAndExtractScript(GameObject weaponPrefab, Transform holdableTransform)
    {
        return Instantiate( weaponPrefab, holdableTransform.position, holdableTransform.rotation, (toolSnapingPoint ?? transform))
            .GetComponent<Holdable>();
    }
}

public enum HoldableType
{
    none = 0,
    sword = 1,
    gun = 2,
    shield = 3,
}
