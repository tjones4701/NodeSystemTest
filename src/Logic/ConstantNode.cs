namespace DEGG.NodeSystem.Nodes
{
    [NodeInformation("Logic/Constant", "Outputs a constant float")]
    [NodeOutput("OUTPUT", typeof(float))]
    [NodeSetting("Value", "The value to output", typeof(float))]
    internal class ConstantNode : Node
    {

        public override bool OnExecute()
        {
            base.OnExecute();
            SetValue("OUTPUT", GetSetting<float>("Value"));
            return true;
        }

        public override void OnSettingchange(NodeSetting setting)
        {
            base.OnSettingchange(setting);
            Value = GetSetting<float>("Value");
            SetValue("OUTPUT", Value);
        }
    }
}
