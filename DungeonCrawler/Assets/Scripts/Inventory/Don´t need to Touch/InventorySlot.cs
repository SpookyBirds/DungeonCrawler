using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    
    Items item;

    public void AddItem(Items newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void onRemoveButton()
    {
        Inventory.Instance.RemoveItem(item);
    }

    public void UseItem ()
    {
        if(item != null)
        {
            item.Use();
        }

    }
}
