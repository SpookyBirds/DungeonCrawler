using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour {

    public float radius = 1f;
    public Color color = Color.red;
    [Space]
    public bool drawDebugWires = true;

    private void OnDrawGizmos()
    {
        if (drawDebugWires)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }


}
