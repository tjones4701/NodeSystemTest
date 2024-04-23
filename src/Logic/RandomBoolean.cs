//namespace DEGG.NodeSystem.Nodes
//{

//    [NodeInformation("Logic/Or", "Returns 1 if any input is true.")]
//    [NodeOutput("OUTPUT", typeof(bool))]
//    [NodeSetting("Interval", "Time in milliseconds when this will update.", typeof(long))]
//    internal class RandomBooleanNode : Node
//    {
//        public bool NextValue { get; set; }

//        public long LastRanAtTick { get; set; } = 0;
//        public override void OnSetup()
//        {
//            base.OnSetup();
//        }

//        public override bool OnExecute()
//        {
//            base.OnExecute();
//            SetValue(NextValue);
//            return true;

//        }
//        public override void OnTick()
//        {
//            long interval = GetSetting<long>("Interval") * 10000;
//            long now = DateTime.Now.Ticks;
//            if (LastRanAtTick < now)
//            {
//                LastRanAtTick = now + interval;
//                Random random = new Random();
//                bool nextValue = random.Next(0, 2) == 1 ? true : false;
//                if (NextValue != nextValue)
//                {
//                    NextValue = nextValue;
//                    SelfExecute();
//                }
//            }
//        }
//    }
//}
