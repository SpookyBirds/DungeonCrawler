using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one Inventory found!");
            return;
        }
        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    
    public int inventorySpace = 10;
    public List<Items> items = new List<Items>();

    public bool AddItem(Items item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= inventorySpace)
            {
                Debug.Log("Not enough room in Inventory!");
                return false;
            }
             items.Add(item);

            if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void RemoveItem(Items item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
