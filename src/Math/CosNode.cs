namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Cos")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class CosNode : Node
    {
        [NodeInput("Input")]
        public float Input { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute(); ;
            SetValue("OUTPUT", (float)Math.Cos(Input));
            return true;
        }
    }

}
