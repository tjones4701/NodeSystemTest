namespace ConsoleApp1.src
{
    public class NodeManagerQueueItem
    {

        public NodeManagerQueueItem? Previous { get; set; }
        public NodeManagerQueueItem? Next { get; set; }
        public required Node Current { get; set; }

        public NodeManagerQueueItem GetLast()
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
        public NodeManagerQueueItem GetPrevious()
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

        public void InsertAfter(NodeManagerQueueItem item)
        {
            NodeManagerQueueItem? oldNext = Next;
            item.Next = Next;
            item.Previous = this;
            Next = item;
            if (oldNext != null)
            {
                oldNext.Previous = item;
            }
        }

        public void InsertBefore(NodeManagerQueueItem item)
        {
            NodeManagerQueueItem? oldPrevious = Previous;
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

    public static class NodeManager
    {
        public static List<Node> AllNodes { get; set; } = new List<Node>();
        public static NodeManagerQueueItem? FirstItem { get; set; }
        public static int ExecutionsPerTick { get; set; } = 1;

        public static void Tick()
        {
            for (int i = 0; i < AllNodes.Count; i++)
            {
                AllNodes[i].OnTick();
            }
            Execute(ExecutionsPerTick);
        }

        public static void Add(Node node)
        {
            NodeManagerQueueItem item = new NodeManagerQueueItem { Current = node };
            if (FirstItem == null)
            {
                FirstItem = item;
            }
            else
            {
                NodeManagerQueueItem last = FirstItem.GetLast();
                last.Next = item;
            }
        }


        public static void Execute(int amount)
        {
            ExecuteNext();
            amount = amount - 1;
            if (amount > 0)
            {
                Execute(amount);
            }
        }

        public static void ExecuteNext()
        {
            if (FirstItem != null)
            {
                FirstItem.Current.Execute();
                NodeManagerQueueItem oldFirstItem = FirstItem;
                FirstItem = FirstItem.Next;
                oldFirstItem.Remove();
            }
        }

    }

    public class NodeConnector
    {
        public string Name { get; set; } = "name";
        public Node? Parent { get; set; }
        public List<NodeConnection> Connections { get; set; } = new List<NodeConnection>();
        public virtual int MaxConnections { get; set; }
        public object? Value { get; set; }

        public void RemoveConnection(NodeConnection connection)
        {
            Connections.Remove(connection);
            connection.To.Connections.Remove(connection);
        }


        public NodeConnection Connect(InputNodeConnector other)
        {
            if (other.Parent == Parent)
            {
                throw new InvalidOperationException("Cannot connect nodes that are part of the same parent.");
            }
            NodeConnector from = this;
            InputNodeConnector to = other;
            NodeConnection connection = new NodeConnection { From = from, To = to };
            Connections.Add(connection);
            other.Connections.Add(connection);

            return connection;
        }


        public void OnChange()
        {
            if (Parent == null)
            {
                return;
            }
            for (int i = 0; i < Connections.Count; i++)
            {
                NodeConnection connection = Connections[i];
                NodeConnector nodeConnector = connection.To;
                if (nodeConnector == this)
                {
                    nodeConnector = connection.From;
                }
                if (nodeConnector.Parent != null)
                {
                    NodeManager.Add(nodeConnector.Parent);
                }
            }
        }
    }

    public class InputNodeConnector : NodeConnector
    {
        public override int MaxConnections { get; set; } = 1;

        public T? GetValue<T>()
        {
            NodeConnection? connection = Connections.FirstOrDefault();
            if (connection == null)
            {
                return default(T);
            }
            if (connection.From.Parent == null)
            {
                return default(T);
            }
            return connection.From.Parent.GetValue<T>();
        }
    }

    public class NodeConnection
    {
        public required NodeConnector From { get; set; }
        public required InputNodeConnector To { get; set; }
    }


    public class Node
    {
        public List<InputNodeConnector> Inputs { get; set; } = new();
        public List<NodeConnector> Outputs { get; set; } = new();
        public object? Value { get; set; }
        public bool IsSetup { get; set; } = false;

        public void Setup()
        {
            if (IsSetup)
            {
                return;
            }
            OnSetup();
            NodeManager.AllNodes.Add(this);
            NodeManager.Add(this);
            IsSetup = true;
        }
        public virtual void OnSetup()
        {

        }

        public InputNodeConnector AddInput(string name)
        {
            return AddInput<InputNodeConnector>(name);
        }

        public InputNodeConnector AddInput<T>(string name) where T : InputNodeConnector, new()
        {
            InputNodeConnector? existing = GetInputByName(name);
            if (existing != null)
            {
                return existing;
            }
            T input = new T { Name = name, Parent = this };
            Inputs.Add(input);
            return input;
        }

        public NodeConnector AddOutput(string name)
        {
            return AddOutput<NodeConnector>(name);
        }

        public void Connect(Node other, string inputName, string outputName)
        {
            NodeConnector? output = other.GetOutputByName(outputName);
            InputNodeConnector? input = GetInputByName(inputName);
            if (output == null)
            {
                throw new InvalidOperationException($"Output {outputName} not found on node {this}");
            }
            if (input == null)
            {
                throw new InvalidOperationException($"Input {inputName} not found on node {other}");
            }

            output.Connect(input);
        }


        public NodeConnector AddOutput<T>(string name) where T : NodeConnector, new()
        {
            NodeConnector? existing = GetInputByName(name);
            if (existing != null)
            {
                return existing;
            }

            T output = new T { Name = name, Parent = this };
            Outputs.Add(output);
            return output;
        }

        public InputNodeConnector? GetInputByName(string name)
        {
            InputNodeConnector? input = Inputs.FirstOrDefault(x => x.Name == name);
            return input;
        }
        public NodeConnector? GetOutputByName(string name)
        {
            return Outputs.FirstOrDefault(x => x.Name == name);
        }



        public T? GetValue<T>()
        {
            if (Value is T val)
            {
                return val;
            }
            return default(T);
        }

        public T? GetValueOfInput<T>(string name)
        {
            InputNodeConnector? input = GetInputByName(name);
            if (input == null)
            {
                throw new InvalidOperationException($"Input {name} not found on node {this}");
            }
            return input.GetValue<T>();
        }

        public void SetValue(object value)
        {
            bool hasChanged = !EqualityComparer<object>.Default.Equals(Value, value);
            Value = value;
            if (hasChanged)
            {
                TriggerChange();
            }
        }

        public void SelfExecute()
        {
            NodeManager.Add(this);
        }

        public void TriggerChange()
        {
            for (int i = 0; i < Outputs.Count; i++)
            {
                Outputs[i].OnChange();
            }
        }

        public Dictionary<string, T?> GetValueOfInputs<T>()
        {
            Dictionary<string, T?> values = new Dictionary<string, T?>();
            for (int i = 0; i < Inputs.Count; i++)
            {
                values[Inputs[i].Name] = Inputs[i].GetValue<T>();
            }
            return values;
        }

        public void Execute()
        {
            OnExecute();
        }

        public virtual void OnExecute()
        {

        }

        public virtual void OnTick()
        {

        }
    }
}
