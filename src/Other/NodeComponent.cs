namespace DEGG.NodeSystem
{

    public class NodeComponent
    {
        public NodeNetwork? Network { get; set; }
        public Node? Node { get; set; }

        public T? SetNode<T>(T node) where T : Node
        {
            if (Network == null)
            {
                return null;
            }
            Node = node;
            Network.Add(node);

            return node;
        }
        public T? SetNode<T>() where T : Node, new()
        {
            return SetNode(new T());
        }

    }
}
