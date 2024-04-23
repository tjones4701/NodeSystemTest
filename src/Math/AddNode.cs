namespace DEGG.NodeSystem.Nodes
{


    [NodeInformation("Math/Add", "Adds A to B and out outputs the result")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class AddNode : Node
    {
        [NodeInput("A")]
        public float InputA { get; set; }

        [NodeInput("B")]
        public float InputB { get; set; }

        public override bool OnExecute()
        {
            base.OnExecute();
            SetValue("OUTPUT", InputA + InputB);
            return true;
        }
    }

}
