namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Floor")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("INPUT", [typeof(bool), typeof(string), typeof(float)])]
    public class FloorNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("INPUT"));
            SetValue(Math.Floor(first));
            return true;
        }
    }

}
