using System.Collections.Generic;
using System.Text;

namespace CSharpVersion
{
    public class Vector
    {
        public Vector()
        {
            
        }
        public Vector(int [] vector)
        {
            Coordinates = new List<int>(vector);
        }
        public List<int> Coordinates { get; set; } = new List<int>();

        public int this[int i]
        {
            get { return Coordinates[i]; }
            set { Coordinates[i] = value; }
        } 

        public Vector Copy()
        {
            var vector = new Vector()
            {
                Coordinates = new List<int>(Coordinates.Capacity)
            };
            for (int i = 0; i < Coordinates.Count; i++)
            {
                vector.Coordinates.Add(Coordinates[i]);
            }
            return vector;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var coordinate in Coordinates)
            {
                sb.Append(coordinate);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}