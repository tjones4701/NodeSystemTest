using System.Reflection;

namespace DEGG.NodeSystem
{

    public class InputNodeConnector : NodeConnector
    {
        public NodeInput2Attribute? Attribute { get; set; }

        public System.Reflection.PropertyInfo? PropertyInfo { get; set; }

        public NodeInput(Node parent, NodeInput2Attribute attribute, System.Reflection.PropertyInfo propertyInfo)
        {
            Parent = parent;
            Attribute = attribute;
            PropertyInfo = propertyInfo;
        }

        public InputNodeConnector(Node parent, NodeInput2Attribute attribute, PropertyInfo propertyInfo) : base(parent, attribute, propertyInfo)
        {
        }

        public override int MaxConnections { get; set; } = 1;

        public T? GetValue<T>()
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
            return connection.From.Parent.GetValue<T>();
        }
    }
}
