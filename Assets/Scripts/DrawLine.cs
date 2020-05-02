using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws a line between two locations
/// Author: Joseph Dasilva
/// </summary>
public class DrawLine : MonoBehaviour
{
    // LineRenderer within the line gameobject
    private LineRenderer lineRenderer;
    // TextMesh within the line gameobject
    private TextMesh textMesh;
    // Current length
    private float counter;
    // Desired distance
    private float dist;

    /// <summary>
    /// Point the line begins at
    /// </summary>
    public Vector3 origin;

    /// <summary>
    /// Point the line ends at
    /// </summary>
    public Vector3 destination;

    /// <summary>
    /// How long it takes for the line to be drawn; higher number is slower
    /// </summary>
    public float lineDrawSpeed = 6f;

    /// <summary>
    /// Ion the origin is located at
    /// </summary>
    [HideInInspector]
    public SolutionManager.IonType originIon;

    /// <summary>
    /// Ion the destination is located at
    /// </summary>
    [HideInInspector]
    public SolutionManager.IonType destIon;

    // Start is called before the first frame update
    void Start()
    {
        // Set the LineRenderer to preferred values
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin);
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;

        // Set the TextMesh to preferred values
        textMesh = GetComponent<TextMesh>();
        textMesh.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        textMesh.fontSize = 100;
        textMesh.characterSize = 10;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.color = Color.black;

        // Create a shadow for the TextMesh
        GameObject inner = new GameObject();
        inner.transform.SetParent(gameObject.transform);
        TextMesh innerTextMesh = CopyComponent(textMesh, inner);
        innerTextMesh.color = Color.white;
        innerTextMesh.transform.localPosition = new Vector3(3, -3, 0);
        innerTextMesh.offsetZ = -5;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a valid origin and destination
        if (!origin.Equals(null) && !destination.Equals(null))
        {
            dist = Vector3.Distance(origin, destination);

            OrientText();

            // If line is currently not done expanding
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

            // If line has been expanded
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

    /// <summary>
    /// Sets the text's location and rotation
    /// </summary>
    public void OrientText()
    {
        //Debug.Log(originIon.ToString() + " " + destIon.ToString());

        textMesh.transform.position = (destination + origin) / 2;
        textMesh.transform.LookAt(((destination + origin) / 2) + new Vector3(0,0,1));
    }

    /// <summary>
    /// Duplicates a TextMesh component into a new GameObject
    /// </summary>
    /// <param name="original"> Original TextMesh component </param>
    /// <param name="destination"> GameObject for the TextMesh component </param>
    /// <returns></returns>
    TextMesh CopyComponent(TextMesh original, GameObject destination)
    {
        TextMesh textMesh = destination.AddComponent<TextMesh>();
        textMesh.text = original.text;
        textMesh.transform.localScale = new Vector3(1, 1, 1);
        textMesh.fontSize = original.fontSize;
        textMesh.characterSize = original.characterSize;
        textMesh.anchor = original.anchor;
        textMesh.alignment = original.alignment;
        textMesh.fontStyle = original.fontStyle;
        textMesh.color = original.color;
        return textMesh;
    }
}
