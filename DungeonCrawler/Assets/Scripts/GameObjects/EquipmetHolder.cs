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
    [Space]
    public Transform toolSnapingPoint;
    public Transform leftHandSnapingPoint;
    public Transform rightHandSnapingPoint;

    private void Awake()
    {
        LeftHand  = Instantiate(leftEquipedHoldable,  (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        RightHand = Instantiate(rightEquipedHoldable, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();

        LeftHand.model.parent = leftHandSnapingPoint;
        RightHand.model.parent = rightHandSnapingPoint;

        LeftHand.model.localPosition = LeftHand.transformationPosition;
        LeftHand.model.localRotation = Quaternion.Euler(LeftHand.transformationRotation);

        RightHand.model.localPosition = RightHand.transformationPosition;
        RightHand.model.localRotation = Quaternion.Euler(RightHand.transformationRotation);
    }
}
