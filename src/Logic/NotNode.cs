namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Logic/Not", "Returns 1 if any input is true.")]
    [NodeOutput("OUTPUT", typeof(bool))]
    public class NotNode : Node
    {
        [NodeInput]
        public bool Input { get; set; }
        public override bool OnExecute()
        {
            base.OnExecute();

            SetValue("OUTPUT", !Input);
            return true;
        }
    }
}
