using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Ion class
/// Author: Joseph Dasilva
/// </summary>
public class Ion : MonoBehaviour
{
    /// <summary>
    /// SolutionManager in the scene
    /// </summary>
    public SolutionManager manager;

    /// <summary>
    /// Type of Ion
    /// </summary>
    public SolutionManager.IonType ion;

    /// <summary>
    /// Adds the Ion to the list of Ions being tracked in the SolutionManager,
    /// called by ImageTargetController
    /// </summary>
    public void AddIon()
    {
        manager.AddIon(ion, gameObject.transform.position);
    }

    /// <summary>
    /// Updates the Ion in the list of Ions being tracked in the SolutionManager,
    /// called by ImageTargetController
    /// </summary>
    public void UpdateIon()
    {
        manager.UpdateIon(ion, gameObject.transform.position);
    }

    /// <summary>
    /// Removes the Ion from the list of Ions being tracked in the SolutionManager,
    /// called by ImageTargetController
    /// </summary>
    public void RemoveIon()
    {
        manager.RemoveIon(ion);
    }
}
