namespace DEGG.NodeSystem
{

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

        public bool IsConnected()
        {
            return Connections.Count > 0;
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
                    nodeConnector.Parent.Network?.Add(nodeConnector.Parent);
                }
            }
        }
    }
}
