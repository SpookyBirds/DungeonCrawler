using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    
    public static Inventory Instance;

    private void Awake()
    {
        //checks if there is more than one inventory.
        if (Instance != null)
        {
            Debug.LogWarning("More than one Inventory found!");
            return;
        }
        Instance = this;
    }
  

    //Delegate for updating inventory
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int inventorySpace = 10;
    public List<Item> items = new List<Item>();

    /// <summary>
    /// Add an item to the inventory. Returns false if the item coundn't be added
    /// </summary>
    /// <param name="item">Iem to add</param>
    public bool AddItem(Item item)
    {
        //When the item is not a default item the player can pick it up if he has enough inventory space
        
        
            if (items.Count >= inventorySpace)
            {
                Debug.Log("Not enough room in Inventory!");
                return false;
            }
            items.Add(item);

            //subscription to a delagate that updates the inventory
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        
        return true;
    }

    /// <summary>
    /// Deletes the item from the inventory
    /// </summary>
    public void DeleteItem(Item item)
    {
        //Removes the item from the list
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    /// <summary>
    /// Drops the item to the ground (instantiating) and calls DeleteItem()
    /// </summary>
    public void DropItem(Item item)
    {
        //Removes the item from the list
        Instantiate(item.itemPrefab, transform.position, Quaternion.identity, Global.inst.level);
        DeleteItem(item);
    }
}