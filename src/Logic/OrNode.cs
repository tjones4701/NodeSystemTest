namespace ConsoleApp1.src.Logic
{
    internal class OrNode : Node
    {
        public override void OnSetup()
        {
            base.OnSetup();
            AddOutput("OUTPUT");
            AddInput("A");
            AddInput("B");
            AddInput("C");
            AddInput("D");
        }

        public override void OnExecute()
        {
            base.OnExecute();
            Dictionary<string, object?> values = GetValueOfInputs<object>();
            foreach (KeyValuePair<string, object?> kv in values)
            {
                object? value = kv.Value;
                string str = value?.ToString()?.ToLower() ?? "0";
                if (str != null)
                {
                    if (str != "0" && str.ToLower() != "false")
                    {
                        SetValue(1);
                        return;
                    }
                }
            }
            SetValue(0);
        }
    }
}
