using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentType equipmentType;
    private Equipment Newitem;

    public override void Use()
    {
        base.Use();
        Debug.Log("Using " + itemName);
        EquipmentManager.instance.Equip(this);
        Inventory.Instance.DeleteItem(this);
    }
    

}
