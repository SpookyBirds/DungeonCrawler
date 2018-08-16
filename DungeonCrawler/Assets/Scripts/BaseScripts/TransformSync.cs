using UnityEngine;

public class TransformSync : MonoBehaviour {

    public Transform SyncTransform { get; set; }

    public SyncMode SyncMode { get; set; }

    public TransformSync()
    {
        SyncMode = SyncMode.CurrentToOther;
    }

    private void Update()
    {
        if (SyncTransform == null)
            return;

        switch (SyncMode)
        {
            case SyncMode.CurrentToOther:
                transform.position = SyncTransform.position;
                transform.rotation = SyncTransform.rotation;
                break;

            case SyncMode.OtherToCurrent:
                SyncTransform.position = transform.position;
                SyncTransform.rotation = transform.rotation;
                break;

            default: break;
        }
    }

}                          
