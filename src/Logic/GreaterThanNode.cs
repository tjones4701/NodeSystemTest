namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Logic/Greater Than", "Returns true if A is greater than B")]
    [NodeOutput("OUTPUT", typeof(bool))]
    [NodeInput("A", [typeof(bool), typeof(string), typeof(float)])]
    [NodeInput("B", [typeof(bool), typeof(string), typeof(float)])]
    public class GreaterThanNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            object? a = GetValueOfInput("A");
            object? b = GetValueOfInput("B");
            if (a is float && b is float)
            {
                SetValue((float)a > (float)b ? true : false);
                return true;
            }
            // If they are both not floats then compare as strings
            string aString = a?.ToString() ?? "";
            string bString = b?.ToString() ?? "";

            SetValue(string.Compare(aString, bString) > 0 ? true : false);
            return true;
        }
    }

}
