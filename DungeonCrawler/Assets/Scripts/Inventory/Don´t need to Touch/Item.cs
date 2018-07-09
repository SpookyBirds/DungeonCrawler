using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName ="Inventory/Items")]
public class Item : ScriptableObject {
    // Define item-name, icon and if it´s default
    public string itemName = "new Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public GameObject itemPrefab;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }

}
