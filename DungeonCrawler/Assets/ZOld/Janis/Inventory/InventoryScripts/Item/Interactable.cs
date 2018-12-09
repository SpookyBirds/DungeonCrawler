using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;

    public bool inRange = false;

    public Transform InteractionTransform;

    
    public float distance;

    public bool Interacted = false;

    [SerializeField] [Tooltip("just for debugging")]
    private bool displayCollider;

    public virtual void Interact()
    {
        //Debug.Log("Interacted with" + transform);
        //this methode is meant to be overwritten!
    }

    public virtual void Update()
    {
        // checks Distance between player and Interactable object!
        distance = Vector3.Distance(Global.inst.player.transform.position, InteractionTransform.position);

        if (distance <= radius)
        {
            // if player is in range of an object goes into function
            OnRange();
        }

        if (distance >= radius)
        {
            // if player is not in rage of an object goes into function
            OutRange();
        }
    }

    public void OnRange()
    {
        // when the player is in range of an object, it sets the variables true, so a player can pickup an item.
        inRange = true;
            if(inRange && !Interacted)
            {
                Interact();
                Interacted = true;
            }
    }

    public void OutRange()
    {
        // resets the variables if player is not in range anymore
        inRange = false;
        Interacted = false;
    }


    
    private void OnDrawGizmosSelected()
    {
        if (displayCollider)
        {   // shows gizmo in the editor for the pickup range
            if (InteractionTransform == null)
            InteractionTransform = transform;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(InteractionTransform.position, radius);
        }
    }


    


}
