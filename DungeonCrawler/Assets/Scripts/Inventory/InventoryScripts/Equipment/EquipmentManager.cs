using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    private Equipment[] currentEquipment;

    public void Start()
    { 
        int slotNumbers = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Equipment[slotNumbers];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentType;
        currentEquipment[slotIndex] = newItem;
    }
}
