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

    private Vector3[] positions;

    [SerializeField]
    private int numberOfPositions = 50;

    [SerializeField]
    private GameObject followingObject;

    [SerializeField]
    private float speedOfFollowingObject;

    private float counter;

    private int currentPosition;

    private bool forwardDirection = true;

    private LineRenderer lineRenderer;

    [SerializeField][Tooltip("Activate when you want to Change the Curve, otherwise please uncheck")]
    private bool updateCurveToRuntime;

    void Start()
    {
        positions = new Vector3[numberOfPositions];

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPositions;
        DrawCurve();
    }

    void Update()
    {
        if(updateCurveToRuntime)
        {
            DrawCurve();
        }
        FollowCurve();
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

    private void FollowCurve()
    {
        counter += speedOfFollowingObject * Time.deltaTime;

        if(counter>=1)
        {
            counter = 0;
            if (forwardDirection)
                currentPosition++;
            else
                currentPosition--;

            if (currentPosition == numberOfPositions-1)
                forwardDirection = false;

            if (currentPosition == 0)
                forwardDirection = true;
        }

        followingObject.transform.position = positions[currentPosition];
    }
}
