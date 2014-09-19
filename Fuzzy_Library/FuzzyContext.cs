using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Fuzzy_Library
{
    // This is simply a double version of Point used for coordinating the polygons for the fuzzy sets
    public class CoordPoint
    {
        double _x = 0.0;
        double _y = 0.0;

        public CoordPoint()
        {
            _x = 0;
            _y = 0;
        }
        public CoordPoint(double X, double Y)
        {
            _x = X;
            _y = Y;
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public CoordPoint Clone()
        {
            return new CoordPoint(_x, _y);
        }
    }
        
    /// <summary>
    /// FuzzyContext
    /// All-encompassing class which will manage and take ultimate responsibility for the rest of the objects.
    /// 
    /// Properties:
    ///     AllVariables contains every FuzzyVariable object
    ///     AllRules contains every FuzzyRule object
    ///     
    /// Methods:
    ///     FireRule creates a list of all the Variable-Set pairs and assigns a list of double to them, which will be used to tabulate the values from the rules
    ///         in order to calculate the fuzzy AND and OR functions (Min & Max per Zadeh's method)
    ///
    /// 2014-09-18
    ///     Initial entry. JRD
    /// </summary>
    public class FuzzyContext
    {
        public List<FuzzyVariable> AllVariables = new List<FuzzyVariable>();

        List<FuzzySet> AllSets = new List<FuzzySet>();
        
        public List<FuzzyRule> AllRules = new List<FuzzyRule>();

        // Fires the rules in parallel, modifying FuzzyVariables & their FuzzySets' membership values
        public void FireRules()
        {

            Console.WriteLine("Creating Rules Hashtable.");
            Hashtable rulesHash = new Hashtable();

            foreach (FuzzyVariable fv in AllVariables)
            {
                foreach (FuzzySet fs in fv.FuzzySets)
                {
                    rulesHash.Add(fv.Name + "-" + fs.Name, new List<double>());
                    Console.Write(fv.Name + "-" + fs.Name + ": ");
                    foreach (CoordPoint cp in fs.BaseVertices)
                    {
                        Console.Write("(" + cp.X + ", " + cp.Y + ") ");
                    }
                    Console.WriteLine();
                }

            }


            Console.WriteLine("Updating Rules Hashtable with firing results for {0} rules.", AllRules.Count);

            for (int i = 0; i < AllRules.Count; i++)
            {
                double result = AllRules[i].Fire();

                for (int j = 0; j < AllRules[i].AntecedentParams.Count; j++)
                {
                    string hashkey = AllRules[i].AntecedentParams[j].ParamVariable.Name + "-" + AllRules[i].AntecedentParams[j].ParamSet.Name;
                    //((List<double>)rulesHash[hashkey]).Add(result);

                    Console.WriteLine(hashkey + ": " + result);
                }

            }
               
        }
        

    }

    
}
