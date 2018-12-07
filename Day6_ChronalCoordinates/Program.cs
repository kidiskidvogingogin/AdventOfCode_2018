using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6_ChronalCoordinates
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        private static void Part1()
        {
            var line = string.Empty;
            IList<Coordinate> coordinates = new List<Coordinate>();
            int minX = 0, maxX = 0, minY = 0, maxY = 0;
            while((line = Console.ReadLine()) != "end")
            {
                var coords = line.Split(", ");
                var tempCoord = new Coordinate { X = int.Parse(coords[0]), Y = int.Parse(coords[1]) };
                coordinates.Add(tempCoord);
                minX = tempCoord.X <= minX || minX == 0 ? tempCoord.X : minX;
                maxX = tempCoord.X >= maxX || maxX == 0 ? tempCoord.X : maxX;
                minY = tempCoord.Y <= minY || minY == 0 ? tempCoord.Y : minY;
                maxY = tempCoord.Y >= maxY || maxY == 0 ? tempCoord.Y : maxY;
            }

            var potentiallyFiniteCoordinates = coordinates.Where(x => x.CheckIfFinite(coordinates)).ToList();
            var finiteCoordinates = new List<Coordinate>();

            foreach(var coordinate in potentiallyFiniteCoordinates)
            {
                bool top = false, left = false, bottom = false, right = false;
                //check top from bounding edge
                for (int y = minY; y < coordinate.Y; y++)
                {
                    var tempCoord = new Coordinate { X = coordinate.X, Y = y };
                    for(int i = 0; i < coordinates.Count; i++)
                    {
                        if(tempCoord.GetDistanceFromCoordinate(coordinates[i]) < tempCoord.GetDistanceFromCoordinate(coordinate))
                        {
                            top = true;
                            y = coordinate.Y;
                        }
                    }
                }
                //check left from bounding edge
                for (int x = minX; x < coordinate.X; x++)
                {
                    var tempCoord = new Coordinate { X = x, Y = coordinate.Y };
                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        if(tempCoord.GetDistanceFromCoordinate(coordinates[i]) < tempCoord.GetDistanceFromCoordinate(coordinate))
                        {
                            left = true;
                            x = coordinate.X;
                        }
                    }
                }
                //check bottom from bounding edge
                for(int y = coordinate.Y + 1; y <= maxY; y++)
                {
                    var tempCoord = new Coordinate { X = coordinate.X, Y = y };
                    for(int i = 0; i < coordinates.Count; i++)
                    {
                        if(tempCoord.GetDistanceFromCoordinate(coordinates[i]) < tempCoord.GetDistanceFromCoordinate(coordinate))
                        {
                            bottom = true;
                            y = maxY + 1;
                        }
                    }
                }
                //check right from bounding edge
                for (int x = coordinate.X + 1; x <= maxX; x++)
                {
                    var tempCoord = new Coordinate { X = x, Y = coordinate.Y };
                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        if(tempCoord.GetDistanceFromCoordinate(coordinates[i]) < tempCoord.GetDistanceFromCoordinate(coordinate))
                        {
                            right = true;
                            x = maxX + 1;
                        }
                    }
                }
                if (top && left && bottom && right)
                    finiteCoordinates.Add(coordinate);
            }

            //var finiteCoordinates = coordinates.Where(x => x.CheckIfFinite(coordinates)).ToList();
            var infiniteCoordinates = coordinates.Where(x => !finiteCoordinates.Contains(x)).ToList();

            Console.WriteLine($"There are {finiteCoordinates.Count} finite coordinates");
            Console.WriteLine($"There are {infiniteCoordinates.Count} infinite coordinates");

            foreach (var coord in finiteCoordinates)
            {
                //Console.WriteLine($"[{coord.X},{coord.Y}]");
                //Console.WriteLine($"Top-Left Coord is:     [{topLeft.X},{topLeft.Y}]");
                //Console.WriteLine($"Bottom-Left Coord is:  [{bottomLeft.X},{bottomLeft.Y}]");
                //Console.WriteLine($"Bottom-Right Coord is: [{bottomRight.X},{bottomRight.Y}]");
                //Console.WriteLine($"Top-Right Coord is:    [{topRight.X},{topRight.Y}]");

                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        if(coord.X == x && coord.Y == y)
                        {
                            coord.UnboundArea++;
                        }
                        else
                        {
                            var currentCoordinate = new Coordinate { X = x, Y = y };
                            var coordDistance = coord.GetDistanceFromCoordinate(currentCoordinate);
                            var areaCounts = true;
                            foreach (var coordinate in coordinates)
                            {
                                if (coordinate.X != coord.X || coordinate.Y != coord.Y)
                                {
                                    var tempCoordDistance = coordinate.GetDistanceFromCoordinate(currentCoordinate);
                                    if (coordinate.GetDistanceFromCoordinate(currentCoordinate) <= coordDistance)
                                    {
                                        areaCounts = false;
                                        break;
                                    }
                                }
                            }
                            if (areaCounts)
                            {
                                coord.UnboundArea++;
                                //Console.WriteLine($"[{currentCoordinate.X},{currentCoordinate.Y}]");
                            }
                        }
                    }
                }
                //Console.WriteLine($"Unbound Area: {coord.UnboundArea}");
            }
            finiteCoordinates.Sort((x, y) => y.UnboundArea.CompareTo(x.UnboundArea));
            Console.WriteLine($"The answer is: {finiteCoordinates[0].UnboundArea}");
            Console.ReadLine();
        }

        private static void Part2()
        {
            var line = string.Empty;
            IList<Coordinate> coordinates = new List<Coordinate>();
            int minX = 0, maxX = 0, minY = 0, maxY = 0;
            while ((line = Console.ReadLine()) != "end")
            {
                var coords = line.Split(", ");
                var tempCoord = new Coordinate { X = int.Parse(coords[0]), Y = int.Parse(coords[1]) };
                coordinates.Add(tempCoord);
                minX = tempCoord.X <= minX || minX == 0 ? tempCoord.X : minX;
                maxX = tempCoord.X >= maxX || maxX == 0 ? tempCoord.X : maxX;
                minY = tempCoord.Y <= minY || minY == 0 ? tempCoord.Y : minY;
                maxY = tempCoord.Y >= maxY || maxY == 0 ? tempCoord.Y : maxY;
            }

            var regionSize = 0;
            for(int x = minX; x < maxX; x++)
            {
                for(int y = minY; y < maxY; y++)
                {
                    var totalDistance = 0;
                    var tempCoord = new Coordinate { X = x, Y = y };
                    foreach (var coordinate in coordinates)
                    {
                        totalDistance += coordinate.GetDistanceFromCoordinate(tempCoord);
                        if (totalDistance >= 10000)
                            break;
                    }
                    if (totalDistance < 10000)
                        regionSize++;
                }
            }
            Console.WriteLine($"The answer is: {regionSize}");
            Console.ReadLine();
        }
    }
}
