using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmetHolder : MonoBehaviour {

    [SerializeField] [Tooltip("The holdable equiped in the left hand")]
    private GameObject leftEquipedHoldable;
    public Holdable_old LeftHand { get; set; }
    [SerializeField] [Tooltip("The holdable equiped in the right hand")]
    private GameObject rightEquipedHoldable;
    public Holdable_old RightHand { get; set; }
    [SerializeField] [Tooltip("The helmet equiped on the character")]
    private GameObject helmetArmor;
    public Holdable_old Helmet { get; set; }
    [SerializeField] [Tooltip("The bodyarmor equiped on the character")]
    private GameObject bodyArmor;
    public Holdable_old Body { get; set; }
    [SerializeField] [Tooltip("The legarmor equiped on the character")]
    private GameObject legArmor;
    public Holdable_old Legs { get; set; }
    [Space]
    public Transform toolSnapingPoint;
    public Transform leftHandSnapingPoint;
    public Transform rightHandSnapingPoint;
    public Transform helmetSnapingPoint;
    public Transform bodyArmorSnapingPoint;
    public Transform legArmorSnapingPoint;

    public Item leftHandSlot;
    public Item rightHandSlot;

    private void Awake()
    {
        InitializHelmet();
        InitializBody();
        InitializLegs();
        InitializRightHand();
        InitializLeftHand();
    }
    
    public void InitializHelmet()
    {
        if (helmetArmor == null)
        {
            helmetSnapingPoint = null;
        }
        else if (helmetArmor != null)
        {
            Helmet = Instantiate(helmetArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable_old>();
            Helmet.model.parent = helmetSnapingPoint;
            Helmet.model.localPosition = Helmet.transformationPosition;
            Helmet.model.localRotation = Quaternion.Euler(Helmet.transformationRotation);

        }
    }

    public void InitializBody()
    {
        if (bodyArmor == null)
        {
            bodyArmorSnapingPoint = null;
        }
        else if (bodyArmor != null)
        {
            Body = Instantiate(bodyArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable_old>();
            Body.model.parent = bodyArmorSnapingPoint;
            Body.model.localPosition = Body.transformationPosition;
            Body.model.localRotation = Quaternion.Euler(Body.transformationRotation);

        }
    }

    public void InitializLegs()
    {
        if (legArmor == null)
        {
            legArmorSnapingPoint = null;
        }
        if (legArmor != null)
        {
            Legs = Instantiate(legArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable_old>();
            Legs.model.parent = legArmorSnapingPoint;
            Legs.model.localPosition = Body.transformationPosition;
            Legs.model.localRotation = Quaternion.Euler(Legs.transformationRotation);

        }
    }

    public void InitializLeftHand()
    {
        if (leftEquipedHoldable == null)
        {
            leftEquipedHoldable = Global.inst.emptyHandFist;
        }

        LeftHand = Instantiate(leftEquipedHoldable, (toolSnapingPoint ?? transform)).GetComponent<Holdable_old>();
        LeftHand.model.parent = leftHandSnapingPoint;
        LeftHand.model.localPosition = LeftHand.transformationPosition;
        LeftHand.model.localRotation = Quaternion.Euler(LeftHand.transformationRotation);
    }

    public void InitializRightHand()
    {
        if (rightEquipedHoldable == null)
        {
            rightEquipedHoldable = Global.inst.emptyHandFist;

        }

        RightHand = Instantiate(rightEquipedHoldable, (toolSnapingPoint ?? transform)).GetComponent<Holdable_old>();
        RightHand.model.parent = rightHandSnapingPoint;
        RightHand.model.localPosition = RightHand.transformationPosition;
        RightHand.model.localRotation = Quaternion.Euler(RightHand.transformationRotation);
    }
}
