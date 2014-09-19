using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Fuzzy_Library
{
    /* FuzzyVariable has a name, like "Temperature" or "Distance" or "Speed", and, when called up, would tell you:
     * Range - eg, 0.0 - 1.0
     */
    public class FuzzyVariable
    {
        static int VariableCount = 0;
        double currentValue;

        public FuzzyVariable()
        {
            Name = "FuzzyVariable" + VariableCount;
            VariableCount++;
            Range = new double[2] { 0.0, 1.0 };
            FuzzySets = new List<FuzzySet>();
            Value = 0.5;
        }

        public FuzzyVariable(string VariableName, double[] VariableRange, ref List<FuzzySet> VariableSets)
        {
            Name = VariableName;
            Range = VariableRange;
            FuzzySets = VariableSets;
            Value = (VariableRange[0] + VariableRange[1]) * 0.5;
        }

        public string Name
        {
            get;
            set;
        }

        // Range of expected values, defaults 0.0 to 1.0
        double[] Range
        {
            get;
            set;
        }

        public List<FuzzySet> FuzzySets
        {
            get;
            set;
        }

        public double Value
        {
            get
            {
                return currentValue;
            }

            set
            {
                currentValue = value;

                for (int i = 0; i < FuzzySets.Count; i++)
                {
                    FuzzySet fs = FuzzySets[i];
                    fs.SetMembershipForX(value);
                }
            }
        }

        public FuzzySet GetSetByName(string SetName)
        {
            foreach (FuzzySet fs in FuzzySets)
            {
                if (fs.Name == SetName)
                {

                    return fs; // Does this return the object or a copy?
                }
            }

            return null;
        }


    }
}
