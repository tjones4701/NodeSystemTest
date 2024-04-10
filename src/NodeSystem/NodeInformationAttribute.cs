namespace DEGG.NodeSystem
{
    public class NodeInformationAttribute : Attribute
    {
        public string Name;
        public string Description = "";

        public NodeInformationAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public NodeInformationAttribute(string name)
        {
            Name = name;
        }
    }
}
