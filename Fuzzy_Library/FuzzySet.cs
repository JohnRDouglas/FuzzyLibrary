using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Fuzzy_Library
{
    /* FuzzySet would have a name, like "Too Hot" or "Too Cold" and, when called up, would tell you:
     *      Shape
     *      Vertices - List <double[]> List of X, Y coordinates
     *          BaseVertices: Define the set
     *          CurrentVertices: Modified polygons taking x value into effect
     *      Membership value for a given x value
     *      x value for a given Membership (0-1)
     */
    public class FuzzySet
    {
        List<CoordPoint> _valueVertices;
        double _membershipValue;

        public FuzzySet()
        {
            Name = "";
            BaseVertices = new List<CoordPoint>();
        }

        public FuzzySet(string SetName, List<CoordPoint> SetVertices)
        {
            Name = SetName;
            BaseVertices = SetVertices;
        }

        public string Name
        {
            get;
            set;
        }

        public List<CoordPoint> BaseVertices
        {
            get;
            set;
        }

        public List<CoordPoint> ValueVertices
        {
            get { return _valueVertices; }

        }

        public double Lower_Bound
        {
            get
            {
                return BaseVertices[0].X;
            }
        }

        public double Upper_Bound
        {
            get { return BaseVertices[BaseVertices.Count - 1].X; }
        }
      
        public double MembershipOf(double x)
        {
            if (x >= BaseVertices[0].X && x <= BaseVertices[BaseVertices.Count-1].X)
            {

            }

            return 0;
        }

        // This is rough. Take the overlapping fuzzy sets, altered by the membership values, and return an X value.
        public double Defuzzification()
        {
            return 0;
        }

        public double xValueFor(double MembershipValue)
        {
            return 0;
        }

        // Return the center of the polygon provided, usually BasicVertices or ValueVertices
        public CoordPoint CenterOfPolygon(List<CoordPoint> Vertices)
        {
            CoordPoint result = new CoordPoint();

            foreach (CoordPoint p in Vertices)
            {
                result.X += p.X;
                result.Y += p.Y;
            }

            result.X /= Vertices.Count;
            result.Y /= Vertices.Count;

            return result;
        }

        public double AreaOfPolygon(List<CoordPoint> Vertices)
        {
            double result = 0;

            Vertices.Add(Vertices[0]);

            for(int i = 0; i < Vertices.Count - 1; i++)
            {
                result = result + (Vertices[i].X * Vertices[i+1].Y - Vertices[i].Y * Vertices[i+1].X);
            }

            result /= 2;

            return Math.Abs(result);
        }

        // Called when the FuzzyVariable it belongs to gets updated. Updates ValueVertices accordingly
        public void SetMembershipForX(double x)
        {
            //Console.WriteLine(Name + " " + x);
            // If x falls in the set's range, go through the set's points, find the one it lies between, and insert 
            if (x >= Lower_Bound && x <= Upper_Bound)
            {
                List<CoordPoint> newValueVertices = new List<CoordPoint>();
                CoordPoint newPoint1 = new CoordPoint();
                double y = 0;


                for (int i = 0; i < BaseVertices.Count - 1; i++ )
                {
                    CoordPoint p1 = BaseVertices[i];
                    CoordPoint p2 = BaseVertices[i + 1];

                    if (x >= p1.X && x <= p2.X)
                    {
                        y = p1.Y + ((x - p1.X) / (p2.X - p1.X)) * (p2.Y - p1.Y);
                        break;
                    }
                }
                
                // Insert a point in between all points where y is between P1 & P2.Y
                
                
                for (int i = 0; i < BaseVertices.Count - 1; i++ )
                {
                    CoordPoint p1 = BaseVertices[i];
                    CoordPoint p2 = BaseVertices[i + 1];
                    //Console.WriteLine("   (" + p1.X + ", " + p1.Y + "), (" + p2.X + ", " + p2.Y + ")");

                    //newValueVertices.Add(BaseVertices[i]);
                    if ((y >= p1.Y && y < p2.Y && p1.Y < p2.Y) || (y <= p1.Y && y >= p2.Y && p1.Y > p2.Y))
                    {
                        // Upslope: add p1 but not p2
                        // downslope: add p2 but not p1
                        if (p1.Y < p2.Y)
                            newValueVertices.Add(BaseVertices[i]);

                        newPoint1.X = p1.X + ((y - p1.Y) / (p2.Y - p1.Y)) * (p2.X - p1.X);
                        newPoint1.Y = y;
                        //Console.WriteLine("       (" + newPoint1.X + ", " + newPoint1.Y + ")");
                        newValueVertices.Add(newPoint1.Clone());

                        if (p1.Y > p2.Y)
                            newValueVertices.Add(BaseVertices[i + 1]);
                    }
                    else 
                    { 
                        newValueVertices.Add(BaseVertices[i]); 
                    }
                }

                _valueVertices = newValueVertices;

                _membershipValue = newPoint1.Y;

            }
            else
            {
                _membershipValue = 0;
                _valueVertices = new List<CoordPoint>();
            }
            
        }

        public double MembershipValue
        {
            get
            {
                return _membershipValue;
            }

        }
    }
}