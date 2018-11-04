using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurve : MonoBehaviour {

    public GameObject curve;

    public float speed;

    public bool active;

    public bool destroyAtEndpoint;

    private float counter;

    private int currentPositionNumber;

    private int startPositionNumber;

    private int endPositionNumber;

    private bool forwardDirection = true;

    void Start ()
    {
        endPositionNumber = curve.GetComponent<QuadraticCurve>().numberOfPositions - 1;
	}
	
	void Update ()
    {
        if(active)
        {
            counter += speed * Time.deltaTime;

            if (counter >= 1)
            {
                counter = 0;
                if (forwardDirection)
                    currentPositionNumber++;
                else
                    currentPositionNumber--;

                if (currentPositionNumber == endPositionNumber)
                {
                    if (destroyAtEndpoint)
                        Destroy(gameObject);
                    active = false;
                    forwardDirection = false;
                }
                if (currentPositionNumber == startPositionNumber)
                {
                    forwardDirection = true;
                    active = false;
                }
            }
            transform.position = curve.GetComponent<QuadraticCurve>().positions[currentPositionNumber];
        }
    }
}
