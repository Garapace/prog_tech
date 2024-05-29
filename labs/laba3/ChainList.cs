using System;
using System.Net.NetworkInformation;
using System.Text;

namespace laba3
{
    internal class ChainList<T> : BaseList<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Data { set; get; }
            public Node Next { set; get; }

            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        private Node head;

        public ChainList()
        {
            head = null;
            quantity = 0;
        }

        protected override BaseList<T> EmptyClone()
        {
            return new ChainList<T>();
        }

        private Node Find(int index)
        {
            if (index < 0 || index >= quantity) { return null; }

            int i = 0;
            Node CurrentNode = head;
            while (CurrentNode != null && i < index)
            {
                CurrentNode = CurrentNode.Next;
                i++;
            }

            if (i == index) { return CurrentNode; }
            else { return null; }
        }

        public override void Add(T item)
        {
            Node NewNode = new Node(item, null);

            if (head == null) { head = NewNode; } // проверка что голова не ведёт в null
            else
            {
                Node tail = Find(quantity - 1);
                tail.Next = NewNode;
            }
            quantity++;
            check();
        }

        public override void Insert(T item, int index)
        {
            if (index < 0 || index > quantity) { return; }
            if (index == 0)
            {
                Node NewNode = new Node(item, null);
                NewNode.Next = head;
                head = NewNode;
            }
            else
            {
                Node CurrentNode = Find(index - 1);
                Node NewNode = new Node(item, null);
                NewNode.Next = CurrentNode.Next;
                CurrentNode.Next = NewNode;
            }
            quantity++;
            check();
        }

        public override void Delete(int index)
        {
            if (index < 0 || index >= quantity) { return; }
            if (index == 0 && head != null)
            {
                head = head.Next;
                quantity--;
                return;
            }

            Node PreviousNode = Find(index - 1);
            Node CurrentNode = Find(index);

            if (CurrentNode != null)
            {
                PreviousNode.Next = CurrentNode.Next;
                quantity--;
            }
            check();
        }

        public override void Clear()
        {
            head = null;
            quantity = 0;
        }

        public override T this[int index]
        {
            get
            {
                if (index < quantity || index >= 0)
                {
                    Node CurrentNode = Find(index);
                    return CurrentNode.Data;
                }
                throw new Exceptions.BadIndexException("Не существующий индекс");
            }
            set
            {
                if (index < quantity || index >= 0)
                {
                    Node CurrentNode = Find(index);
                    CurrentNode.Data = value;
                }
                throw new Exceptions.BadIndexException("Не существующий индекс");
            }
        }

        public override void Sort()
        {
            Node currentNode = head;
            for (int i = 0; i < quantity - 1; i++)
            {
                Node nextNode = currentNode.Next;
                for (int j = i + 1; j < quantity; j++)
                {
                    if (currentNode.Data.CompareTo(nextNode.Data) > 0)
                    {
                        (currentNode.Data, nextNode.Data) = (nextNode.Data, currentNode.Data);
                    }
                    nextNode = nextNode.Next;
                }
                currentNode = currentNode.Next;
            }
        }

        public override void Print()
        {
            Node CurrentNode = head;
            while (CurrentNode != null)
            {
                Console.Write(CurrentNode.Data + " ");
                CurrentNode = CurrentNode.Next;
            }
            Console.WriteLine();
        }

        public override string ToString()
        {
            Node currentNode = head;
            StringBuilder write = new StringBuilder();

            while (currentNode != null)
            {
                write.Append(currentNode.Data + "\n");
                currentNode = currentNode.Next;
            }
            return write.ToString();
        }

    }
}
