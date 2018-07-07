using UnityEngine;

public class ItemPickUp : Interactable {

    public Items Item;

    public override void Interact()
    {
        base.Interact();
        PickUp();

    }
    
    public void PickUp()
    {
        

    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            Debug.Log("Picking up Item " + Item.name);
            bool wasPickedUp = Inventory.Instance.AddItem(Item);

            if(wasPickedUp)
                Destroy(gameObject);
        }
    }
}
