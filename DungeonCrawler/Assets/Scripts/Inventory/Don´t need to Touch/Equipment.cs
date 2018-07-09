using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : Items {

    public Equipmentslots equipmentSlots;
   
    public float DMG = 0f;
    public float Protection = 0f;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }


}

public enum Equipmentslots { Head,Body,Legs,Weapon,Shield}
