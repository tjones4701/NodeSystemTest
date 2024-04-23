namespace DEGG.NodeSystem
{

    public class InputNodeConnector : NodeConnector
    {
        public NodeInputAttribute? Attribute { get; set; }

        public System.Reflection.PropertyInfo? PropertyInfo { get; set; }

        public InputNodeConnector(Node parent, NodeInputAttribute attribute, System.Reflection.PropertyInfo propertyInfo)
        {
            Parent = parent;
            Attribute = attribute;
            PropertyInfo = propertyInfo;
        }


        public override int MaxConnections { get; set; } = 1;

        public object? GetValue()
        {
            NodeConnection? connection = Connections.FirstOrDefault();
            if (connection == null)
            {
                return default;
            }
            if (connection.From.Parent == null)
            {
                return default;
            }
            return connection.From.Value;
        }
    }
}
