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
        LeftHand  = Instantiate(leftEquipedHoldable,  (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        RightHand = Instantiate(rightEquipedHoldable, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        Helmet = Instantiate(helmetArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        Body = Instantiate(bodyArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        Legs = Instantiate(legArmor, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();

        LeftHand.model.parent = leftHandSnapingPoint;
        RightHand.model.parent = rightHandSnapingPoint;
        Helmet.model.parent = helmetSnapingPoint;
        Body.model.parent = bodyArmorSnapingPoint;
        Legs.model.parent = legArmorSnapingPoint;

        LeftHand.model.localPosition = LeftHand.transformationPosition;
        LeftHand.model.localRotation = Quaternion.Euler(LeftHand.transformationRotation);

        RightHand.model.localPosition = RightHand.transformationPosition;
        RightHand.model.localRotation = Quaternion.Euler(RightHand.transformationRotation);

        Helmet.model.localPosition = Helmet.transformationPosition;
        Helmet.model.localRotation = Quaternion.Euler(Helmet.transformationRotation);

        Body.model.localPosition = Body.transformationPosition;
        Body.model.localRotation = Quaternion.Euler(Body.transformationRotation);

        Legs.model.localPosition = Body.transformationPosition;
        Legs.model.localRotation = Quaternion.Euler(Legs.transformationRotation);
    }
}
