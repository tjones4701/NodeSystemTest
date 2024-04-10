namespace DEGG.NodeSystem
{

    public class NodeConnection
    {
        public required NodeConnector From { get; set; }
        public required InputNodeConnector To { get; set; }
    }
}
