using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day9_MarbleMania
{
    class Program
    {
        static DoublyLinkedList<int> marbles;
        static IList<long> players;
        static int maxMarbleValue;
        static int participantCount;
        static void Main(string[] args)
        {
            marbles = new DoublyLinkedList<int>();
            ReadInput();
            players = new List<long>(participantCount);
            for (int i = 0; i < participantCount; i++)
                players.Add(0);
            //Part1();
            Part2();
            Console.ReadLine();
        }

        private static void ReadInput()
        {
            var numberPattern = @"[0-9]+";
            var inputString = Console.ReadLine();
            var inputs = inputString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var maxMarble = Regex.Match(inputs[1], numberPattern).Value;
            var participants = Regex.Match(inputs[0], numberPattern).Value;
            maxMarbleValue = Convert.ToInt32(maxMarble);
            participantCount = Convert.ToInt32(participants);
        }

        private static void Part1()
        {
            var marbleValue = 0;
            var currentMarble = 0;
            var currentPlayerIndex = 0;
            marbles.AddFirst(0);
            while(marbleValue < maxMarbleValue)
            {
                marbleValue++;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                currentMarble = PlayTurn(currentPlayerIndex, currentMarble, marbleValue);
                //Console.WriteLine(marbles.AllNodesToString());
            }
            //for (int i = 0; i < players.Count; i++)
            //    Console.WriteLine($"Player {i + 1}: {players[i]}");
            ((List<long>)players).Sort((x, y) => y.CompareTo(x));
            Console.WriteLine($"The answer is: {players[0]}");
        }

        private static int PlayTurn(int currentPlayer, int currentMarble, int marbleToPlay)
        {
            var newValue = currentMarble;
            if (marbleToPlay % 23 == 0)
            {
                players[currentPlayer] += marbleToPlay;
                players[currentPlayer] += marbles.RemoveNodeAndSetHead(-7);
                newValue = marbles.GetNodeXAway(-7);
            }
            else
            {
                marbles.AddNodeXAway(1, marbleToPlay);
                newValue = marbleToPlay;
            }
            return newValue;
        }

        private static void Part2()
        {
            maxMarbleValue *= 100; //Part2 asks what the value would be if the last marble were 100 times larger
            var marbleValue = 0;
            var currentMarble = 0;
            var currentPlayerIndex = 0;
            marbles.AddFirst(0);
            while (marbleValue < maxMarbleValue)
            {
                marbleValue++;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                currentMarble = PlayTurn(currentPlayerIndex, currentMarble, marbleValue);
            }
            ((List<long>)players).Sort((x, y) => y.CompareTo(x));
            Console.WriteLine($"The answer is: {players[0]}");
        }
    }
}
