using UnityEngine;

public class Gun : Holdable
{
    [Space]
    [SerializeField]
    private float maxReach = 10;
    [SerializeField]
    private float damagePerHit = 10;

    private PointerSupplier pointerSupplier;
    public ParticleSystem shotParticle;
    
    // Secures that only one shot is fired
    private bool isAiming = false;
    private bool IsAiming
    {
        get { return isAiming; }
        set
        {
            isAiming = value;
            StartAim(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        pointerSupplier = Camera.main.GetComponent<PointerSupplier>();
    }

    public override bool UseLong(Controller controller)
    {
        IsAiming = true;
        return false;
    }

    public override void UpdateUse(Controller controller, bool quit)
    {
        if (quit)
        {
            if (IsAiming)
            {
                IsAiming = false;
                Shoot(
                    controller, 
                    pointerSupplier.cameraMovementController.RotationCenterPoint.position,  
                    Camera.main.ScreenPointToRay(Input.mousePosition).direction
                );
            }
        }
    }

    public override bool UseShort(Controller controller)
    {
        return Shoot(controller, model.position, pointerSupplier.character.forward);
    }

    private void StartAim(bool doAim)
    {
        pointerSupplier.cameraMovementController.ToggleCameraAimingPosition(doAim);
    }

    private bool Shoot(Controller controller, Vector3 position, Vector3 direction)
    {
        ShotingParticles();
        // Get all collider in shoot distance
        RaycastHit[] hits = Physics.RaycastAll(
            position, direction, maxReach);

        // Return if no one was found
        if (hits.Length <= 0)
            return false;

        // Hold the distance of everyone hit in regard to the shooter 
        float[] distancesToHit = new float[hits.Length];

        // Evaluate if the hit was an opponent for everyone, save it's distance if it was, save int.MaxValue if not
        for (int index = 0; index < hits.Length; index++)
        {
            if (hits[index].collider.IsAnyTagEqual(controller.EnemyTypes) == false &&
                     hits[index].collider.IsTagNeutral() == false)
            {
                distancesToHit[index] = int.MaxValue;
                continue;
            }
            distancesToHit[index] = 
                Vector3.Distance(hits[index].transform.position, pointerSupplier.character.position);
        }

        // The variable to save at which index the nearest opponent from the hits array lies
        int indexOfNearestOpponent = 0;

        // Evaluating which is the nearest and saving it's index in the variable a line above
        for (int index = 1; index < hits.Length; index++)
        {
            if (distancesToHit[index] < distancesToHit[indexOfNearestOpponent])
                indexOfNearestOpponent = index;
        }

        // If the "nearest" still is int.MaxValue, return because no enemy was hit (details above)
        if (distancesToHit[indexOfNearestOpponent] == int.MaxValue)
            return false;

        // Get the evaluated entity, then check null, just for the sake of safety, and finally damage it
        Entity entityToDamage;
        if((entityToDamage = hits[indexOfNearestOpponent].collider.GetComponent<Entity>()) != null)
        {
            entityToDamage.TryToDamage(damagePerHit);
            return true;
        }
        
        return false;
    }

    void ShotingParticles()
    {
        //Starts spawning the particles when the player shoots
        shotParticle.Play();
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawRay(transform.position, pointerSupplier.character.forward * maxReach);
    //    Gizmos.DrawRay(
    //        pointerSupplier.cameraMovementController.RotationCenterPoint.position,
    //        Camera.main.ScreenPointToRay(Input.mousePosition).direction * maxReach);
    //}
}
