namespace DEGG.NodeSystem
{
    public class NodeLinkListItem
    {

        public NodeLinkListItem? Previous { get; set; }
        public NodeLinkListItem? Next { get; set; }
        public required Node Current { get; set; }

        public NodeLinkListItem GetLast()
        {
            if (Next == null)
            {
                return this;
            }
            else
            {
                return Next.GetLast();
            }
        }
        public NodeLinkListItem GetPrevious()
        {
            if (Previous == null)
            {
                return this;
            }
            else
            {
                return Previous.GetPrevious();
            }
        }
        public void Remove()
        {
            if (Previous != null)
            {
                Previous.Next = Next;
            }
            if (Next != null)
            {
                Next.Previous = Previous;
            }
        }

        public void InsertAfter(NodeLinkListItem item)
        {
            NodeLinkListItem? oldNext = Next;
            item.Next = Next;
            item.Previous = this;
            Next = item;
            if (oldNext != null)
            {
                oldNext.Previous = item;
            }
        }

        public void InsertBefore(NodeLinkListItem item)
        {
            NodeLinkListItem? oldPrevious = Previous;
            item.Previous = Previous;
            item.Next = this;
            Previous = item;
            if (oldPrevious != null)
            {
                oldPrevious.Next = item;
            }
        }
        public int Count()
        {
            if (Next == null)
            {
                return 1;
            }
            else
            {
                return 1 + Next.Count();
            }
        }
    }



}
