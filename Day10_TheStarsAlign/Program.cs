using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day10_TheStarsAlign
{
    class Program
    {
        static IList<Star> stars;
        static void Main(string[] args)
        {
            PopulateStars();
            //Part1();
            Part2();
            Console.ReadKey();
        }

        private static void PopulateStars()
        {
            stars = new List<Star>();
            var input = string.Empty;
            var pattern = @"<\s*.?\d+,\s+.?\d+>";
            while((input = Console.ReadLine()) != "end")
            {
                var matches = Regex.Matches(input, pattern);
                var coordinates = matches[0].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var velocities = matches[1].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                var xCoord = Convert.ToInt32(Regex.Match(coordinates[0], @"-?\d+").Value);
                var yCoord = Convert.ToInt32(Regex.Match(coordinates[1], @"-?\d+").Value);
                var xVeloc = Convert.ToInt32(Regex.Match(velocities[0], @"-?\d+").Value);
                var yVeloc = Convert.ToInt32(Regex.Match(velocities[1], @"-?\d+").Value);

                var star = new Star(xCoord, yCoord)
                {
                    XVelocity = xVeloc,
                    YVelocity = yVeloc
                };
                stars.Add(star);
            }
        }

        private static void Part1()
        {
            int maxX = int.MinValue, maxY = int.MinValue, minX = int.MaxValue, minY = int.MaxValue;
            foreach(var star in stars)
            {
                if (star.X > maxX)
                    maxX = star.X;
                if (star.Y > maxY)
                    maxY = star.Y;
                if (star.X < minX)
                    minX = star.X;
                if (star.Y < minY)
                    minY = star.Y;
            }

            var previousDistance = maxY - minY;
            var growth = (maxY - minY) - previousDistance;

            while (growth <= 0)
            {
                previousDistance = maxY - minY;
                minX = int.MaxValue;
                minY = int.MaxValue;
                maxX = int.MinValue;
                maxY = int.MinValue;
                foreach (var star in stars)
                {
                    star.MoveStar();
                    if (star.X > maxX)
                        maxX = star.X;
                    if (star.Y > maxY)
                        maxY = star.Y;
                    if (star.X < minX)
                        minX = star.X;
                    if (star.Y < minY)
                        minY = star.Y;
                }
                growth = (maxY - minY) - previousDistance;
            }

            minX = int.MaxValue;
            minY = int.MaxValue;
            maxX = int.MinValue;
            maxY = int.MinValue;
            foreach (var star in stars)
            {
                star.RevertMove();
                if (star.X > maxX)
                    maxX = star.X;
                if (star.Y > maxY)
                    maxY = star.Y;
                if (star.X < minX)
                    minX = star.X;
                if (star.Y < minY)
                    minY = star.Y;
            }
            PrintBoard(minX, minY, maxX, maxY);
            WriteBoard(minX, minY, maxX, maxY);
        }

        private static void PrintBoard(int minX, int minY, int maxX, int maxY)
        {
            using (var streamWriter = new StreamWriter(@"Day10_Output.txt"))
            {
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        var matched = false;
                        foreach (var star in stars)
                            if (star.X == x && star.Y == y)
                            {
                                streamWriter.Write(" # ");
                                matched = true;
                                break;
                            }
                        if (!matched)
                            streamWriter.Write(" . ");
                    }
                    streamWriter.WriteLine();
                }
            }
        }

        private static void WriteBoard(int minX, int minY, int maxX, int maxY)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var matched = false;
                    foreach (var star in stars)
                        if (star.X == x && star.Y == y)
                        {
                            Console.Write(" # ");
                            matched = true;
                            break;
                        }
                    if (!matched)
                        Console.Write(" . ");
                }
                Console.WriteLine();
            }
        }

        private static void Part2()
        {
            int maxX = int.MinValue, maxY = int.MinValue, minX = int.MaxValue, minY = int.MaxValue;
            foreach (var star in stars)
            {
                if (star.X > maxX)
                    maxX = star.X;
                if (star.Y > maxY)
                    maxY = star.Y;
                if (star.X < minX)
                    minX = star.X;
                if (star.Y < minY)
                    minY = star.Y;
            }

            var previousDistance = maxY - minY;
            var growth = (maxY - minY) - previousDistance;

            var seconds = 0;
            while (growth <= 0)
            {
                previousDistance = maxY - minY;
                minX = int.MaxValue;
                minY = int.MaxValue;
                maxX = int.MinValue;
                maxY = int.MinValue;
                foreach (var star in stars)
                {
                    star.MoveStar();
                    if (star.X > maxX)
                        maxX = star.X;
                    if (star.Y > maxY)
                        maxY = star.Y;
                    if (star.X < minX)
                        minX = star.X;
                    if (star.Y < minY)
                        minY = star.Y;
                }
                growth = (maxY - minY) - previousDistance;
                seconds++;
            }
            seconds--;
            Console.WriteLine($"The answer is: {seconds}");
        }
    }
}
