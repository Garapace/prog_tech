using System.Net.NetworkInformation;

namespace laba2
{
    internal class ChainList : BaseList
    {
        private class Node
        {
            public int Data { set; get; }
            public Node Next { set; get; }

            public Node(int data, Node next)
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

        protected override BaseList EmptyClone()
        {
            ChainList ChainList = new ChainList();
            return ChainList;
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

        public override void Add(int digit)
        {
            Node NewNode = new Node(digit, null);

            if (head == null) { head = NewNode; } // проверка что голова не ведёт в null
            else
            {
                Node tail = Find(quantity - 1);
                tail.Next = NewNode;
            }
            quantity++;
        }

        public override void Insert(int digit, int index)
        {
            if (index < 0 || index > quantity) { return; }
            if (index == 0)
            {
                Node NewNode = new Node(digit, null);
                NewNode.Next = head;
                head = NewNode;
            }
            else
            {
                Node CurrentNode = Find(index - 1);
                Node NewNode = new Node(digit, null);
                NewNode.Next = CurrentNode.Next;
                CurrentNode.Next = NewNode;
            }
            quantity++;
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
        }

        public override void Clear()
        {
            head = null;
            quantity = 0;
        }

        public override int this[int index]
        {
            get
            {
                if (index < quantity || index >= 0)
                {
                    Node CurrentNode = Find(index);
                    return CurrentNode.Data;
                }
                return 0;
            }
            set
            {
                if (index < quantity || index >= 0)
                {
                    Node CurrentNode = Find(index);
                    CurrentNode.Data = value;
                }
            }
        }

        public override void Sort()
        {
            Node CurrentNode = head;
            for (int i = 0; i < quantity - 1; i++)
            {
                Node NextNode = CurrentNode.Next;
                for (int j = i + 1; j < quantity; j++)
                {
                    if (CurrentNode.Data > NextNode.Data)
                    {
                        int temp = CurrentNode.Data;
                        CurrentNode.Data = NextNode.Data;
                        NextNode.Data = temp;
                    }
                    NextNode = NextNode.Next;
                }
                CurrentNode = CurrentNode.Next;
            }
        }


        public override void DeleteRepeat()
        {
            Node PrevNode = null;
            Node CurrentNode = head;
            while (CurrentNode.Next != null)
            {
                Node NextNode = CurrentNode.Next;
                while (NextNode != null)
                {
                    if (CurrentNode.Data == NextNode.Data)
                    {
                        if (PrevNode == null)
                        {
                            head = head.Next;
                            quantity--;
                        }
                        else
                        {
                            PrevNode.Next = CurrentNode.Next;
                            quantity--;
                        }
                        CurrentNode = CurrentNode.Next;
                        NextNode = CurrentNode;
                    }
                    NextNode = NextNode.Next;
                }
                PrevNode = CurrentNode;
                CurrentNode = CurrentNode.Next;
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
    }
}
