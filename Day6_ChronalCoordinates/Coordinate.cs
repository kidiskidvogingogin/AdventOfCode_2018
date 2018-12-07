using System;
using System.Collections.Generic;
using System.Text;

namespace Day6_ChronalCoordinates
{
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int UnboundArea { get; set; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public int GetDistanceFromCoordinate(Coordinate coord)
        {
            return Math.Abs(X - coord.X) + Math.Abs(Y - coord.Y);
        }

        public bool CheckIfFinite(IList<Coordinate> allCoordinates)
        {
            bool top = false;
            bool left = false;
            bool bottom = false;
            bool right = false;
            foreach (Coordinate coord in allCoordinates)
            {
                if(X != coord.X && Y != coord.Y)
                {
                    top = !top ? coord.Y <= Y : top;
                    left = !left ? coord.X <= X : top;
                    bottom = !bottom ? coord.Y >= Y : bottom;
                    right = !right ? coord.X >= X : right;
                }
            }
            return top && left && bottom && right;
        }

        public Coordinate FindClosestMinXMinY(IList<Coordinate> coordinates)
        {
            int minDistance = -1;
            Coordinate retVal = null;
            foreach (var coord in coordinates)
            {
                if (coord.X < X && coord.Y < Y)
                {
                    int distance = GetDistanceFromCoordinate(coord);
                    if (distance <= minDistance || minDistance == -1)
                    {
                        minDistance = distance;
                        retVal = coord;
                    }
                }
            }
            return retVal;
        }

        public Coordinate FindClosestMinXMaxY(IList<Coordinate> coordinates)
        {
            int minDistance = -1;
            Coordinate retVal = null;
            foreach(var coord in coordinates)
            {
                if(coord.X < X && coord.Y > Y)
                {
                    int distance = GetDistanceFromCoordinate(coord);
                    if(distance <= minDistance || minDistance == -1)
                    {
                        minDistance = distance;
                        retVal = coord;
                    }
                }
            }
            return retVal;
        }

        public Coordinate FindClosestMaxXMinY(IList<Coordinate> coordinates)
        {
            int minDistance = -1;
            Coordinate retVal = null;
            foreach(var coord in coordinates)
            {
                if(coord.X > X && coord.Y < Y)
                {
                    int distance = GetDistanceFromCoordinate(coord);
                    if(distance <= minDistance || minDistance == -1)
                    {
                        minDistance = distance;
                        retVal = coord;
                    }
                }
            }
            return retVal;
        }

        public Coordinate FindClosestMaxXMaxY(IList<Coordinate> coordinates)
        {
            int minDistance = -1;
            Coordinate retVal = null;
            foreach(var coord in coordinates)
            {
                if(coord.X > X && coord.Y > Y)
                {
                    int distance = GetDistanceFromCoordinate(coord);
                    if(distance <= minDistance || minDistance == -1)
                    {
                        minDistance = distance;
                        retVal = coord;
                    }
                }
            }
            return retVal;
        }
    }
}
