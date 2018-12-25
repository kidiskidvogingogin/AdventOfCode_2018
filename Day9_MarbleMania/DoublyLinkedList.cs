using System;
using System.Collections.Generic;
using System.Text;

namespace Day9_MarbleMania
{
    class DoublyLinkedList<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
            //public bool IsHead { get; set; }
        }

        private Node Head { get; set; }

        public void AddFirst(T data)
        {
            if (Head == null)
                Head = new Node { Value = data };
            else
            {
                var current = new Node
                {
                    Next = Head,
                    Previous = Head.Previous,
                    Value = data
                };
                Head.Previous = current;
                Head = current;
            }
        }

        public void AddLast(T data)
        {
            if (Head == null)
                Head = new Node { Value = data };
            else
            {
                var nodeToAdd = new Node
                {
                    Value = data
                };
                var current = Head;
                while(current.Next != null)
                {
                    current = current.Next;
                }
                nodeToAdd.Previous = current.Previous;
                nodeToAdd.Next = Head;
                current.Next = nodeToAdd;
            }
        }

        public T GetNodeXAway(int distance)
        {
            var current = Head;

            if (distance < 0)
                for (int i = 0; i > distance; i--)
                    current = Head.Previous;
            else if (distance > 0)
                for (int i = 0; i < distance; i++)
                    current = Head.Next;

            return current.Value;
        }

        public T RemoveNodeXAway(int distance)
        {
            var current = Head;
            if (distance < 0)
                for (int i = 0; i > distance; i--)
                    current = current.Previous;
            else if (distance > 0)
                for (int i = 0; i < distance; i++)
                    current = current.Next;

            var nextNode = current.Next;
            var previousNode = current.Previous;
            nextNode.Previous = previousNode;
            previousNode.Next = nextNode;

            return current.Value;
        }

        public T RemoveNodeAndSetHead(int distance)
        {
            var current = Head;
            if (distance < 0)
                for (int i = 0; i > distance; i--)
                    current = current.Previous;
            else if (distance > 0)
                for (int i = 0; i < distance; i++)
                    current = current.Next;

            var nextNode = current.Next;
            var previousNode = current.Previous;
            nextNode.Previous = previousNode;
            previousNode.Next = nextNode;

            Head = current.Next;
            return current.Value;
        }

        public void AddNodeXAway(int distance, T value)
        {
            var current = Head;
            if(Head.Next != null)
            {
                if (distance < 0)
                    for (int i = 0; i > distance; i--)
                        current = current.Previous;
                else if (distance > 0)
                    for (int i = 0; i < distance; i++)
                        current = current.Next;

                var newNode = new Node
                {
                    Next = current.Next,
                    Previous = current,
                    Value = value
                };
                current.Next.Previous = newNode;
                current.Next = newNode;

                Head = newNode;
            }
            else
            {
                var newNode = new Node
                {
                    Next = Head,
                    Previous = Head,
                    Value = value
                };
                Head.Next = newNode;
                Head.Previous = newNode;

                Head = newNode;
            }
        }

        public string AllNodesToString()
        {
            var retVal = new StringBuilder();
            var current = Head.Next;
            retVal.Append($"{Head.Value}");
            while(current != Head)
            {
                retVal.Append($" {current.Value}");
                current = current.Next;
            }
            return retVal.ToString();
        }
    }
}
