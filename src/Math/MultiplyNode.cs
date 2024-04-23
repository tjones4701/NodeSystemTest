namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Multiply", "Multiplies A from B and outputs the result. ")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class MultiplyNode : Node
    {
        [NodeInput("A")]
        public float InputA { get; set; }

        [NodeInput("B")]
        public float InputB { get; set; }

        public override bool OnExecute()
        {
            base.OnExecute();
            SetValue("OUTPUT", InputA * InputB);
            return true;
        }
    }
}
