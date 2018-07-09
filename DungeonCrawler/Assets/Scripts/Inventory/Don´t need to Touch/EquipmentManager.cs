using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;

    public void Start()
    {
        int SlotNumbers = System.Enum.GetNames(typeof(Equipmentslots)).Length;
        currentEquipment = new Equipment[SlotNumbers];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlots;

        currentEquipment[slotIndex] = newItem;
    }
}
