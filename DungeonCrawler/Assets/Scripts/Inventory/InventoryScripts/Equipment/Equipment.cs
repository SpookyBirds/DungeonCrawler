﻿using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentType equipmentType;
   
    public override void Use()
    {
        base.Use();
        Debug.Log("Using " + itemName);
        EquipmentManager.instance.Equip(this);
    }
}
