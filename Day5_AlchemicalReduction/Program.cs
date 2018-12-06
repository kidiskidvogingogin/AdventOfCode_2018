using System;

namespace Day5_AlchemicalReduction
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
            string input = System.IO.File.ReadAllText(@"TestInput.txt").Trim();
            //string input = Console.ReadLine().Trim();
            //Console.WriteLine(Part1ReactionRecursive(0, input));
            Console.WriteLine($"The answer is: {Part1Reaction(input).Length}");
            Console.ReadLine();
        }

        private static void Part2()
        {
            string input = System.IO.File.ReadAllText(@"TestInput.txt").Trim();
            int shortestSequence = int.MaxValue;
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for(int i = 0; i < 26; i++)
            {
                char capitalLetter = alphabet[i];
                char lowercaseLetter = Convert.ToChar(alphabet[i] + 32);
                var newInput = input.Replace(capitalLetter.ToString(), "").Replace(lowercaseLetter.ToString(), "");
                var reactionResult = Part1Reaction(newInput);
                if (reactionResult.Length < shortestSequence)
                    shortestSequence = reactionResult.Length;
            }
            Console.WriteLine($"The answer is: {shortestSequence}");
            Console.ReadLine();
        }

        private static string Part1Reaction(string polymer)
        {
            var startIndex = 0;
            while (startIndex + 1 < polymer.Length)
            {
                if ((IsUpperCase(polymer[startIndex]) && (polymer[startIndex] + 32) == polymer[startIndex + 1])
                    || (!IsUpperCase(polymer[startIndex]) && polymer[startIndex] == (polymer[startIndex + 1] + 32)))
                {
                    polymer = polymer.Substring(0, startIndex) + polymer.Substring(startIndex + 2);
                    if (startIndex - 1 < 0)
                        startIndex = 0;
                    else
                        startIndex -= 1;
                }
                else
                {
                    startIndex = startIndex + 1;
                }
            }
            return polymer;
        }

        private static string Part1ReactionRecursive(int startIndex, string polymer)
        {
            if (startIndex + 1 < polymer.Length)
            {
                if ((IsUpperCase(polymer[startIndex]) && (polymer[startIndex] + 32) == polymer[startIndex + 1])
                    || (!IsUpperCase(polymer[startIndex]) && polymer[startIndex] == (polymer[startIndex + 1] + 32)))
                {
                    polymer = polymer.Substring(0, startIndex) + polymer.Substring(startIndex + 2);
                    if (startIndex - 1 < 0)
                        startIndex = 0;
                    else
                        startIndex -= 1;
                    return Part1ReactionRecursive(startIndex, polymer);
                }
                else
                {
                    startIndex = startIndex + 1;
                    return Part1ReactionRecursive(startIndex, polymer);
                }
            }
            else
            {
                return polymer;
            }
        }

        private static bool IsUpperCase(char c)
        {
            return c >= 65 && c <= 90;
        }
    }
}
