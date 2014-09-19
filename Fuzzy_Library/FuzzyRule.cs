using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy_Library
{
    public class RuleParam
    {
        public FuzzyVariable ParamVariable;
        public FuzzySet ParamSet;

        public RuleParam(FuzzyVariable Variable, string SetName, double DecimalValue = 0)
        {
            ParamVariable = Variable;
            ParamSet = Variable.GetSetByName(SetName);
            this.DecimalValue = DecimalValue;
        }

        public double DecimalValue
        {
            get;
            set;
        }
    }

    public class FuzzyRule
    {
        public List<RuleParam> PrecedentParams = new List<RuleParam>();
        public List<RuleParam> AntecedentParams = new List<RuleParam>();

        public FuzzyRule()
        {

        }

        public FuzzyRule(List<RuleParam> Precedents, List<RuleParam> Antecedents)
        {
         
            PrecedentParams = Precedents;
            AntecedentParams = Antecedents;
        }

        
        // Returns firing strength (Zadeh's AND)
        public double Fire()
        {
            double result = double.MaxValue;
            foreach (RuleParam param in PrecedentParams)
            {
                
                result = Math.Min(result, param.ParamSet.MembershipValue);
                //Console.WriteLine("Firing for " + param.ParamVariable.Name + "-" + param.ParamSet.Name + ": " + result);              
  
            }

            return result;

        }
    }

}
