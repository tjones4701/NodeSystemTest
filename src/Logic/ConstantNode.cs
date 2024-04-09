namespace ConsoleApp1.src.Logic
{
    internal class ConstantNode : Node
    {
        public override void OnSetup()
        {
            base.OnSetup();
            AddOutput("OUTPUT");
        }

        public override void OnExecute()
        {
            base.OnExecute();
            SetValue(1);
        }
    }
}
