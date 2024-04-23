namespace DEGG.NodeSystem.Nodes
{

    [NodeInformation("Logic/Tick", "Returns the current tick in milliseconds")]
    [NodeOutput("OUTPUT", typeof(long))]
    internal class TickNode : Node
    {
        public override void OnTick()
        {
            long now = Network?.CurrentTick ?? 0;
            SetValue("OUTPUT", now);
        }
    }
}
