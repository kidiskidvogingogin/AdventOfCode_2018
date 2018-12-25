using System;
using System.Collections.Generic;
using System.Text;

namespace Day8_MemoryManeuver
{
    class Node
    {
        public int ChildNodesCount { get; set; }
        public int MetadataCount { get; set; }
        public List<Node> Children { get; set; }
        public List<int> Metadata { get; set; }

        public Node()
        {
            ChildNodesCount = 0;
            MetadataCount = 0;
            Children = new List<Node>();
            Metadata = new List<int>();
        }

        public int GetValue()
        {
            var value = 0;
            foreach (var metaIndex in Metadata)
            {
                if (ChildNodesCount == 0)
                    value += metaIndex;
                else if (metaIndex <= ChildNodesCount)
                    value += Children[metaIndex - 1].GetValue();
            }
            return value;
        }
    }
}
