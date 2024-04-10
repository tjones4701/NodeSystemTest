namespace DEGG.NodeSystem
{

    public class InputNodeConnector : NodeConnector
    {
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
