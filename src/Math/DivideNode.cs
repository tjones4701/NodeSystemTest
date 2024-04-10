namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Divide", "Divides A from B and outputs the result. Will not calculate/update if B is 0.")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("A", [typeof(bool), typeof(string), typeof(float)])]
    [NodeInput("B", [typeof(bool), typeof(string), typeof(float)])]
    public class DivideNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("A"));
            float second = Utilities.ToNumber(GetValueOfInput<object>("B"));
            if (second == 0)
            {
                return true;
            }
            SetValue(first / second);
            return true;
        }
    }
}
