namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Ceiling")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("INPUT", [typeof(bool), typeof(string), typeof(float)])]
    public class CeilingNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("INPUT"));
            SetValue(Math.Ceiling(first));
            return true;
        }
    }

}
