
using DEGG.NodeSystem;
using DEGG.NodeSystem.Nodes;

namespace ConsoleApp1.src
{
    public class TestScenario
    {
        public NodeNetwork NodeManager { get; set; } = new NodeNetwork();
        public float Tick { get; set; } = 0;
        public bool Running { get; set; } = true;
        public void Setup()
        {
            NodeManager.OnTick = OnTick;
            RandomBooleanNode nodeB = NodeManager.Add<RandomBooleanNode>();
            nodeB.SetSetting<long>("Interval", 1000);
            OrNode nodeA = NodeManager.Add<OrNode>();
            nodeA.Connect(nodeB, "A", "OUTPUT");


            TickNode tickNode = NodeManager.Add<TickNode>();


            ConstantNode constantNode1 = NodeManager.Add<ConstantNode>();
            constantNode1.SetSetting("Value", 10000f);

            DivideNode divideNode = NodeManager.Add<DivideNode>();
            divideNode.Connect(tickNode, "A", "OUTPUT");
            divideNode.Connect(constantNode1, "B", "OUTPUT");

            ConstantNode constantNode = NodeManager.Add<ConstantNode>();
            constantNode.SetSetting("Value", 10f);



            SinNode sinNode = NodeManager.Add<SinNode>();
            sinNode.Connect(divideNode, "INPUT", "OUTPUT");

            MultiplyNode multiplyNode = NodeManager.Add<MultiplyNode>();
            multiplyNode.Connect(constantNode, "A", "OUTPUT");
            multiplyNode.Connect(sinNode, "B", "OUTPUT");

            RoundNode roundNode = NodeManager.Add<RoundNode>();
            roundNode.Connect(multiplyNode, "INPUT", "OUTPUT");


        }

        public void OnTick()
        {
            Draw();
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 5);
            List<Node> nodes = NodeManager.Nodes;
            Tick = Tick + 1;
            Console.WriteLine("------------");
            Console.WriteLine($"Tick: {Tick} ({NodeManager.QueueCount()}) ({NodeManager.Nodes.Count()})");
            Console.WriteLine("------------");
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];
                Console.WriteLine($"N{i} - {node.NodeInformation.Name} - {nodes[i].Value}");
            }
        }



        public void Run()
        {
            while (Running)
            {
                NodeManager.Tick();
            }
        }
    }
}
