using System;
using System.Collections.Generic;

namespace Day1_ChronalCalibration
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
            int chron = 0;
            while ((input = Console.ReadLine()) != "end")
            {
                chron += Convert.ToInt32(input);
            }
            Console.WriteLine($"The answer is: {chron}");
            Console.ReadLine();
        }

        private static void Part2()
        {
            ISet<int> frequencies = new HashSet<int>();
            List<int> inputs = new List<int>();
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "end")
                inputs.Add(Convert.ToInt32(input));

            int frequency = 0, index = 0;
            while (frequencies.Add(frequency))
            {
                frequency += inputs[index % inputs.Count];
                index++;
            }
            Console.WriteLine($"The answer is: {frequency}");
            Console.ReadLine();
        }
    }
}
