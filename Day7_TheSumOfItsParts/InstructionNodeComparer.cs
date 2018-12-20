using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Day7_TheSumOfItsParts
{
    class InstructionNodeComparer : IEqualityComparer<InstructionNode>
    {
        public bool Equals(InstructionNode x, InstructionNode y)
        {
            return x.Parent == y.Parent;
        }

        public int GetHashCode(InstructionNode obj)
        {
            return obj.Parent.GetHashCode();
        }
    }
}
