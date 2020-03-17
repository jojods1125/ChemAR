using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ion : MonoBehaviour
{
    public SolutionManager manager;
    public SolutionManager.IonType ion;


    // Called in ImageTargetController
    public void AddIon()
    {
        manager.AddIon(ion, gameObject.transform.position);
    }

    // Called in ImageTargetController
    public void UpdateIon()
    {
        manager.UpdateIon(ion, gameObject.transform.position);
    }

    // Called in ImageTargetController
    public void RemoveIon()
    {
        manager.RemoveIon(ion);
    }
}
