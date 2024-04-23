namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Divide", "Divides A from B and outputs the result. Will not calculate/update if B is 0.")]
    [NodeOutput("OUTPUT", typeof(float))]
    public class DivideNode : Node
    {

        [NodeInput("A")]
        public float InputA { get; set; }

        [NodeInput("B")]
        public float InputB { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute();
            if (InputB == 0)
            {
                return true;
            }
            SetValue("OUTPUT", InputA / InputB);
            return true;
        }
    }
}
