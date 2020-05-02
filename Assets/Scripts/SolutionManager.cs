using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Manager for everything involving compounds and ions
/// Author: Joseph Dasilva
/// </summary>
public class SolutionManager : MonoBehaviour
{
    /// <summary>
    /// List of currently tracked Anions
    /// </summary>
    List<SolutionMatrix.Anion> trackedAnions = new List<SolutionMatrix.Anion>();

    /// <summary>
    /// List of currnetly tracked Cations
    /// </summary>
    List<SolutionMatrix.Cation> trackedCations = new List<SolutionMatrix.Cation>();

    /// <summary>
    /// List of currently tracked Connections
    /// </summary>
    List<Connection> compounds = new List<Connection>();

    /// <summary>
    /// List of existing line GameObjects
    /// </summary>
    List<GameObject> lines = new List<GameObject>();

    [Tooltip("0 for all compounds, 1 for insoluble and exceptions, 2 for exceptions only")]
    [Range(0, 2)]
    public int COMPOUND_THRESH = -1;

    /// <summary>
    /// EasyAR Image Tracker Behaviour reference
    /// </summary>
    public easyar.ImageTrackerBehaviour imageTracker;
    
    /// <summary>
    /// Whether the number of precipitate points should be shown on compound lines
    /// </summary>
    public bool showPoints = true;

    /// <summary>
    /// Material to use on the line for soluble compounds
    /// </summary>
    public Material solubleMat;

    /// <summary>
    /// Material to use on the line for insoluble compounds
    /// </summary>
    public Material insolubleMat;

    /// <summary>
    /// Material to use on the line for exception compounds
    /// </summary>
    public Material specialMat;

    /// <summary>
    /// InfoList reference for use in the Info mode
    /// </summary>
    public InfoList infoList;

    /// <summary>
    /// Connection between two ions, signifying a compound
    /// </summary>
    struct Connection
    {
        public SolutionMatrix.Cation cation;
        public SolutionMatrix.Anion anion;
        public Vector3 start;
        public Vector3 end;
        public int type;
    }

    /// <summary>
    /// Possible types for an Ion
    /// </summary>
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

    /// <summary>
    /// Dictionary corresponding IonTypes to their Cation in SolutionMatrix.cations
    /// </summary>
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

    /// <summary>
    /// Dictionary corresponding Cations in SolutionMatrix.cations to their IonType
    /// </summary>
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

    /// <summary>
    /// Dictionary corresponding IonTypes to their Anion in SolutionMatrix.anions
    /// </summary>
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

    /// <summary>
    /// Dictionary corresponding Anions in SolutionMatrix.anions to their IonType
    /// </summary>
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

    /// <summary>
    /// Dictionary of existing Anions corresponding to their current position
    /// </summary>
    Dictionary<SolutionMatrix.Anion, Vector3> anPos = new Dictionary<SolutionMatrix.Anion, Vector3>();

    /// <summary>
    /// Dictionary of existing Cations corresponding to their current position
    /// </summary>
    Dictionary<SolutionMatrix.Cation, Vector3> catPos = new Dictionary<SolutionMatrix.Cation, Vector3>();

    /// <summary>
    /// Notifies the system that an IonType is being tracked and creates the appropriate Connections
    /// </summary>
    /// <param name="ion"> New IonType being tracked </param>
    /// <param name="position"> Position the IonType was found at </param>
    public void AddIon(IonType ion, Vector3 position)
    {
        // If the IonType is an Anion...
        if (anDict.TryGetValue(ion, out SolutionMatrix.Anion an))
        {
            // If not currently tracked
            if (!trackedAnions.Contains(an))
            {
                trackedAnions.Add(an);
                anPos.Add(an, position);
                //Debug.Log("ANION " + an.name + " POSITION IS " + position);
                AddConnection(an);
                if (infoList != null)
                {
                    infoList.AddIonToList(ion);
                }
            }
        }
        // If the IonType is a Cation...
        else if (catDict.TryGetValue(ion, out SolutionMatrix.Cation cat))
        {
            // If not currently tracked
            if (!trackedCations.Contains(cat))
            {
                trackedCations.Add(cat);
                catPos.Add(cat, position);
                //Debug.Log("CATION " + cat.name + " POSITION IS " + position);
                AddConnection(cat);
                if (infoList != null)
                {
                    infoList.AddIonToList(ion);
                }
            }
        }
    }

    /// <summary>
    /// Creates all of the necessary Connections between a given Cation and the currently tracked Anions
    /// </summary>
    /// <param name="cat"> Cation having Connections created </param>
    private void AddConnection(SolutionMatrix.Cation cat)
    {
        // Checks the tracked Anions to see if any of them form a compound with cat
        foreach (SolutionMatrix.Anion an in trackedAnions)
        {
            if (cat.compounds[an.index] >= COMPOUND_THRESH)
            {
                anPos.TryGetValue(an, out Vector3 anionPos);

                catPos.TryGetValue(cat, out Vector3 cationPos);

                // Creates the Connection if it doesn't already exist
                Connection conn = new Connection() { cation = cat, anion = an, start = cationPos, end = anionPos, type = cat.compounds[an.index] };
                if (!compounds.Contains(conn))
                {
                    compounds.Add(conn);

                    // Creates the line GameObject
                    GameObject obj = new GameObject();
                    LineRenderer rend = obj.AddComponent<LineRenderer>();
                    rend.positionCount = 2;
                    rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                    // Includes points or not
                    TextMesh textMesh = obj.AddComponent<TextMesh>();
                    if (showPoints)
                    {
                        textMesh.text = cat.name + " " + an.name + "\n" + (cat.precipitatePts + an.precipitatePts) + " points";
                    }
                    else
                    {
                        textMesh.text = cat.name + " " + an.name;
                    }

                    // Sets material of the line
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

                    // Adds DrawLine component
                    DrawLine draw = obj.AddComponent<DrawLine>();
                    draw.origin = conn.start;
                    draw.destination = conn.end;
                    catDictReverse.TryGetValue(cat, out draw.originIon);
                    anDictReverse.TryGetValue(an, out draw.destIon);
                    lines.Add(obj);

                    // Adds Wikipedia page button if Info mode
                    if (infoList != null)
                    {
                        infoList.AddCompoundToList(draw.originIon, draw.destIon);
                    }
                }
                //Debug.Log("CONNECTION POSITION IS " + conn.start + " TO " + conn.end);
            }
        }
    }

    /// <summary>
    /// Creates all of the necessary Connections between a given Anion and the currently tracked Cations
    /// </summary>
    /// <param name="an"> Anion having Connections created </param>
    private void AddConnection(SolutionMatrix.Anion an)
    {
        // Checks the tracked Cations to see if any of them form a compound with an
        foreach (SolutionMatrix.Cation cat in trackedCations)
        {
            if (an.compounds[cat.index] >= COMPOUND_THRESH)
            {
                anPos.TryGetValue(an, out Vector3 anionPos);

                catPos.TryGetValue(cat, out Vector3 cationPos);

                // Creates the Connection if it doesn't already exist
                Connection conn = new Connection() { cation = cat, anion = an, start = cationPos, end = anionPos, type = an.compounds[cat.index] };
                if (!compounds.Contains(conn))
                {
                    compounds.Add(conn);

                    // Creates the line GameObject
                    GameObject obj = new GameObject();
                    LineRenderer rend = obj.AddComponent<LineRenderer>();
                    rend.positionCount = 2;
                    rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                    // Includes points or not
                    TextMesh textMesh = obj.AddComponent<TextMesh>();
                    if (showPoints)
                    {
                        textMesh.text = cat.name + " " + an.name + "\n" + (cat.precipitatePts + an.precipitatePts) + " points";
                    }
                    else
                    {
                        textMesh.text = cat.name + " " + an.name;
                    }

                    // Sets the material of the line
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
                    
                    // Adds the DrawLine component
                    DrawLine draw = obj.AddComponent<DrawLine>();
                    draw.origin = conn.start;
                    draw.destination = conn.end;
                    catDictReverse.TryGetValue(cat, out draw.originIon);
                    anDictReverse.TryGetValue(an, out draw.destIon);
                    lines.Add(obj);

                    // Adds Wikipedia page button if Info mode
                    if (infoList != null)
                    {
                        infoList.AddCompoundToList(draw.originIon, draw.destIon);
                    }
                }
                //Debug.Log("CONNECTION POSITION IS " + conn.start + " TO " + conn.end);
            }
        }
    }

    /// <summary>
    /// Updates an ion's current position
    /// </summary>
    /// <param name="ion"> IonType being updated </param>
    /// <param name="position"> New position </param>
    public void UpdateIon(IonType ion, Vector3 position)
    {
        // Updates position if an anion
        if (anDict.TryGetValue(ion, out SolutionMatrix.Anion an))
        {
            if (trackedAnions.Contains(an))
            {
                anPos[an] = position;
                //Debug.Log("ANION " + an.name + " POSITION IS " + position);
            }
        }
        // Updates position if a cation
        else if (catDict.TryGetValue(ion, out SolutionMatrix.Cation cat))
        {
            if (trackedCations.Contains(cat))
            {
                catPos[cat] = position;
                UpdateConnection(cat);  // Connections exist only when both a cation and anion exist,
                                        // so this only needs to be called on one of them.
                                        // This function affects all of the Connections of a given Cation.
                //Debug.Log("CATION " + cat.name + " POSITION IS " + position);
            }
        }
    }

    /// <summary>
    /// Updates all of the Connections to a given Cation
    /// </summary>
    /// <param name="cat"> Cation's Connection to update </param>
    private void UpdateConnection(SolutionMatrix.Cation cat)
    {
        // Checks all of the Connections for a specific Cation
        foreach (Connection conn in compounds.ToArray())
        {
            // Updates the Connection's origin and destination positions
            if (conn.cation.Equals(cat))
            {
                anPos.TryGetValue(conn.anion, out Vector3 anionPos);

                catPos.TryGetValue(conn.cation, out Vector3 cationPos);

                Connection conn2 = new Connection() { cation = conn.cation, anion = conn.anion, start = cationPos, end = anionPos, type = conn.type };

                compounds.Remove(conn);
                compounds.Insert(0, conn2);

                // Checks all of the existing line GameObjects for the one corresponding to the Connection we have
                for(int i = 0; i < lines.Count; i++)
                {
                    catDict.TryGetValue(lines[i].GetComponent<DrawLine>().originIon, out SolutionMatrix.Cation c);
                    anDict.TryGetValue(lines[i].GetComponent<DrawLine>().destIon, out SolutionMatrix.Anion a);

                    // Updates the line's origin and destination
                    if (c.Equals(conn2.cation) && a.Equals(conn2.anion))
                    {
                        lines[i].GetComponent<DrawLine>().origin = conn2.start;
                        lines[i].GetComponent<DrawLine>().destination = conn2.end;
                    }
                }

            }
        }
    }

    /// <summary>
    /// Notifies the system that an IonType is no longer being tracked and removes the appropriate Connections
    /// </summary>
    /// <param name="ion"> IonType to remove </param>
    public void RemoveIon(IonType ion)
    {
        // If an IonType is an Anion
        if (anDict.TryGetValue(ion, out SolutionMatrix.Anion an))
        {
            if (trackedAnions.Contains(an))
            {
                // Remove Anion and Connections
                RemoveConnection(an);
                trackedAnions.Remove(an);
                anPos.Remove(an);

                // Remove Wikipedia button if in Info mode
                if (infoList != null)
                {
                    infoList.RemoveIonFromList(ion);
                }
            }
        }
        // If an IonType is a Cation
        else if (catDict.TryGetValue(ion, out SolutionMatrix.Cation cat))
        {
            if (trackedCations.Contains(cat))
            {
                // Remove Cation and Connections
                RemoveConnection(cat);
                trackedCations.Remove(cat);
                catPos.Remove(cat);
                
                // Remove Wikipedia button if in Info mode
                if (infoList != null)
                {
                    infoList.RemoveIonFromList(ion);
                }
            }
        }
    }

    /// <summary>
    /// Destroys all of a given Cation's Connections
    /// </summary>
    /// <param name="cat"> Cation to destroy the Connections of </param>
    private void RemoveConnection(SolutionMatrix.Cation cat)
    {
        // Checks all Connections for cat Cation and removes it from list of compounds
        foreach (Connection conn in compounds)
        {
            if (conn.cation.Equals(cat))
            {
                compounds.Remove(conn);

                // Checks all line GameObjects for cat Cation and destroys it
                for (int i = 0; i < lines.Count; i++)
                {
                    catDict.TryGetValue(lines[i].GetComponent<DrawLine>().originIon, out SolutionMatrix.Cation c);

                    if (c.Equals(cat))
                    {
                        // Destroys Wikipedia page button if Info mode
                        if (infoList != null)
                        {
                            infoList.RemoveCompoundFromList(lines[i].GetComponent<DrawLine>().originIon, lines[i].GetComponent<DrawLine>().destIon);
                        }
                        
                        // Destroys line GameObject
                        Destroy(lines[i]);
                        lines.Remove(lines[i]);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Destroys all of a given Anion's Connections
    /// </summary>
    /// <param name="an"> Anion to destroy the Connections of </param>
    private void RemoveConnection(SolutionMatrix.Anion an)
    {
        // Checks all Connections for an Anion and removes it from list of compounds
        foreach (Connection conn in compounds)
        {
            if (conn.anion.Equals(an))
            {
                compounds.Remove(conn);

                // Checks all line GameObjects for an Anion and destroys it
                for (int i = 0; i < lines.Count; i++)
                {
                    anDict.TryGetValue(lines[i].GetComponent<DrawLine>().destIon, out SolutionMatrix.Anion a);

                    if (a.Equals(an))
                    {
                        // Destroys Wikipedia page button if Info mode
                        if (infoList != null)
                        {
                            infoList.RemoveCompoundFromList(lines[i].GetComponent<DrawLine>().originIon, lines[i].GetComponent<DrawLine>().destIon);
                        }

                        // Destroys line GameObject
                        Destroy(lines[i]);
                        lines.Remove(lines[i]);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Gets a list of all tracked IonTypes, regardless of Cation or Anion
    /// </summary>
    /// <returns> List of IonTypes being tracked </returns>
    public List<IonType> GetTrackedIons()
    {
        List<IonType> allTrackedIons = new List<IonType>();

        foreach (SolutionMatrix.Anion an in trackedAnions)
        {
            anDictReverse.TryGetValue(an, out IonType ion);
            allTrackedIons.Add(ion);
        }

        foreach (SolutionMatrix.Cation cat in trackedCations)
        {
            catDictReverse.TryGetValue(cat, out IonType ion);
            allTrackedIons.Add(ion);
        }

        return allTrackedIons;
    }

    /// <summary>
    /// Gets a list of all tracked compounds as IonType arrays
    /// </summary>
    /// <returns> List of IonType compound arrays being tracked, where index 0 is Cation and index 1 is Anion </returns>
    public List<IonType[]> GetTrackedCompounds()
    {
        List<IonType[]> allTrackedCompounds = new List<IonType[]>();

        foreach (Connection conn in compounds)
        {
            IonType[] compound = new IonType[2];
            catDictReverse.TryGetValue(conn.cation, out compound[0]);
            anDictReverse.TryGetValue(conn.anion, out compound[1]);
            allTrackedCompounds.Add(compound);
        }

        return allTrackedCompounds;
    }
}
