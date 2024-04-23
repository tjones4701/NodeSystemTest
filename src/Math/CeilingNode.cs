namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Ceiling")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class CeilingNode : Node
    {
        [NodeInput("Input")]
        public float Input { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute(); ;
            SetValue("OUTPUT", (float)Math.Ceiling(Input));
            return true;
        }
    }

}
