﻿using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EquipmentManager : MonoBehaviour {

    public static EquipmentManager instance;

    [SerializeField] private Image[] images = new Image[System.Enum.GetNames(typeof(EquipmentType)).Length];

    private Equipment[] currentEquipment;
    //public Button removeButton1;
    public Transform itemsParents;
    

    EquipmentSlots[] slots;
    EquipmentSlots equipment;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    { 
        int slotNumbers = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Equipment[slotNumbers];
        slots = itemsParents.GetComponentsInChildren<EquipmentSlots>();
    }

    public void Equip(Equipment newItem)
    {

        Equipment oldItem = null;
        int slotIndex = (int)newItem.equipmentType;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Inventory.Instance.AddItem(oldItem);
                
        }


        currentEquipment[slotIndex] = newItem;
        images[slotIndex].enabled = true;
        images[slotIndex].sprite = newItem.icon;
        RemoveSwitcher(slotIndex);
    }

    /// <summary>
    /// Unequips item. Ignores the command if the inventory is full
    /// </summary>
    /// <param name="slotIndex"></param>
    public void OnUnEquip(int slotIndex)
    {
        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            if (Inventory.Instance.AddItem(oldItem) == false)
                return;
        }

       

        currentEquipment[slotIndex] = null;
        images[slotIndex].enabled = false;
        images[slotIndex].sprite = null;
        RemoveSwitcher(slotIndex);
    }

    public void RemoveSwitcher(int slotIndex)
    {
       
            if (currentEquipment[slotIndex] != null)
            {
                slots[slotIndex].activateRemove();
            }
            else
            {
                slots[slotIndex].deactivateRemove();
            }
    }
}
