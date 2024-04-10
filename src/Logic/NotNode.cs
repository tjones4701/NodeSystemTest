namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Logic/Not", "Returns 1 if any input is true.")]
    [NodeOutput("OUTPUT", typeof(bool))]
    [NodeInput("INPUT", [typeof(bool), typeof(string), typeof(float)])]
    public class NotNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            bool Input = Utilities.ToBoolean(GetValueOfInput("INPUT"));

            SetValue(!Input);
            return true;
        }
    }
}
