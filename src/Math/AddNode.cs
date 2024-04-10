namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Add", "Adds A to B and out outputs the result")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("A", [typeof(bool), typeof(string), typeof(float)])]
    [NodeInput("B", [typeof(bool), typeof(string), typeof(float)])]
    public class AddNode : Node
    {
        [NodeInput2("A")]
        public float InputA { get; set; }

        [NodeInput2("B")]
        public float InputB { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute();
            SetValue(InputA + InputB);
            return true;
        }
    }

}
