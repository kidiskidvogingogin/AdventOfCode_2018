using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7_TheSumOfItsParts
{
    class InstructionNode
    {
        public char Parent { get; set; }
        public List<char> Children { get; set; }
        public ISet<char> ChildrenSet { get; set; }
        public List<char> Prerequisits { get; set; }
        public ISet<char> PrerequisitsSet { get; set; }
        public int Time { get { return 60 + Parent - 'A' + 1; } }

        public InstructionNode()
        {
            Children = new List<char>();
            ChildrenSet = new HashSet<char>();
            Prerequisits = new List<char>();
            PrerequisitsSet = new HashSet<char>();
        }

        public void AddChild(char child)
        {
            if (ChildrenSet.Add(child))
                Children.Add(child);
        }

        public void AddChildOrdered(char child)
        {
            if (ChildrenSet.Add(child))
                Children.Add(child);

            if (Children.Count > 1)
                Children.Sort((x, y) => x.CompareTo(y));
        }

        public void AddPrerequisit(char prereq)
        {
            if (PrerequisitsSet.Add(prereq))
                Prerequisits.Add(prereq);
        }

        public void RemoveChild(char child)
        {
            Children.Remove(child);
            ChildrenSet.Remove(child);
        }

        public void ClearChildren()
        {
            Children.Clear();
            ChildrenSet.Clear();
        }
    }
}
