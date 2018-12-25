using System;
using System.Collections.Generic;

namespace Day8_MemoryManeuver
{
    class Program
    {
        private static string[] entries;
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"TestInput.txt").Trim();
            //string input = Console.ReadLine();
            entries = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            //Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            var rootNode = PopulateTree();
            var metadataTotal = TraverseTree(rootNode);
            Console.WriteLine($"The answer is {metadataTotal}.");
        }

        private static void Part2()
        {
            var rootNode = PopulateTree();
            Console.WriteLine($"The answer is {rootNode.GetValue()}.");
        }

        private static int TraverseTree(Node node)
        {
            var metaTotal = 0;
            foreach (var metadata in node.Metadata)
                metaTotal += metadata;
            for (int i = 0; i < node.ChildNodesCount; i++)
            {
                metaTotal += TraverseTree(node.Children[i]);
            }
            return metaTotal;
        }
        private static Node PopulateTree()
        {
            var nodes = new List<Node>();
            var entriesQueue = new Queue<string>(entries);

            var root = new Node
            {
                ChildNodesCount = Convert.ToInt32(entriesQueue.Dequeue()),
                MetadataCount = Convert.ToInt32(entriesQueue.Dequeue())
            };

            var tree = PopulateChildren(root.ChildNodesCount, root, entriesQueue);
            return tree;
        }

        private static Node PopulateChildren(int childrenToProcess, Node currentParent, Queue<string> inputs)
        {
            for (int i = currentParent.ChildNodesCount - 1; i >= 0; i--)
            {
                var childNode = new Node
                {
                    ChildNodesCount = Convert.ToInt32(inputs.Dequeue()),
                    MetadataCount = Convert.ToInt32(inputs.Dequeue())
                };
                currentParent.Children.Add(PopulateChildren(currentParent.ChildNodesCount, childNode, inputs));
            }
            for (int x = 0; x < currentParent.MetadataCount; x++)
                currentParent.Metadata.Add(Convert.ToInt32(inputs.Dequeue()));
            return currentParent;
        }
    }
}
