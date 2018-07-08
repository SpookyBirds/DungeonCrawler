using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName ="Inventory/Items")]
public class Items : ScriptableObject {

    new public string name = "new Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

}
