namespace DEGG.NodeSystem
{

    public class NodeNetwork
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public NodeLinkListItem? FirstItem { get; set; }
        /**
         * The amount of executions per tick.
         * If this goes over the MaxExecutionsPerTick then it will be split over multiple ticks.
         * This is used as a fallback to prevent the update loop from freezing.
         **/
        public int MaxExecutionsPerTick { get; set; } = 10000;

        /**
         * The minimum amount of updates per second.
         * If the amount of updates per second goes below this then it will wait until the next update before executing the next node.
         **/
        public int MinimumUPS { get; set; } = 60;

        public long LastTickTime { get; set; } = 0;

        public Action? OnTick { get; set; }

        public long CurrentTick { get; set; }

        public long GetTime()
        {
            DateTime now = DateTime.Now;
            return now.Ticks;
        }

        public int QueueCount()
        {
            return FirstItem?.Count() ?? 0;
        }

        public void Tick()
        {
            CurrentTick = CurrentTick + 1;
            // add 1 second to the time in nanoseconds.
            long endTickTime = GetTime() + 10000000;

            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Tick();
            }

            bool continueLooping = true;
            int currentExecutions = 0;

            while (continueLooping)
            {

                // Will always do atleast a single execution.
                // If the looping through Nodes[i].OnTick() takes ages then the simulation will only execute once every update.
                Execute(1);
                currentExecutions++;
                if (IsEmpty())
                {
                    continueLooping = false;
                    continue;
                }
                if (currentExecutions >= MaxExecutionsPerTick)
                {
                    continueLooping = false;
                    continue;
                }
                long currentTime = GetTime();
                if (currentTime > endTickTime)
                {
                    continueLooping = false;
                    continue;
                }
            }
            // Invote OnTick if it is set.
            OnTick?.Invoke();
        }

        public void Add(Node node)
        {
            NodeLinkListItem item = new NodeLinkListItem { Current = node };
            if (FirstItem == null)
            {
                FirstItem = item;
            }
            else
            {
                NodeLinkListItem last = FirstItem.GetLast();
                last.Next = item;
            }
        }

        public T Add<T>() where T : Node, new()
        {
            T newNode = new T();
            newNode.Network = this;
            Nodes.Add(newNode);
            newNode.Setup();

            Add(newNode);

            return newNode;
        }

        public bool IsEmpty()
        {
            return FirstItem == null;
        }


        public void Execute()
        {
            ExecuteNext();
        }
        public void Execute(int amount)
        {
            ExecuteNext();
            amount = amount - 1;
            if (amount > 0)
            {
                Execute(amount);
            }
        }

        public void ExecuteNext()
        {
            if (FirstItem != null)
            {
                NodeLinkListItem oldFirstItem = FirstItem;

                Node currentNode = oldFirstItem.Current;
                bool result = currentNode.Execute();

                FirstItem = FirstItem?.Next;
                oldFirstItem.Remove();

                // If the result is not true then we just add the node back to the end of the list.
                if (result != true)
                {
                    Add(currentNode);
                    return;
                }
            }
        }

    }
}
