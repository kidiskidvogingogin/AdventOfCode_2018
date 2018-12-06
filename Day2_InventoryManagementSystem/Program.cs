using System;
using System.Collections.Generic;

namespace Day2_InventoryManagementSystem
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
            var input = string.Empty;
            var twoCount = 0;
            var threeCount = 0;
            while ((input = Console.ReadLine()) != "end")
            {
                IDictionary<char, int> charCount = new Dictionary<char, int>();
                bool foundThree = false;
                bool foundTwo = false;
                for (int i = 0; i < input.Length; i++)
                {
                    if (!charCount.ContainsKey(input[i]))
                    {
                        charCount.Add(input[i], 1);
                    }
                    else
                    {
                        charCount[input[i]]++;
                    }
                }
                foreach (KeyValuePair<char, int> kvp in charCount)
                {
                    if (kvp.Value == 3 && !foundThree)
                    {
                        threeCount++;
                        foundThree = true;
                    }
                    else if (kvp.Value == 2 && !foundTwo)
                    {
                        twoCount++;
                        foundTwo = true;
                    }

                    if (foundThree && foundTwo)
                        break;
                }
            }
            Console.WriteLine($"The answer is: {threeCount * twoCount}");
            Console.ReadLine();
        }

        private static void Part2()
        {
            var input = string.Empty;
            var id1 = string.Empty;
            var id2 = string.Empty;
            var commonLetters = string.Empty;
            var matchFound = false;
            var idLength = 26;
            IList<string> ids = new List<string>();
            while ((input = Console.ReadLine()) != "end")
            {
                ids.Add(input);
            }
            for (int i = 0; i < ids.Count && !matchFound; i++)
            {
                for (int x = 0; x < ids.Count; x++)
                {
                    commonLetters = string.Empty;
                    if (x != i)
                    {
                        for(int j = 0; j < ids[i].Length; j++)
                        {
                            if(ids[i][j] == ids[x][j])
                            {
                                commonLetters += ids[i][j];
                            }
                            else
                            {
                                commonLetters += "_";
                            }
                        }
                        if(commonLetters.Replace("_", "").Length == idLength - 1)
                        {
                            id1 = ids[i];
                            id2 = ids[x];
                            matchFound = true;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"ID1: {id1}");
            Console.WriteLine($"ID2: {id2}");
            Console.WriteLine("_______________________________");
            Console.WriteLine($"     {commonLetters}");
            Console.WriteLine($"The answer is: {commonLetters.Replace("_", "")}");
            Console.ReadLine();
        }
    }
}
