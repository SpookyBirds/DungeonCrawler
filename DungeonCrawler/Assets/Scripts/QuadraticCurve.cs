using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticCurve : MonoBehaviour {

    [SerializeField]
    private Transform point1;

    [SerializeField]
    private Transform point2;

    [SerializeField]
    private Transform point3;

    [HideInInspector]
    public Vector3[] positions;

    public int numberOfPositions = 50;

    //[SerializeField]
    //private GameObject followingObject;

    //[SerializeField]
    //private float speedOfFollowingObject;

    //private float counter;

    //private int currentPosition;

    //private bool forwardDirection = true;


    //alternative
    //private int startPosition = 0;
    //private int endPosition;
    //public bool active;

    private LineRenderer lineRenderer;

    [SerializeField][Tooltip("Activate when you want to Change the Curve, otherwise please uncheck")]
    private bool updateCurveToRuntime;

    void Start()
    {
        positions = new Vector3[numberOfPositions];

        //endPosition = numberOfPositions-1;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPositions;
        DrawCurve();
    }

    void Update()
    {
        if(updateCurveToRuntime)
            DrawCurve();

        //if (active)
            //MoveObjectAlongCurve();
    }

    private void DrawCurve()
    {
        for(int i = 1; i < numberOfPositions+1; i++)
        {
            float t = (float)i / (float)numberOfPositions;
            positions[i-1] = (1 - t) * (1 - t) * point1.position + 2 * (1 - t) * t * point2.position + t * t * point3.position;
        }

        lineRenderer.SetPositions(positions);

    }

    /*private void MoveObjectAlongCurve()
    {
        counter += speedOfFollowingObject * Time.deltaTime;

        if (counter >= 1)
        {
            counter = 0;
            if (forwardDirection)
                currentPosition++;
            else
                currentPosition--;

            if (currentPosition == endPosition)
            {
                active = false;
                forwardDirection = false;
            }
            if(currentPosition == startPosition)
            {
                forwardDirection = true;
                active = false;
            }
        }

        followingObject.transform.position = positions[currentPosition];
    }*/
}
