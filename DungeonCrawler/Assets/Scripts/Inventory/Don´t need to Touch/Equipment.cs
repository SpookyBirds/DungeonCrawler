using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : ScriptableObject {

    new public string name = "New Equipment";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public float DMG = 0f;
    public float Protection = 0f;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }


}

public enum Equipmentslots { Head,Body,Legs,Weapon,Shield}
