namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Round")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class RoundNode : Node
    {
        [NodeInput("Input")]
        public float Input { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute(); ;
            SetValue("OUTPUT", (float)Math.Round(Input));
            return true;
        }
    }

}
