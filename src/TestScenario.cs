using ConsoleApp1.src.Logic;

namespace ConsoleApp1.src
{
    public class TestScenario
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public float Tick { get; set; } = 0;
        public void Setup()
        {
            RandomBooleanNode nodeB = new RandomBooleanNode();
            OrNode nodeA = new OrNode();
            Nodes.Add(nodeA);
            Nodes.Add(nodeB);

            nodeA.Setup();
            nodeB.Setup();

            nodeA.Connect(nodeB, "A", "OUTPUT");
        }

        public void Update()
        {
            Tick = Tick + 1;
            Console.WriteLine("------------");
            Console.WriteLine($"Tick: {Tick} ({NodeManager.FirstItem?.Count()})");
            Console.WriteLine("------------");
            for (int i = 0; i < Nodes.Count; i++)
            {
                Console.WriteLine($"N{i} - {Nodes[i].Value}");
            }
            NodeManager.Tick();
        }
    }
}
