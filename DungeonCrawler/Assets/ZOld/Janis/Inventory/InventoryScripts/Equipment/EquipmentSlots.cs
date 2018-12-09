using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlots : MonoBehaviour {

    public Button removeButton;

    Item item;

    public void activateRemove()
    {
        // Adds the item to the inventory
        removeButton.interactable = true;
    }

    public void deactivateRemove()
    {
        // clears the item from the inventory
        removeButton.interactable = false;
    }
}
