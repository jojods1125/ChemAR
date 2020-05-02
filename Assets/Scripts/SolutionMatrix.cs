using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Readable data for all ions and compounds
/// Author: Joseph Dasilva
/// </summary>
static class SolutionMatrix
{
    /// <summary>
    /// Cation ions, known for having a positive charge
    /// </summary>
    public struct Cation
    {
        /// <summary>
        /// Name of Cation
        /// </summary>
        public string name;

        /// <summary>
        /// Index the Cation appears at in the list of compounds in Anions
        /// </summary>
        public int index;

        /// <summary>
        /// Array of ints referring to the Anions that form compounds with this Cation,
        /// <br> where 0 means soluble, 1 means insoluble, and 2 means exception </br>
        /// </summary>
        public int[] compounds;

        /// <summary>
        /// Number of points the ion is worth while in the solution
        /// </summary>
        public int aqueousPts;

        /// <summary>
        /// Number of points the ion is worth as a precipitate
        /// </summary>
        public int precipitatePts;
    }


    /// <summary>
    /// Anion ions, known for having a negative charge
    /// </summary>
    public struct Anion
    {
        /// <summary>
        /// Name of Anion
        /// </summary>
        public string name;

        /// <summary>
        /// Index the Anion appears at in the list of compounds in Cations
        /// </summary>
        public int index;

        /// <summary>
        /// Array of ints referring to the Cations that form compounds with this Anion,
        /// <br> where 0 means soluble, 1 means insoluble, and 2 means exception </br>
        /// </summary>
        public int[] compounds;

        /// <summary>
        /// Number of points the ion is worth while in the solution
        /// </summary>
        public int aqueousPts;

        /// <summary>
        /// Number of points the ion is worth as a precipitate
        /// </summary>
        public int precipitatePts;
    }

    

    static Cation Ammonium = new Cation()
    {
        name = "Ammonium",
        index = 0,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        aqueousPts = 4,
        precipitatePts = 0
    };

    static Cation Sodium = new Cation()
    {
        name = "Sodium",
        index = 1,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        aqueousPts = 2,
        precipitatePts = 0
    };

    static Cation Potassium = new Cation()
    {
        name = "Potassium",
        index = 2,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        aqueousPts = 2,
        precipitatePts = 0
    };

    static Cation Lithium = new Cation()
    {
        name = "Lithium",
        index = 3,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        aqueousPts = 4,
        precipitatePts = 10
    };

    static Cation Magnesium = new Cation()
    {
        name = "Magnesium",
        index = 4,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1 },
        aqueousPts = 12,
        precipitatePts = 6
    };

    static Cation Calcium = new Cation()
    {
        name = "Calcium",
        index = 5,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 2, 1, 0, 2, 1 },
        aqueousPts = 16,
        precipitatePts = 8
    };

    static Cation Strontium = new Cation()
    {
        name = "Strontium",
        index = 6,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1 },
        aqueousPts = 16,
        precipitatePts = 4
    };

    static Cation Barium = new Cation()
    {
        name = "Barium",
        index = 7,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1 },
        aqueousPts = 16,
        precipitatePts = 4
    };

    static Cation IronIII = new Cation()
    {
        name = "Iron(III)",
        index = 8,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
        aqueousPts = 12,
        precipitatePts = 6
    };

    static Cation CopperII = new Cation()
    {
        name = "Copper(II)",
        index = 9,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
        aqueousPts = 16,
        precipitatePts = 4
    };

    static Cation Silver = new Cation()
    {
        name = "Silver",
        index = 10,
        compounds = new int[11] { 0, 0, 0, 1, 1, 1, 2, 2, 1, 2, 1 },
        aqueousPts = 28,
        precipitatePts = 2
    };

    static Cation Zinc = new Cation()
    {
        name = "Zinc",
        index = 11,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
        aqueousPts = 16,
        precipitatePts = 5
    };

    static Cation LeadII = new Cation()
    {
        name = "Lead(II)",
        index = 12,
        compounds = new int[11] { 0, 0, 0, 0, 2, 1, 1, 1, 1, 1, 1 },
        aqueousPts = 24,
        precipitatePts = 2
    };

    static Cation Aluminum = new Cation()
    {
        name = "Aluminum",
        index = 13,
        compounds = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
        aqueousPts = 8,
        precipitatePts = 8
    };




    static Anion Nitrate = new Anion()
    {
        name = "Nitrate",
        index = 0,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        aqueousPts = 2,
        precipitatePts = 0
    };

    static Anion Acetate = new Anion()
    {
        name = "Acetate",
        index = 1,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        aqueousPts = 2,
        precipitatePts = 0
    };

    static Anion Chlorate = new Anion()
    {
        name = "Chlorate",
        index = 2,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        aqueousPts = 2,
        precipitatePts = 0
    };

    static Anion Chloride = new Anion()
    {
        name = "Chloride",
        index = 3,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
        aqueousPts = 4,
        precipitatePts = 10
    };

    static Anion Bromide = new Anion()
    {
        name = "Bromide",
        index = 4,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 2, 0 },
        aqueousPts = 8,
        precipitatePts = 10
    };

    static Anion Iodide = new Anion()
    {
        name = "Iodide",
        index = 5,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
        aqueousPts = 8,
        precipitatePts = 8
    };

    static Anion Sulfate = new Anion()
    {
        name = "Sulfate",
        index = 6,
        compounds = new int[14] { 0, 0, 0, 0, 0, 2, 1, 1, 0, 0, 2, 0, 1, 0 },
        aqueousPts = 16,
        precipitatePts = 8
    };

    static Anion Carbonate = new Anion()
    {
        name = "Carbonate",
        index = 7,
        compounds = new int[14] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 2, 1, 1, 0 },
        aqueousPts = 24,
        precipitatePts = 4
    };

    static Anion Chromate = new Anion()
    {
        name = "Chromate",
        index = 8,
        compounds = new int[14] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 },
        aqueousPts = 28,
        precipitatePts = 4
    };

    static Anion Hydroxide = new Anion()
    {
        name = "Hydroxide",
        index = 9,
        compounds = new int[14] { 2, 0, 0, 0, 1, 2, 0, 0, 1, 1, 2, 1, 1, 1 },
        aqueousPts = 28,
        precipitatePts = 6
    };

    static Anion Phosphate = new Anion()
    {
        name = "Phosphate",
        index = 10,
        compounds = new int[14] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        aqueousPts = 32,
        precipitatePts = 2
    };

    /// <summary>
    /// Appropriately ordered list of Cations
    /// </summary>
    public static Cation[] cations = new Cation[14] { Ammonium, Sodium, Potassium, Lithium, Magnesium, Calcium, Strontium, Barium, IronIII, CopperII, Silver, Zinc, LeadII, Aluminum };

    /// <summary>
    /// Appropriately ordered list of Anions
    /// </summary>
    public static Anion[] anions = new Anion[11] { Nitrate, Acetate, Chlorate, Chloride, Bromide, Iodide, Sulfate, Carbonate, Chromate, Hydroxide, Phosphate };

}