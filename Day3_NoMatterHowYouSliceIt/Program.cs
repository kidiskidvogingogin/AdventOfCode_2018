using System;
using System.Collections.Generic;

namespace Day3_NoMatterHowYouSliceIt
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
            string input = string.Empty;
            int tempIndex;

            IList<Fabric> claims = new List<Fabric>();
            ISet<Coordinate> usedCoordinates = new HashSet<Coordinate>(new CoordinateComparer());
            while ((input = Console.ReadLine()) != "end")
            {
                tempIndex = input.IndexOf("@");
                claims.Add(Fabric.Factory(input.Substring(tempIndex + 2)));
            }

            for(int j = 0; j < claims.Count; j++)
                for(int k = 0; k < claims.Count; k++)
                    if(k != j)
                    {
                        claims[j].CheckClaimOverlaps(claims[k], usedCoordinates);
                    }

            Console.WriteLine($"The answer is: {usedCoordinates.Count}");
            Console.ReadLine();
        }

        private static void Part2()
        {
            string input = string.Empty;
            string id = string.Empty;
            int tempIndex;

            IList<Fabric> claims = new List<Fabric>();
            ISet<string> invalidClaimIDs = new HashSet<string>();

            while ((input = Console.ReadLine()) != "end")
            {
                tempIndex = input.IndexOf("@");
                id = input.Substring(0, tempIndex);
                claims.Add(Fabric.Factory(input.Substring(tempIndex + 2), id));
            }

            for (int j = 0; j < claims.Count; j++)
                for (int k = 0; k < claims.Count; k++)
                    if (j != k && claims[j].CheckForOverlap(claims[k]))
                        invalidClaimIDs.Add(claims[j].ID);

            for (int i = 0; i < claims.Count; i++)
                if(!invalidClaimIDs.Contains(claims[i].ID))
                    Console.WriteLine(claims[i].ID);
            Console.ReadLine();
        }
    }
}
