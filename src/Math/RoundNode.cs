namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Round")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("INPUT", [typeof(bool), typeof(string), typeof(float)])]
    public class RoundNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("INPUT"));
            SetValue(Math.Round(first));
            return true;
        }
    }

}
