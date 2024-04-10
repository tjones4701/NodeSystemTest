namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Sin")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("INPUT", [typeof(bool), typeof(string), typeof(float)])]
    public class SinNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("INPUT"));
            SetValue(Math.Sin(first));
            return true;
        }
    }

}
