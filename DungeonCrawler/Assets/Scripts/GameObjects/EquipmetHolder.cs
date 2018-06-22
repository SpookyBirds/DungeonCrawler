using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmetHolder : MonoBehaviour {

    [SerializeField]
    private GameObject leftHand;
    public Holdable LeftHand { get; set; }
    [SerializeField]
    private GameObject rightHand;
    public Holdable RightHand { get; set; }
    [Space]
    public Transform toolSnapingPoint;
    public Transform leftHandSnapingPoint;
    public Transform rightHandSnapingPoint;

    private void Awake()
    {
        LeftHand  = Instantiate(leftHand,  (toolSnapingPoint ?? transform)).GetComponent<Holdable>();
        RightHand = Instantiate(rightHand, (toolSnapingPoint ?? transform)).GetComponent<Holdable>();

        LeftHand.model.parent = leftHandSnapingPoint;
        RightHand.model.parent = rightHandSnapingPoint;

        LeftHand.model.localPosition = LeftHand.transformationPosition;
        LeftHand.model.localRotation = Quaternion.Euler(LeftHand.transformationRotation);

        RightHand.model.localPosition = RightHand.transformationPosition;
        RightHand.model.localRotation = Quaternion.Euler(RightHand.transformationRotation);
    }
}
