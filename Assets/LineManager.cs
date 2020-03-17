using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [HideInInspector]
    public int lineCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ManageConnections()
    {
        GameObject obj = new GameObject();
        LineRenderer rend = obj.AddComponent<LineRenderer>();
        DrawLine draw = obj.AddComponent<DrawLine>();




        //d.origin = conn.start;
        //d.destination = conn.end;
        //lines.Add(d);
    }
}
