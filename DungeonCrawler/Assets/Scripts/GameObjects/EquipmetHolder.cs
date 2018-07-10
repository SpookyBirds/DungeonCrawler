using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmetHolder : MonoBehaviour {

    [SerializeField] [Tooltip("The holdable equiped in the left hand")]
    private GameObject leftEquipedHoldable;
    public Holdable LeftHand { get; set; }
    [SerializeField] [Tooltip("The holdable equiped in the right hand")]
    private GameObject rightEquipedHoldable;
    public Holdable RightHand { get; set; }
    [SerializeField]
    [Tooltip("The helmet equiped on the character")]
    private GameObject helmetArmor;
    public Holdable Helmet { get; set; }
    [SerializeField]
    [Tooltip("The bodyarmor equiped on the character")]
    private GameObject bodyArmor;
    public Holdable Body { get; set; }
    [SerializeField]
    [Tooltip("The legarmor equiped on the character")]
    private GameObject legArmor;
    public Holdable Legs { get; set; }
    [Space]
    public Transform toolSnapingPoint;
    public Transform leftHandSnapingPoint;
    public Transform rightHandSnapingPoint;
    public Transform helmetSnapingPoint;
    public Transform bodyArmorSnapingPoint;
    public Transform legArmorSnapingPoint;

    private void Awake()
    {
        if (leftEquipedHoldable == null)
        {
            leftEquipedHoldable = Global.inst.EmptyHandFist;
        }
        
        LeftHand = Instantiate(leftEquipedHoldable, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        LeftHand.model.parent = leftHandSnapingPoint;
        LeftHand.model.localPosition = LeftHand.transformationPosition;
        LeftHand.model.localRotation = Quaternion.Euler(LeftHand.transformationRotation);


        if(rightEquipedHoldable == null)
        {
            rightEquipedHoldable = Global.inst.EmptyHandFist;

        }
        
        RightHand = Instantiate(rightEquipedHoldable, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        RightHand.model.parent = rightHandSnapingPoint;
        RightHand.model.localPosition = RightHand.transformationPosition;
        RightHand.model.localRotation = Quaternion.Euler(RightHand.transformationRotation);



        if (helmetArmor == null)
        {
            helmetSnapingPoint = null;
        }
        else if(helmetArmor != null)
        {
            Helmet = Instantiate(helmetArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
            Helmet.model.parent = helmetSnapingPoint;
            Helmet.model.localPosition = Helmet.transformationPosition;
            Helmet.model.localRotation = Quaternion.Euler(Helmet.transformationRotation);

        }

        if (bodyArmor == null)
        {
            bodyArmorSnapingPoint = null;
        }
        else if (bodyArmor != null)
        {
            Body = Instantiate(bodyArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
            Body.model.parent = bodyArmorSnapingPoint;
            Body.model.localPosition = Body.transformationPosition;
            Body.model.localRotation = Quaternion.Euler(Body.transformationRotation);

        }

        if (legArmor == null)
        {
            legArmorSnapingPoint = null;
        }
        if(legArmor != null)
        {
            Legs = Instantiate(legArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
            Legs.model.parent = legArmorSnapingPoint;
            Legs.model.localPosition = Body.transformationPosition;
            Legs.model.localRotation = Quaternion.Euler(Legs.transformationRotation);

        }



    }
}
