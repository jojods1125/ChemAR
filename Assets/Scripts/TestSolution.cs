using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test script for testing soluble/insoluble/exception functionality
public class TestSolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int cat = 5;
        int an = 6;

        SolutionMatrix.Cation cation = (SolutionMatrix.Cation)SolutionMatrix.cations.GetValue(cat);
        SolutionMatrix.Anion anion = (SolutionMatrix.Anion)SolutionMatrix.anions.GetValue(an);

        // Prints if a specific compound is soluble, insoluble, or exception
        if ((int)cation.compounds.GetValue(an) == 0)
        {
            Debug.Log("SOLUBLE - " + cation.name + " " + anion.name);
        }
        else if ((int)cation.compounds.GetValue(an) == 1)
        {
            Debug.Log("INSOLUBLE - " + cation.name + " " + anion.name);
        }
        else
        {
            Debug.Log("EXCEPTION - " + cation.name + " " + anion.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(((SolutionMatrix.Cation)SolutionMatrix.cations.GetValue(0)).name);

        
    }
}
