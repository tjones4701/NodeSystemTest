namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Subtract", "Subtracts A from B inputs from the first and outputs the result")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("A", [typeof(bool), typeof(string), typeof(float)])]
    [NodeInput("B", [typeof(bool), typeof(string), typeof(float)])]
    public class SubtractNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("A"));
            float second = Utilities.ToNumber(GetValueOfInput<object>("B"));
            SetValue(first - second);
            return true;
        }
    }
}
