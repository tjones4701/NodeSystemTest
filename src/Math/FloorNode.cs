namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Floor")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class FloorNode : Node
    {
        [NodeInput("Input")]
        public float Input { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute(); ;
            SetValue("OUTPUT", (float)Math.Floor(Input));
            return true;
        }
    }

}
