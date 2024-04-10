namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Cos")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("INPUT", [typeof(bool), typeof(string), typeof(float)])]
    public class CosNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("INPUT"));
            SetValue((float)Math.Cos(first));
            return true;
        }
    }

}
