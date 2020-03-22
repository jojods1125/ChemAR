using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private TextMesh textMesh;
    private float counter;
    private float dist;

    public Vector3 origin;
    public Vector3 destination;
    public float lineDrawSpeed = 6f;

    [HideInInspector]
    public SolutionManager.IonType originIon;

    [HideInInspector]
    public SolutionManager.IonType destIon;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin);
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;

        textMesh = GetComponent<TextMesh>();
        textMesh.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (!origin.Equals(null) && !destination.Equals(null))
        {
            dist = Vector3.Distance(origin, destination);

            CreateText();

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

    public void CreateText()
    {
        //Debug.Log(originIon.ToString() + " " + destIon.ToString());

        textMesh.transform.position = (destination + origin) / 2;
        textMesh.transform.LookAt(((destination + origin) / 2) + new Vector3(0,0,1));
    }
}
