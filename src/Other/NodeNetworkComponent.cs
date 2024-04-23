namespace DEGG.NodeSystem
{

    public class NodeNetworkComponent
    {
        public NodeNetwork? Network { get; set; } = new NodeNetwork();

        public void Tick()
        {
            if (Network == null)
            {
                return;
            }

            Network.Tick();
        }

    }
}
