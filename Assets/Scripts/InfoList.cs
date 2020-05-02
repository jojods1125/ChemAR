using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Deals with all functionality for the list of Wikipedia pages for each ion/compound in Info mode
/// Author: Joseph Dasilva
/// </summary>
public class InfoList : MonoBehaviour
{
    /// <summary>
    /// Sample button GameObject to recreate
    /// </summary>
    public GameObject sampleButton;

    /// <summary>
    /// Dictionary of Wikipedia pages coordinated to IonType
    /// </summary>
    readonly Dictionary<SolutionManager.IonType, string> ionURLs = new Dictionary<SolutionManager.IonType, string>()
    {
        { SolutionManager.IonType.Ammonium,     "https://en.wikipedia.org/wiki/Ammonium" },
        { SolutionManager.IonType.Sodium,       "https://en.wikipedia.org/wiki/Sodium" },
        { SolutionManager.IonType.Potassium,    "https://en.wikipedia.org/wiki/Potassium" },
        { SolutionManager.IonType.Lithium,      "https://en.wikipedia.org/wiki/Lithium" },
        { SolutionManager.IonType.Magnesium,    "https://en.wikipedia.org/wiki/Magnesium" },
        { SolutionManager.IonType.Calcium,      "https://en.wikipedia.org/wiki/Calcium" },
        { SolutionManager.IonType.Strontium,    "https://en.wikipedia.org/wiki/Strontium" },
        { SolutionManager.IonType.Barium,       "https://en.wikipedia.org/wiki/Barium" },
        { SolutionManager.IonType.IronIII,      "https://en.wikipedia.org/wiki/Iron(III)" },
        { SolutionManager.IonType.CopperII,     "https://en.wikipedia.org/wiki/Copper" },
        { SolutionManager.IonType.Silver,       "https://en.wikipedia.org/wiki/Silver" },
        { SolutionManager.IonType.Zinc,         "https://en.wikipedia.org/wiki/Zinc" },
        { SolutionManager.IonType.LeadII,       "https://en.wikipedia.org/wiki/Lead" },
        { SolutionManager.IonType.Aluminum,     "https://en.wikipedia.org/wiki/Aluminum" },
        { SolutionManager.IonType.Nitrate,      "https://en.wikipedia.org/wiki/Nitrate" },
        { SolutionManager.IonType.Acetate,      "https://en.wikipedia.org/wiki/Acetate" },
        { SolutionManager.IonType.Chlorate,     "https://en.wikipedia.org/wiki/Chlorate" },
        { SolutionManager.IonType.Chloride,     "https://en.wikipedia.org/wiki/Chloride" },
        { SolutionManager.IonType.Bromide,      "https://en.wikipedia.org/wiki/Bromide" },
        { SolutionManager.IonType.Iodide,       "https://en.wikipedia.org/wiki/Iodide" },
        { SolutionManager.IonType.Sulfate,      "https://en.wikipedia.org/wiki/Sulfate" },
        { SolutionManager.IonType.Carbonate,    "https://en.wikipedia.org/wiki/Carbonate" },
        { SolutionManager.IonType.Chromate,     "https://en.wikipedia.org/wiki/Chromate_and_dichromate" },
        { SolutionManager.IonType.Hydroxide,    "https://en.wikipedia.org/wiki/Hydroxide" },
        { SolutionManager.IonType.Phosphate,    "https://en.wikipedia.org/wiki/Phosphate" }
    };

    /// <summary>
    /// Dictionary of Wikipedia pages coordinated to compound name strings
    /// </summary>
    readonly Dictionary<string, string> compoundURLs = new Dictionary<string, string>()
    {
        { "Ammonium Nitrate",       "https://en.wikipedia.org/wiki/Ammonium_nitrate" },
        { "Ammonium Acetate",       "https://en.wikipedia.org/wiki/Ammonium_acetate" },
        { "Ammonium Chlorate",      "https://en.wikipedia.org/wiki/Ammonium_chlorate" },
        { "Ammonium Chloride",      "https://en.wikipedia.org/wiki/Ammonium_chloride" },
        { "Ammonium Bromide",       "https://en.wikipedia.org/wiki/Ammonium_bromide" },
        { "Ammonium Iodide",        "https://en.wikipedia.org/wiki/Ammonium_iodide" },
        { "Ammonium Sulfate",       "https://en.wikipedia.org/wiki/Ammonium_sulfate" },
        { "Ammonium Carbonate",     "https://en.wikipedia.org/wiki/Ammonium_carbonate" },
        { "Ammonium Chromate",      "https://en.wikipedia.org/wiki/Ammonium_chromate" },
        { "Ammonium Hydroxide",     "https://en.wikipedia.org/wiki/Ammonia_solution" },
        { "Ammonium Phosphate",     "https://en.wikipedia.org/wiki/Ammonium_phosphate" },

        { "Sodium Nitrate",         "https://en.wikipedia.org/wiki/Sodium_nitrate" },
        { "Sodium Acetate",         "https://en.wikipedia.org/wiki/Sodium_acetate" },
        { "Sodium Chlorate",        "https://en.wikipedia.org/wiki/Sodium_chlorate" },
        { "Sodium Chloride",        "https://en.wikipedia.org/wiki/Sodium_chloride" },
        { "Sodium Bromide",         "https://en.wikipedia.org/wiki/Sodium_bromide" },
        { "Sodium Iodide",          "https://en.wikipedia.org/wiki/Sodium_iodide" },
        { "Sodium Sulfate",         "https://en.wikipedia.org/wiki/Sodium_sulfate" },
        { "Sodium Carbonate",       "https://en.wikipedia.org/wiki/Sodium_carbonate" },
        { "Sodium Chromate",        "https://en.wikipedia.org/wiki/Sodium_chromate" },
        { "Sodium Hydroxide",       "https://en.wikipedia.org/wiki/Sodium_hydroxide" },
        { "Sodium Phosphate",       "https://en.wikipedia.org/wiki/Sodium_phosphate" },

        { "Potassium Nitrate",      "https://en.wikipedia.org/wiki/Potassium_nitrate" },
        { "Potassium Acetate",      "https://en.wikipedia.org/wiki/Potassium_acetate" },
        { "Potassium Chlorate",     "https://en.wikipedia.org/wiki/Potassium_chlorate" },
        { "Potassium Chloride",     "https://en.wikipedia.org/wiki/Potassium_chloride" },
        { "Potassium Bromide",      "https://en.wikipedia.org/wiki/Potassium_bromide" },
        { "Potassium Iodide",       "https://en.wikipedia.org/wiki/Potassium_iodide" },
        { "Potassium Sulfate",      "https://en.wikipedia.org/wiki/Potassium_sulfate" },
        { "Potassium Carbonate",    "https://en.wikipedia.org/wiki/Potassium_carbonate" },
        { "Potassium Chromate",     "https://en.wikipedia.org/wiki/Potassium_chromate" },
        { "Potassium Hydroxide",    "https://en.wikipedia.org/wiki/Potassium_hydroxide" },
        { "Potassium Phosphate",    "https://en.wikipedia.org/wiki/Potassium_phosphate" },

        { "Lithium Nitrate",        "https://en.wikipedia.org/wiki/Lithium_nitrate" },
        { "Lithium Acetate",        "https://en.wikipedia.org/wiki/Lithium_acetate" },
        { "Lithium Chlorate",       "https://en.wikipedia.org/wiki/Lithium_chlorate" },
        { "Lithium Chloride",       "https://en.wikipedia.org/wiki/Lithium_chloride" },
        { "Lithium Bromide",        "https://en.wikipedia.org/wiki/Lithium_bromide" },
        { "Lithium Iodide",         "https://en.wikipedia.org/wiki/Lithium_iodide" },
        { "Lithium Sulfate",        "https://en.wikipedia.org/wiki/Lithium_sulfate" },
        { "Lithium Carbonate",      "https://en.wikipedia.org/wiki/Lithium_carbonate" },
      //{ "Lithium Chromate",       "https://en.wikipedia.org/wiki/Lithium_chromate" },
        { "Lithium Hydroxide",      "https://en.wikipedia.org/wiki/Lithium_hydroxide" },
      //{ "Lithium Phosphate",      "https://en.wikipedia.org/wiki/Lithium_phosphate" },

        { "Magnesium Nitrate",      "https://en.wikipedia.org/wiki/Magnesium_nitrate" },
        { "Magnesium Acetate",      "https://en.wikipedia.org/wiki/Magnesium_acetate" },
        { "Magnesium Chlorate",     "https://en.wikipedia.org/wiki/Magnesium_chlorate" },
        { "Magnesium Chloride",     "https://en.wikipedia.org/wiki/Magnesium_chloride" },
        { "Magnesium Bromide",      "https://en.wikipedia.org/wiki/Magnesium_bromide" },
        { "Magnesium Iodide",       "https://en.wikipedia.org/wiki/Magnesium_iodide" },
        { "Magnesium Sulfate",      "https://en.wikipedia.org/wiki/Magnesium_sulfate" },
        { "Magnesium Carbonate",    "https://en.wikipedia.org/wiki/Magnesium_carbonate" },
        { "Magnesium Chromate",     "https://en.wikipedia.org/wiki/Magnesium_chromate" },
        { "Magnesium Hydroxide",    "https://en.wikipedia.org/wiki/Magnesium_hydroxide" },
        { "Magnesium Phosphate",    "https://en.wikipedia.org/wiki/Magnesium_phosphate" },

        { "Calcium Nitrate",        "https://en.wikipedia.org/wiki/Calcium_nitrate" },
        { "Calcium Acetate",        "https://en.wikipedia.org/wiki/Calcium_acetate" },
        { "Calcium Chlorate",       "https://en.wikipedia.org/wiki/Calcium_chlorate" },
        { "Calcium Chloride",       "https://en.wikipedia.org/wiki/Calcium_chloride" },
        { "Calcium Bromide",        "https://en.wikipedia.org/wiki/Calcium_bromide" },
        { "Calcium Iodide",         "https://en.wikipedia.org/wiki/Calcium_iodide" },
        { "Calcium Sulfate",        "https://en.wikipedia.org/wiki/Calcium_sulfate" },
        { "Calcium Carbonate",      "https://en.wikipedia.org/wiki/Calcium_carbonate" },
        { "Calcium Chromate",       "https://en.wikipedia.org/wiki/Calcium_chromate" },
        { "Calcium Hydroxide",      "https://en.wikipedia.org/wiki/Calcium_hydroxide" },
        { "Calcium Phosphate",      "https://en.wikipedia.org/wiki/Calcium_phosphate" },

        { "Strontium Nitrate",      "https://en.wikipedia.org/wiki/Strontium_nitrate" },
      //{ "Strontium Acetate",      "https://en.wikipedia.org/wiki/Strontium_acetate" },
        { "Strontium Chlorate",     "https://en.wikipedia.org/wiki/Strontium_chlorate" },
        { "Strontium Chloride",     "https://en.wikipedia.org/wiki/Strontium_chloride" },
        { "Strontium Bromide",      "https://en.wikipedia.org/wiki/Strontium_bromide" },
        { "Strontium Iodide",       "https://en.wikipedia.org/wiki/Strontium_iodide" },
        { "Strontium Sulfate",      "https://en.wikipedia.org/wiki/Strontium_sulfate" },
        { "Strontium Carbonate",    "https://en.wikipedia.org/wiki/Strontium_carbonate" },
        { "Strontium Chromate",     "https://en.wikipedia.org/wiki/Strontium_chromate" },
        { "Strontium Hydroxide",    "https://en.wikipedia.org/wiki/Strontium_hydroxide" },
      //{ "Strontium Phosphate",    "https://en.wikipedia.org/wiki/Strontium_phosphate" },

        { "Barium Nitrate",         "https://en.wikipedia.org/wiki/Barium_nitrate" },
        { "Barium Acetate",         "https://en.wikipedia.org/wiki/Barium_acetate" },
        { "Barium Chlorate",        "https://en.wikipedia.org/wiki/Barium_chlorate" },
        { "Barium Chloride",        "https://en.wikipedia.org/wiki/Barium_chloride" },
        { "Barium Bromide",         "https://en.wikipedia.org/wiki/Barium_bromide" },
        { "Barium Iodide",          "https://en.wikipedia.org/wiki/Barium_iodide" },
        { "Barium Sulfate",         "https://en.wikipedia.org/wiki/Barium_sulfate" },
        { "Barium Carbonate",       "https://en.wikipedia.org/wiki/Barium_carbonate" },
        { "Barium Chromate",        "https://en.wikipedia.org/wiki/Barium_chromate" },
        { "Barium Hydroxide",       "https://en.wikipedia.org/wiki/Barium_hydroxide" },
      //{ "Barium Phosphate",       "https://en.wikipedia.org/wiki/Barium_phosphate" },


        { "IronIII Nitrate",        "https://en.wikipedia.org/wiki/Iron(III)_nitrate" },
        { "IronIII Acetate",        "https://en.wikipedia.org/wiki/Iron(III)_acetate" },
      //{ "IronIII Chlorate",       "https://en.wikipedia.org/wiki/Iron(III)_chlorate" },
        { "IronIII Chloride",       "https://en.wikipedia.org/wiki/Iron(III)_chloride" },
        { "IronIII Bromide",        "https://en.wikipedia.org/wiki/Iron(III)_bromide" },
      //{ "IronIII Iodide",         "https://en.wikipedia.org/wiki/Iron(III)_iodide" },
        { "IronIII Sulfate",        "https://en.wikipedia.org/wiki/Iron(III)_sulfate" },
      //{ "IronIII Carbonate",      "https://en.wikipedia.org/wiki/Iron(III)_carbonate" },
        { "IronIII Chromate",       "https://en.wikipedia.org/wiki/Iron(III)_chromate" },
        { "IronIII Hydroxide",      "https://en.wikipedia.org/wiki/Iron(III)_hydroxide" },
        { "IronIII Phosphate",      "https://en.wikipedia.org/wiki/Iron(III)_phosphate" },

        { "CopperII Nitrate",       "https://en.wikipedia.org/wiki/Copper(II)_nitrate" },
        { "CopperII Acetate",       "https://en.wikipedia.org/wiki/Copper(II)_acetate" },
        { "CopperII Chlorate",      "https://en.wikipedia.org/wiki/Copper(II)_chlorate" },
        { "CopperII Chloride",      "https://en.wikipedia.org/wiki/Copper(II)_chloride" },
        { "CopperII Bromide",       "https://en.wikipedia.org/wiki/Copper(II)_bromide" },
      //{ "CopperII Iodide",        "https://en.wikipedia.org/wiki/Copper(II)_iodide" },
        { "CopperII Sulfate",       "https://en.wikipedia.org/wiki/Copper(II)_sulfate" },
        { "CopperII Carbonate",     "https://en.wikipedia.org/wiki/Copper(II)_carbonate" },
      //{ "CopperII Chromate",      "https://en.wikipedia.org/wiki/Copper(II)_chromate" },
        { "CopperII Hydroxide",     "https://en.wikipedia.org/wiki/Copper(II)_hydroxide" },
        { "CopperII Phosphate",     "https://en.wikipedia.org/wiki/Copper(II)_phosphate" },

        { "Silver Nitrate",         "https://en.wikipedia.org/wiki/Silver_nitrate" },
        { "Silver Acetate",         "https://en.wikipedia.org/wiki/Silver_acetate" },
        { "Silver Chlorate",        "https://en.wikipedia.org/wiki/Silver_chlorate" },
        { "Silver Chloride",        "https://en.wikipedia.org/wiki/Silver_chloride" },
        { "Silver Bromide",         "https://en.wikipedia.org/wiki/Silver_bromide" },
        { "Silver Iodide",          "https://en.wikipedia.org/wiki/Silver_iodide" },
        { "Silver Sulfate",         "https://en.wikipedia.org/wiki/Silver_sulfate" },
        { "Silver Carbonate",       "https://en.wikipedia.org/wiki/Silver_carbonate" },
        { "Silver Chromate",        "https://en.wikipedia.org/wiki/Silver_chromate" },
        { "Silver Hydroxide",       "https://en.wikipedia.org/wiki/Silver_hydroxide" },
        { "Silver Phosphate",       "https://en.wikipedia.org/wiki/Silver_phosphate" },

        { "Zinc Nitrate",           "https://en.wikipedia.org/wiki/Zinc_nitrate" },
        { "Zinc Acetate",           "https://en.wikipedia.org/wiki/Zinc_acetate" },
        { "Zinc Chlorate",          "https://en.wikipedia.org/wiki/Zinc_chlorate" },
        { "Zinc Chloride",          "https://en.wikipedia.org/wiki/Zinc_chloride" },
        { "Zinc Bromide",           "https://en.wikipedia.org/wiki/Zinc_bromide" },
        { "Zinc Iodide",            "https://en.wikipedia.org/wiki/Zinc_iodide" },
        { "Zinc Sulfate",           "https://en.wikipedia.org/wiki/Zinc_sulfate" },
        { "Zinc Carbonate",         "https://en.wikipedia.org/wiki/Smithsonite" },
        { "Zinc Chromate",          "https://en.wikipedia.org/wiki/Zinc_chromate" },
        { "Zinc Hydroxide",         "https://en.wikipedia.org/wiki/Zinc_hydroxide" },
        { "Zinc Phosphate",         "https://en.wikipedia.org/wiki/Zinc_phosphate" },

        { "LeadII Nitrate",         "https://en.wikipedia.org/wiki/Lead(II)_nitrate" },
        { "LeadII Acetate",         "https://en.wikipedia.org/wiki/Lead(II)_acetate" },
      //{ "LeadII Chlorate",        "https://en.wikipedia.org/wiki/Lead(II)_chlorate" },
        { "LeadII Chloride",        "https://en.wikipedia.org/wiki/Lead(II)_chloride" },
        { "LeadII Bromide",         "https://en.wikipedia.org/wiki/Lead(II)_bromide" },
        { "LeadII Iodide",          "https://en.wikipedia.org/wiki/Lead(II)_iodide" },
        { "LeadII Sulfate",         "https://en.wikipedia.org/wiki/Lead(II)_sulfate" },
        { "LeadII Carbonate",       "https://en.wikipedia.org/wiki/Lead(II)_carbonate" },
        { "LeadII Chromate",        "https://en.wikipedia.org/wiki/Lead(II)_chromate" },
        { "LeadII Hydroxide",       "https://en.wikipedia.org/wiki/Lead(II)_hydroxide" },
        { "LeadII Phosphate",       "https://en.wikipedia.org/wiki/Lead(II)_phosphate" },

        { "Aluminum Nitrate",       "https://en.wikipedia.org/wiki/Aluminum_nitrate" },
        { "Aluminum Acetate",       "https://en.wikipedia.org/wiki/Aluminum_acetate" },
      //{ "Aluminum Chlorate",      "https://en.wikipedia.org/wiki/Aluminum_chlorate" },
        { "Aluminum Chloride",      "https://en.wikipedia.org/wiki/Aluminum_chloride" },
        { "Aluminum Bromide",       "https://en.wikipedia.org/wiki/Aluminum_bromide" },
        { "Aluminum Iodide",        "https://en.wikipedia.org/wiki/Aluminum_iodide" },
        { "Aluminum Sulfate",       "https://en.wikipedia.org/wiki/Aluminum_sulfate" },
        { "Aluminum Carbonate",     "https://en.wikipedia.org/wiki/Aluminum_carbonate" },
      //{ "Aluminum Chromate",      "https://en.wikipedia.org/wiki/Aluminum_chromate" },
        { "Aluminum Hydroxide",     "https://en.wikipedia.org/wiki/Aluminum_hydroxide" },
        { "Aluminum Phosphate",     "https://en.wikipedia.org/wiki/Aluminum_phosphate" }
    };

    /// <summary>
    /// Dictionary of existing ion buttons
    /// </summary>
    Dictionary<SolutionManager.IonType, GameObject> ionButtons = new Dictionary<SolutionManager.IonType, GameObject>();

    /// <summary>
    /// Dictionary of existing compound buttons
    /// </summary>
    Dictionary<string, GameObject> compoundButtons = new Dictionary<string, GameObject>();

    /// <summary>
    /// Adds a button that corresponds to the passed IonType
    /// </summary>
    /// <param name="ion"> IonType to create a button for </param>
    public void AddIonToList(SolutionManager.IonType ion)
    {
        if (ionURLs.TryGetValue(ion, out string ionURL))
        {
            GameObject newButton = Instantiate(sampleButton, gameObject.transform);
            newButton.SetActive(true);
            newButton.GetComponentInChildren<Text>().text = ionURL;
            ionButtons.Add(ion, newButton);
        }
    }

    /// <summary>
    /// Removes the button that corresponds to the passed IonType
    /// </summary>
    /// <param name="ion"> IonType to remove the button of </param>
    public void RemoveIonFromList(SolutionManager.IonType ion)
    {
        if (ionButtons.TryGetValue(ion, out GameObject button))
        {
            Destroy(button);
            ionButtons.Remove(ion);
        }
    }

    /// <summary>
    /// Adds a button that corresponds to the compound formed by the passed IonTypes
    /// </summary>
    /// <param name="cation"> IonType for the cation that forms the compound </param>
    /// <param name="anion"> IonType for the anion that forms the compound </param>
    public void AddCompoundToList(SolutionManager.IonType cation, SolutionManager.IonType anion)
    {
        string compound = cation.ToString() + " " + anion.ToString();
        if (compoundURLs.TryGetValue(compound, out string compoundURL))
        {
            GameObject newButton = Instantiate(sampleButton, gameObject.transform);
            newButton.SetActive(true);
            newButton.GetComponentInChildren<Text>().text = compoundURL;
            compoundButtons.Add(compound, newButton);
        }
    }

    /// <summary>
    /// Removes the button that corresponds to the compound formed by the passed IonTypes
    /// </summary>
    /// <param name="cation"> IonType for the cation that forms the compound </param>
    /// <param name="anion"> IonType for the anion that forms the compound </param>
    public void RemoveCompoundFromList(SolutionManager.IonType cation, SolutionManager.IonType anion)
    {
        string compound = cation.ToString() + " " + anion.ToString();
        if (compoundButtons.TryGetValue(compound, out GameObject button))
        {
            Destroy(button);
            compoundButtons.Remove(compound);
        }
    }


    public void OpenWikiPage(Text text)
    {
        Application.OpenURL(text.text);
    }

}
