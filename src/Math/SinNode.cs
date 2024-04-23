namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Sin")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class SinNode : Node
    {
        [NodeInput("Input")]
        public float Input { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute(); ;
            SetValue("OUTPUT", (float)Math.Sin(Input));
            return true;
        }
    }

}
