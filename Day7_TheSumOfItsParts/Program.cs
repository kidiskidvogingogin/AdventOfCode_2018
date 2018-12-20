using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7_TheSumOfItsParts
{
    class Program
    {
        private static IDictionary<char, InstructionNode> nodes;
        private const int NUMBER_OF_WORKERS = 5;

        static void Main(string[] args)
        {
            nodes = GetNodesFromInput();
            //Part1();
            Part2();
        }

        private static void Part1()
        {
            InstructionNode rootNode;
            var unprocessedNodes = GetAllNodes();
            var processedNodes = new Queue<InstructionNode>();

            var availableNodes = new Stack<InstructionNode>();
            var rootNodes = GetRootNodes();
            ((List<InstructionNode>)rootNodes).Sort((x, y) => x.Parent.CompareTo(y.Parent));
            for (int i = rootNodes.Count - 1; i >= 0; i--)
                availableNodes.Push(rootNodes[i]);

            while(unprocessedNodes.Count > 0)
            {
                rootNode = availableNodes.Pop();
                foreach (var child in GetNodesFromChar(rootNode.Children))
                {
                    var newRoot = GetRootNode(child.Parent, unprocessedNodes.Where(x => x.Parent != rootNode.Parent).ToDictionary(x => x.Parent, x => x));
                    if(newRoot.Parent != child.Parent)
                    {
                        rootNode.RemoveChild(child.Parent);
                        rootNode.AddChildOrdered(newRoot.Parent);
                    }
                }
                unprocessedNodes.Remove(rootNode);
                for (int i = rootNode.Children.Count - 1; i >= 0; i--)
                    if (availableNodes.Where(x => x.Parent == rootNode.Children[i]).Count() == 0)
                        availableNodes.Push(nodes[rootNode.Children[i]]);

                processedNodes.Enqueue(rootNode);
            }

            while(processedNodes.Count > 0)
                Console.Write(processedNodes.Dequeue().Parent);

            Console.ReadLine();
        }
        private static void Part2()
        {
            var totalTime = 0;
            var unprocessedNodes = GetAllNodes();
            var processedNodes = new Queue<InstructionNode>();
            var busyWorkers = new List<Worker>();
            Queue<InstructionNode> toDoQueue;
            var completedNodes = new HashSet<InstructionNode>(new InstructionNodeComparer());
            var inProgressNodes = new HashSet<InstructionNode>(new InstructionNodeComparer());

            while(unprocessedNodes.Count > 0)
            {
                toDoQueue = GetAvailableNodes(unprocessedNodes, inProgressNodes);
                while(busyWorkers.Count < NUMBER_OF_WORKERS && toDoQueue.TryDequeue(out InstructionNode nextNode))
                {
                    if(inProgressNodes.Add(nextNode))
                        busyWorkers.Add(new Worker().SetUp(nextNode));
                    unprocessedNodes.Remove(nextNode);
                }
                busyWorkers.Sort((x, y) => x.SecondsUntilAvailable.CompareTo(y.SecondsUntilAvailable));
                var decrementAmount = busyWorkers[0].SecondsUntilAvailable;
                totalTime += decrementAmount;
                var removableWorkers = new List<Worker>();
                foreach(var worker in busyWorkers)
                {
                    worker.DecrementTimeRemaing(decrementAmount);
                    if (worker.Available)
                        removableWorkers.Add(worker);
                }
                foreach(var worker in removableWorkers)
                {
                    busyWorkers.Remove(worker);
                    inProgressNodes.Remove(nodes[worker.NodeToProcess]);
                    processedNodes.Enqueue(nodes[worker.NodeToProcess]);
                }
            }

            if (busyWorkers.Count > 0)
            {
                totalTime += busyWorkers[busyWorkers.Count - 1].SecondsUntilAvailable;
                foreach (var worker in busyWorkers)
                    processedNodes.Enqueue(nodes[worker.NodeToProcess]);
            }

            while (processedNodes.TryDequeue(out InstructionNode node))
                Console.Write(node.Parent);
            Console.WriteLine();

            Console.WriteLine($"The total time taken was {totalTime} seconds");

            Console.ReadLine();
        }


        private static IDictionary<char, InstructionNode> GetNodesFromInput()
        {
            string line = string.Empty, pattern = @"((Step|step) ([A-Z]))+";
            var nodesDict = new Dictionary<char, InstructionNode>();
            var uniqueSteps = new HashSet<char>();
            while((line = Console.ReadLine()) != "end")
            {
                var step1 = Regex.Matches(line, pattern)[0].Value;
                var step2 = Regex.Matches(line, pattern)[1].Value;
                var first = char.Parse(step1.ToUpper().Replace("STEP ", ""));
                var second = char.Parse(step2.ToUpper().Replace("STEP ", ""));

                if (nodesDict.TryGetValue(first, out InstructionNode node))
                {
                    node.AddChildOrdered(second);
                }
                else
                {
                    node = new InstructionNode { Parent = first };
                    node.AddChild(second);
                    nodesDict.Add(first, node);
                }
                uniqueSteps.Add(first);
                uniqueSteps.Add(second);
            }

            foreach (var step in uniqueSteps)
                nodesDict.TryAdd(step, new InstructionNode { Parent = step });

            foreach (var node in nodesDict.Values)
                foreach (var otherNode in nodesDict.Values.Where(x => x.Parent != node.Parent))
                    if (otherNode.ChildrenSet.Contains(node.Parent))
                        node.AddPrerequisit(otherNode.Parent);

            return nodesDict;
        }

        private static ISet<InstructionNode> GetAllNodes()
        {
            var uniqueSteps = new HashSet<InstructionNode>();
            foreach (var node in nodes.Values)
                foreach (var step in node.Children)
                    uniqueSteps.Add(new InstructionNode { Parent = step });

            var unprocessedNodes = nodes.Values.ToHashSet(new InstructionNodeComparer());
            unprocessedNodes.UnionWith(uniqueSteps);

            return unprocessedNodes;
        }

        private static IList<InstructionNode> GetNodesFromChar(IList<char> children)
        {
            var returnNodes = new List<InstructionNode>();
            foreach (var child in children)
                if(nodes.TryGetValue(child, out InstructionNode newNode))
                    returnNodes.Add(newNode);

            return returnNodes;
        }

        private static IList<InstructionNode> GetRootNodes()
        {
            var isRoot = true;
            var roots = new List<InstructionNode>();
            foreach (var root in nodes.Values)
            {
                var nodesSansCurrentRoot = nodes.Values.Where(x => x.Parent != root.Parent);
                var nodesSansEnumerator = nodesSansCurrentRoot.GetEnumerator();
                while(isRoot && nodesSansEnumerator.MoveNext())
                {
                    if (nodesSansEnumerator.Current.ChildrenSet.Contains(root.Parent))
                        isRoot = false;
                }
                if (isRoot)
                    roots.Add(root);
            }
            return roots;
        }

        private static InstructionNode GetRootNode(char criteria, IDictionary<char, InstructionNode> nodeDict)
        {
            var isRoot = true;

            var nodeDictEnumerator = nodeDict.GetEnumerator();
            while (isRoot && nodeDictEnumerator.MoveNext())
                isRoot = !nodeDictEnumerator.Current.Value.ChildrenSet.Contains(criteria);

            if (isRoot)
                return nodes[criteria];
            else
                return GetRootNode(nodeDictEnumerator.Current.Key, nodeDict.Where(x => x.Key != criteria).ToDictionary(x => x.Key, x => x.Value));
        }

        private static Queue<InstructionNode> GetAvailableNodes(ISet<InstructionNode> unprocessedNodes, ISet<InstructionNode> inProgressNodes)
        {
            var availableNodes = new List<InstructionNode>();
            foreach(var node in unprocessedNodes)
            {
                var nodeAvailable = true;
                foreach (var otherNode in unprocessedNodes.Union(inProgressNodes).Where(x => x.Parent != node.Parent))
                {
                    if(node.PrerequisitsSet.Contains(otherNode.Parent))
                    {
                        nodeAvailable = false;
                        break;
                    }
                }
                if (nodeAvailable)
                    availableNodes.Add(node);
            }

            availableNodes.Sort((x, y) => x.Parent.CompareTo(y.Parent));
            return new Queue<InstructionNode>(availableNodes);
        }
    }
}
