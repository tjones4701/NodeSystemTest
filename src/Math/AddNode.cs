﻿namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Math/Add", "Adds A to B and out outputs the result")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeInput("A", [typeof(bool), typeof(string), typeof(float)])]
    [NodeInput("B", [typeof(bool), typeof(string), typeof(float)])]
    public class AddNode : Node
    {
        public override bool OnExecute()
        {
            base.OnExecute();
            float first = Utilities.ToNumber(GetValueOfInput<object>("A"));
            float second = Utilities.ToNumber(GetValueOfInput<object>("B"));
            SetValue(first + second);
            return true;
        }
    }

}
