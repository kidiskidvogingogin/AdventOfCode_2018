using System;
using System.Collections.Generic;
using System.Text;

namespace Day7_TheSumOfItsParts
{
    class Worker
    {
        public char NodeToProcess { get; set; }
        public int SecondsUntilAvailable { get; set; }
        public bool Available { get => SecondsUntilAvailable <= 0; }

        public void DecrementTimeRemaing(int timeToRemove)
        {
            SecondsUntilAvailable -= timeToRemove;
        }

        public Worker SetUp(InstructionNode node)
        {
            NodeToProcess = node.Parent;
            SecondsUntilAvailable = node.Time;

            return this;
        }
    }
}
