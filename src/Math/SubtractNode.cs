namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Subtract", "Subtracts A from B inputs from the first and outputs the result")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class SubtractNode : Node
    {
        [NodeInput("A")]
        public float InputA { get; set; }

        [NodeInput("B")]
        public float InputB { get; set; }

        public override bool OnExecute()
        {
            base.OnExecute();
            SetValue("OUTPUT", InputA - InputB);
            return true;
        }
    }
}
