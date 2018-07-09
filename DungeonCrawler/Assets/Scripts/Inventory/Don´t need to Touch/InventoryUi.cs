using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour {

    Inventory inventory;

    public Transform itemsParents;

    InventorySlot[] slots;


	// Use this for initialization
	void Start () {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallBack += updateUi;

        slots = itemsParents.GetComponentsInChildren<InventorySlot>();
	}

    void updateUi()
    {
        // iterates through the children to get all item slots
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
