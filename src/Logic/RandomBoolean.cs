namespace ConsoleApp1.src.Logic
{
    internal class RandomBooleanNode : Node
    {
        public bool NextValue { get; set; }
        public override void OnSetup()
        {
            base.OnSetup();
            AddOutput("OUTPUT");
        }

        public override void OnExecute()
        {
            base.OnExecute();
            SetValue(NextValue);

        }
        public override void OnTick()
        {
            Random random = new Random();
            bool nextValue = random.Next(0, 2) == 1 ? true : false;
            if (NextValue != nextValue)
            {
                NextValue = nextValue;
                SelfExecute();
            }
        }
    }
}
