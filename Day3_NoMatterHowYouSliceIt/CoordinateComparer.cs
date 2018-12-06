using System;
using System.Collections.Generic;
using System.Text;

namespace Day3_NoMatterHowYouSliceIt
{
    class CoordinateComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate x, Coordinate y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Coordinate obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
