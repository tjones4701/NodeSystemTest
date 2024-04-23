namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Logic/Greater Than", "Returns true if A is greater than B")]
    [NodeOutput("OUTPUT", typeof(bool))]
    public class GreaterThanNode : Node
    {

        [NodeInput]
        public object A { get; set; }

        [NodeInput]
        public object B { get; set; }

        public override bool OnExecute()
        {
            base.OnExecute();
            object? a = A;
            object? b = B;
            if (a is float && b is float)
            {
                SetValue("OUTPUT", (float)a > (float)b ? true : false);
                return true;
            }
            // If they are both not floats then compare as strings
            string aString = a?.ToString() ?? "";
            string bString = b?.ToString() ?? "";

            SetValue("OUTPUT", string.Compare(aString, bString) > 0 ? true : false);
            return true;
        }
    }

}
