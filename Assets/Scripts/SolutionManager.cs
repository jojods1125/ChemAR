using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SolutionManager : MonoBehaviour
{
    List<SolutionMatrix.Anion> trackedAnions = new List<SolutionMatrix.Anion>();
    List<SolutionMatrix.Cation> trackedCations = new List<SolutionMatrix.Cation>();
    List<Connection> compounds = new List<Connection>();

    public easyar.ImageTrackerBehaviour imageTracker;
    public Text textUI;

    struct Connection
    {
        public SolutionMatrix.Cation cation;
        public SolutionMatrix.Anion anion;
        public Vector3 start;
        public Vector3 end;
        public int type;
    }

    public enum IonType
    {
        Ammonium,
        Sodium,
        Potassium,
        Lithium,
        Magnesium,
        Calcium,
        Strontium,
        Barium,
        IronIII,
        CopperII,
        Silver,
        Zinc,
        LeadII,
        Aluminum,

        Nitrate,
        Acetate,
        Chlorate,
        Chloride,
        Bromide,
        Iodide,
        Sulfate,
        Carbonate,
        Chromate,
        Hydroxide,
        Phosphate
    }

    readonly Dictionary<IonType, SolutionMatrix.Cation> catDict = new Dictionary<IonType, SolutionMatrix.Cation>()
    {
        { IonType.Ammonium, SolutionMatrix.cations[0] },
        { IonType.Sodium, SolutionMatrix.cations[1] },
        { IonType.Potassium, SolutionMatrix.cations[2] },
        { IonType.Lithium, SolutionMatrix.cations[3] },
        { IonType.Magnesium, SolutionMatrix.cations[4] },
        { IonType.Calcium, SolutionMatrix.cations[5] },
        { IonType.Strontium, SolutionMatrix.cations[6] },
        { IonType.Barium, SolutionMatrix.cations[7] },
        { IonType.IronIII, SolutionMatrix.cations[8] },
        { IonType.CopperII, SolutionMatrix.cations[9] },
        { IonType.Silver, SolutionMatrix.cations[10] },
        { IonType.Zinc, SolutionMatrix.cations[11] },
        { IonType.LeadII, SolutionMatrix.cations[12] },
        { IonType.Aluminum, SolutionMatrix.cations[13] }
    };


    readonly Dictionary<IonType, SolutionMatrix.Anion> anDict = new Dictionary<IonType, SolutionMatrix.Anion>()
    {
        { IonType.Nitrate, SolutionMatrix.anions[0] },
        { IonType.Acetate, SolutionMatrix.anions[1] },
        { IonType.Chlorate, SolutionMatrix.anions[2] },
        { IonType.Chloride, SolutionMatrix.anions[3] },
        { IonType.Bromide, SolutionMatrix.anions[4] },
        { IonType.Iodide, SolutionMatrix.anions[5] },
        { IonType.Sulfate, SolutionMatrix.anions[6] },
        { IonType.Carbonate, SolutionMatrix.anions[7] },
        { IonType.Chromate, SolutionMatrix.anions[8] },
        { IonType.Hydroxide, SolutionMatrix.anions[9] },
        { IonType.Phosphate, SolutionMatrix.anions[10] }
    };

    Dictionary<SolutionMatrix.Anion, Vector3> anPos = new Dictionary<SolutionMatrix.Anion, Vector3>();
    Dictionary<SolutionMatrix.Cation, Vector3> catPos = new Dictionary<SolutionMatrix.Cation, Vector3>();

    public void AddIon(IonType ion, Vector3 position)
    {
        SolutionMatrix.Anion an;
        SolutionMatrix.Cation cat;

        if (anDict.TryGetValue(ion, out an))
        {
            if (!trackedAnions.Contains(an))
            {
                trackedAnions.Add(an);
                anPos.Add(an, position);
                Debug.Log("ANION " + an.name + " POSITION IS " + position);
                AddConnection(an);
            }
        } 
        else if (catDict.TryGetValue(ion, out cat))
        {
            if (!trackedCations.Contains(cat))
            {
                trackedCations.Add(cat);
                catPos.Add(cat, position);
                Debug.Log("CATION " + cat.name + " POSITION IS " + position);
                AddConnection(cat);
            }
        }
    }

    private void AddConnection(SolutionMatrix.Cation cat)
    {
        foreach (SolutionMatrix.Anion an in trackedAnions)
        {
            if (cat.compounds[an.index] > 0)
            {
                Vector3 anionPos;
                anPos.TryGetValue(an, out anionPos);
                
                Vector3 cationPos;
                catPos.TryGetValue(cat, out cationPos);

                Connection conn = new Connection() { cation = cat, anion = an, start = cationPos, end = anionPos, type = cat.compounds[an.index] };
                if (!compounds.Contains(conn))
                {
                    compounds.Add(conn);
                }

                Debug.Log("CONNECTION POSITION IS " + conn.start + " TO " + conn.end);
            }
        }
    }

    private void AddConnection(SolutionMatrix.Anion an)
    {
        foreach (SolutionMatrix.Cation cat in trackedCations)
        {
            if (an.compounds[cat.index] > 0)
            {
                Vector3 anionPos;
                anPos.TryGetValue(an, out anionPos);

                Vector3 cationPos;
                catPos.TryGetValue(cat, out cationPos);

                Connection conn = new Connection() { cation = cat, anion = an, start = cationPos, end = anionPos, type = an.compounds[cat.index] };
                if (!compounds.Contains(conn))
                {
                    compounds.Add(conn);
                }

                Debug.Log("CONNECTION POSITION IS " + conn.start + " TO " + conn.end);
            }
        }
    }
    



    public void RemoveIon(IonType ion)
    {
        SolutionMatrix.Anion an;
        SolutionMatrix.Cation cat;

        if (anDict.TryGetValue(ion, out an))
        {
            if (trackedAnions.Contains(an))
            {
                RemoveConnection(an);
                trackedAnions.Remove(an);
                anPos.Remove(an);
            }
        }
        else if (catDict.TryGetValue(ion, out cat))
        {
            if (trackedCations.Contains(cat))
            {
                RemoveConnection(cat);
                trackedCations.Remove(cat);
                catPos.Remove(cat);
            }
        }
    }

    private void RemoveConnection(SolutionMatrix.Cation cat)
    {
        foreach (Connection conn in compounds)
        {
            if (conn.cation.Equals(cat))
            {
                compounds.Remove(conn);
            }
        }
    }

    private void RemoveConnection(SolutionMatrix.Anion an)
    {
        foreach (Connection conn in compounds)
        {
            if (conn.anion.Equals(an))
            {
                compounds.Remove(conn);
            }
        }
    }


    private void Update()
    {
        textUI.text = "";
        foreach (Connection conn in compounds)
        {
            //LineRenderer line = gameObject.GetComponent<LineRenderer>();
            //Vector3[] vec = new Vector3[] { conn.start, conn.end };
            //line.SetPositions(vec);
            //Debug.DrawLine(conn.start, conn.end, Color.green);
            textUI.text += conn.cation.name + " " + conn.anion.name + ",  ";
        }
    }


    void UpdateConnections()
    {
        foreach (SolutionMatrix.Anion an in trackedAnions)
        {
            foreach (SolutionMatrix.Cation cat in trackedCations)
            {
                if (an.compounds[cat.index] > 0)
                {
                    Connection conn = new Connection() { cation = cat, anion = an, start = Vector3.zero, end = Vector3.zero, type = an.compounds[cat.index] };
                    if (!compounds.Contains(conn))
                    {
                        compounds.Add(conn);
                    }
                }
            }
        }
    }
}
