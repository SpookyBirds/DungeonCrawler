using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        //checks if there is more than one inventory.
        if(Instance != null)
        {
            Debug.LogWarning("More than one Inventory found!");
            return;
        }
        Instance = this;
    }
    #endregion

    //Delegate for updating inventory
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    
    public int inventorySpace = 10;
    public List<Items> items = new List<Items>();

    public bool AddItem(Items item)
    {
        //When the item is not a default item the player can pick it up if he has enough inventory space
        if (!item.isDefaultItem)
        {
            if(items.Count >= inventorySpace)
            {
                Debug.Log("Not enough room in Inventory!");
                return false;
            }
             items.Add(item);

            //subscription to a delagate that updates the inventory
            if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void RemoveItem(Items item)
    {
        //Removes the item from the list
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
