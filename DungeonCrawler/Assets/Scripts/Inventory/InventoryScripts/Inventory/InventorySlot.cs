using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    
    Item item;

    public void AddItem(Item newItem)
    {
        // Adds the item to the inventory
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        // clears the item from the inventory
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void onRemoveButton()
    {
        //Removes item from the list
        Inventory.Instance.DeleteItem(item);

    }

    public void UseItem ()
    {
        //checks if there is a item in the itemslot. if there is you can use it.
        if(item != null)
        {
            item.Use();
        }

    }
}
