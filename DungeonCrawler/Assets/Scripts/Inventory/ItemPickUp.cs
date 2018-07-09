using UnityEngine;

public class ItemPickUp : Interactable {

    public Item Item;


    public override void Interact()
    {
        //checks if you interact with the item to pick it up
        base.Interact();
        

    }
   
    public override void Update()
    {
        base.Update();
        // Checks if the object is in range. if it is the player can pick it up.
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            Debug.Log("Picking up Item " + Item.itemName);
            bool wasPickedUp = Inventory.Instance.AddItem(Item);

            // When the object is picked up it get´s destroyed.
            if(wasPickedUp)
                Destroy(gameObject);
        }
    }
}
