using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : Item {

    public Equipmentslots equipmentSlots;
   
    public float DMG = 0f;
    public float Protection = 0f;

    public override void Use()
    {
        base.Use();
        Debug.Log("Using " + name);
        EquipmentManager.instance.Equip(this);
    }


}

public enum Equipmentslots { Head,Body,Legs,Weapon,Shield}
