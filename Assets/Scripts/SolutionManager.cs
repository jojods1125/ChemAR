using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SolutionManager : MonoBehaviour
{
    List<SolutionMatrix.Anion> trackedAnions = new List<SolutionMatrix.Anion>();
    List<SolutionMatrix.Cation> trackedCations = new List<SolutionMatrix.Cation>();
    List<Connection> compounds = new List<Connection>();

    List<GameObject> lines = new List<GameObject>();

    public int COMPOUND_THRESH = -1;

    public easyar.ImageTrackerBehaviour imageTracker;
    public Text textUI;

    public Material solubleMat;
    public Material insolubleMat;
    public Material specialMat;

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

    readonly Dictionary<SolutionMatrix.Cation, IonType> catDictReverse = new Dictionary<SolutionMatrix.Cation, IonType>()
    {
        { SolutionMatrix.cations[0], IonType.Ammonium },
        { SolutionMatrix.cations[1], IonType.Sodium },
        { SolutionMatrix.cations[2], IonType.Potassium },
        { SolutionMatrix.cations[3], IonType.Lithium },
        { SolutionMatrix.cations[4], IonType.Magnesium },
        { SolutionMatrix.cations[5], IonType.Calcium },
        { SolutionMatrix.cations[6], IonType.Strontium },
        { SolutionMatrix.cations[7], IonType.Barium },
        { SolutionMatrix.cations[8], IonType.IronIII },
        { SolutionMatrix.cations[9], IonType.CopperII },
        { SolutionMatrix.cations[10], IonType.Silver },
        { SolutionMatrix.cations[11], IonType.Zinc },
        { SolutionMatrix.cations[12], IonType.LeadII },
        { SolutionMatrix.cations[13], IonType.Aluminum }
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

    readonly Dictionary<SolutionMatrix.Anion, IonType> anDictReverse = new Dictionary<SolutionMatrix.Anion, IonType>()
    {
        { SolutionMatrix.anions[0], IonType.Nitrate },
        { SolutionMatrix.anions[1], IonType.Acetate },
        { SolutionMatrix.anions[2], IonType.Chlorate },
        { SolutionMatrix.anions[3], IonType.Chloride },
        { SolutionMatrix.anions[4], IonType.Bromide },
        { SolutionMatrix.anions[5], IonType.Iodide },
        { SolutionMatrix.anions[6], IonType.Sulfate },
        { SolutionMatrix.anions[7], IonType.Carbonate },
        { SolutionMatrix.anions[8], IonType.Chromate },
        { SolutionMatrix.anions[9], IonType.Hydroxide },
        { SolutionMatrix.anions[10], IonType.Phosphate }
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
                //Debug.Log("ANION " + an.name + " POSITION IS " + position);
                AddConnection(an);
            }
        } 
        else if (catDict.TryGetValue(ion, out cat))
        {
            if (!trackedCations.Contains(cat))
            {
                trackedCations.Add(cat);
                catPos.Add(cat, position);
                //Debug.Log("CATION " + cat.name + " POSITION IS " + position);
                AddConnection(cat);
            }
        }
    }

    private void AddConnection(SolutionMatrix.Cation cat)
    {
        foreach (SolutionMatrix.Anion an in trackedAnions)
        {
            if (cat.compounds[an.index] > COMPOUND_THRESH)
            {
                Vector3 anionPos;
                anPos.TryGetValue(an, out anionPos);
                
                Vector3 cationPos;
                catPos.TryGetValue(cat, out cationPos);

                Connection conn = new Connection() { cation = cat, anion = an, start = cationPos, end = anionPos, type = cat.compounds[an.index] };
                if (!compounds.Contains(conn))
                {
                    compounds.Add(conn);

                    GameObject obj = new GameObject();
                    LineRenderer rend = obj.AddComponent<LineRenderer>();
                    rend.positionCount = 2;
                    rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                    TextMesh textMesh = obj.AddComponent<TextMesh>();
                    textMesh.text = cat.name + " " + an.name + "\n" + (cat.aqueousPts + an.aqueousPts) + " points" ;

                    if (conn.type == 0)
                    {
                        rend.material = solubleMat;
                    }
                    else if (conn.type == 1)
                    {
                        rend.material = insolubleMat;
                    }
                    else if (conn.type == 2)
                    {
                        rend.material = specialMat;
                    }

                    DrawLine draw = obj.AddComponent<DrawLine>();
                    //draw.solubleMat = solubleMat;
                    //draw.insolubleMat = insolubleMat;
                    //draw.specialMat = specialMat;
                    draw.origin = conn.start;
                    draw.destination = conn.end;
                    //draw.SetType(conn.type);
                    catDictReverse.TryGetValue(cat, out draw.originIon);
                    anDictReverse.TryGetValue(an, out draw.destIon);
                    lines.Add(obj);
                }

                //Debug.Log("CONNECTION POSITION IS " + conn.start + " TO " + conn.end);
            }
        }
    }

    private void AddConnection(SolutionMatrix.Anion an)
    {
        foreach (SolutionMatrix.Cation cat in trackedCations)
        {
            if (an.compounds[cat.index] > COMPOUND_THRESH)
            {
                Vector3 anionPos;
                anPos.TryGetValue(an, out anionPos);

                Vector3 cationPos;
                catPos.TryGetValue(cat, out cationPos);

                Connection conn = new Connection() { cation = cat, anion = an, start = cationPos, end = anionPos, type = an.compounds[cat.index] };
                if (!compounds.Contains(conn))
                {
                    compounds.Add(conn);

                    GameObject obj = new GameObject();
                    LineRenderer rend = obj.AddComponent<LineRenderer>();
                    rend.positionCount = 2;
                    rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                    TextMesh textMesh = obj.AddComponent<TextMesh>();
                    textMesh.text = cat.name + " " + an.name + "\n" + (cat.aqueousPts + an.aqueousPts) + " points";

                    if (conn.type == 0)
                    {
                        rend.material = solubleMat;
                    }
                    else if (conn.type == 1)
                    {
                        rend.material = insolubleMat;
                    }
                    else if (conn.type == 2)
                    {
                        rend.material = specialMat;
                    }

                    DrawLine draw = obj.AddComponent<DrawLine>();
                    //draw.solubleMat = solubleMat;
                    //draw.insolubleMat = insolubleMat;
                    //draw.specialMat = specialMat;
                    draw.origin = conn.start;
                    draw.destination = conn.end;
                    //draw.SetType(conn.type);
                    catDictReverse.TryGetValue(cat, out draw.originIon);
                    anDictReverse.TryGetValue(an, out draw.destIon);
                    lines.Add(obj);
                }

                //Debug.Log("CONNECTION POSITION IS " + conn.start + " TO " + conn.end);
            }
        }
    }

    public void UpdateIon(IonType ion, Vector3 position)
    {
        SolutionMatrix.Anion an;
        SolutionMatrix.Cation cat;

        if (anDict.TryGetValue(ion, out an))
        {
            if (trackedAnions.Contains(an))
            {
                anPos[an] = position;
                //Debug.Log("ANION " + an.name + " POSITION IS " + position);
            }
        }
        else if (catDict.TryGetValue(ion, out cat))
        {
            if (trackedCations.Contains(cat))
            {
                catPos[cat] = position;
                //Debug.Log("CATION " + cat.name + " POSITION IS " + position);
                UpdateConnection(cat);
            }
        }
    }

    private void UpdateConnection(SolutionMatrix.Cation cat)
    {
        foreach (Connection conn in compounds.ToArray())
        {
            if (conn.cation.Equals(cat))
            {
                Vector3 anionPos;
                anPos.TryGetValue(conn.anion, out anionPos);

                Vector3 cationPos;
                catPos.TryGetValue(conn.cation, out cationPos);

                Connection conn2 = new Connection() { cation = conn.cation, anion = conn.anion, start = cationPos, end = anionPos, type = conn.type };

                compounds.Remove(conn);
                compounds.Insert(0, conn2);

                for(int i = 0; i < lines.Count; i++)
                {
                    catDict.TryGetValue(lines[i].GetComponent<DrawLine>().originIon, out SolutionMatrix.Cation c);
                    anDict.TryGetValue(lines[i].GetComponent<DrawLine>().destIon, out SolutionMatrix.Anion a);

                    if (c.Equals(conn2.cation) && a.Equals(conn2.anion))
                    {
                        lines[i].GetComponent<DrawLine>().origin = conn2.start;
                        lines[i].GetComponent<DrawLine>().destination = conn2.end;
                    }
                }

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

                for (int i = 0; i < lines.Count; i++)
                {
                    catDict.TryGetValue(lines[i].GetComponent<DrawLine>().originIon, out SolutionMatrix.Cation c);

                    if (c.Equals(cat))
                    {
                        Destroy(lines[i]);
                        lines.Remove(lines[i]);
                    }
                }
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

                for (int i = 0; i < lines.Count; i++)
                {
                    anDict.TryGetValue(lines[i].GetComponent<DrawLine>().destIon, out SolutionMatrix.Anion a);

                    if (a.Equals(an))
                    {
                        Destroy(lines[i]);
                        lines.Remove(lines[i]);
                    }
                }
            }
        }
    }


    private void Update()
    {
        textUI.text = "";

        foreach (Connection conn in compounds)
        {
            textUI.text += conn.cation.name + " " + conn.anion.name + ",  ";
        }
    }

}
