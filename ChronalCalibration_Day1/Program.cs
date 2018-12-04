﻿using System;
using System.Collections.Generic;

namespace ChronalCalibration_Day1
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
            while((input = Console.ReadLine()) != "end")
            {
                inputs.Add(Convert.ToInt32(input));
            }
            int frequency = 0;
            int index = 0;
            int loopCount = 1;
            bool firstIteraction = true;
            while(!frequencies.Contains(frequency))
            {
                if (firstIteraction)
                {
                    frequencies.Add(0);
                    firstIteraction = false;
                }
                else
                {
                    frequencies.Add(frequency);
                }

                frequency += inputs[index];
                index++;
                if(index == inputs.Count)
                    index = 0;

                loopCount++;
            }
            Console.WriteLine($"The answer is: {frequency}");
            Console.ReadLine();
        }
    }
}