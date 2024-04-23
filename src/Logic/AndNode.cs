
//namespace DEGG.NodeSystem.Nodes
//{
//    [NodeInformation("Logic/And", "Returns 1 if all connected inputs are.")]
//    [NodeOutput("OUTPUT", typeof(bool))]
//    [NodeInput("A", [typeof(bool), typeof(string), typeof(float)])]
//    [NodeInput("B", [typeof(bool), typeof(string), typeof(float)])]
//    [NodeInput("C", [typeof(bool), typeof(string), typeof(float)])]
//    [NodeInput("D", [typeof(bool), typeof(string), typeof(float)])]
//    internal class AndNote : Node
//    {
//        public override void OnSetup()
//        {
//            base.OnSetup();
//        }

//        public override bool OnExecute()
//        {
//            base.OnExecute();
//            Dictionary<string, object?> values = GetValueOfInputs<object>();
//            foreach (KeyValuePair<string, object?> kv in values)
//            {
//                object? value = kv.Value;
//                string str = value?.ToString()?.ToLower() ?? "0";
//                if (str != null)
//                {
//                    if (str != "0" && str.ToLower() != "false")
//                    {
//                        SetValue(1);
//                        return true;
//                    }
//                }
//            }
//            SetValue(0);
//            return true;
//        }
//    }
//}
