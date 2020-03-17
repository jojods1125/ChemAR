using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Vector3 origin;
    public Vector3 destination;
    public float lineDrawSpeed = 6f;

    [HideInInspector]
    public SolutionManager.IonType originIon;

    [HideInInspector]
    public SolutionManager.IonType destIon;

    public Material solubleMat;
    public Material insolubleMat;
    public Material specialMat;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin);
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!origin.Equals(null) && !destination.Equals(null))
        {
            dist = Vector3.Distance(origin, destination);

            if (counter < dist)
            {
                counter += .1f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, counter);

                Vector3 pointA = origin;
                Vector3 pointB = destination;

                Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

                lineRenderer.SetPosition(0, origin);
                lineRenderer.SetPosition(1, pointAlongLine);
            }

            if (counter > dist)
            {
                Vector3 pointA = origin;
                Vector3 pointB = destination;

                Vector3 pointAlongLine = dist * Vector3.Normalize(pointB - pointA) + pointA;

                lineRenderer.SetPosition(0, origin);
                lineRenderer.SetPosition(1, pointAlongLine);
            }
        }
    }

    public void SetType(int type)
    {
        //if (type == 0)
        //{
        //    lineRenderer.material = solubleMat;
        //}
        //else if (type == 1)
        //{
        //    lineRenderer.
        //}
        //else if (type == 2)
        //{
        //    lineRenderer.material = specialMat;
        //}
    }
}
