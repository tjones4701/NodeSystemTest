namespace DEGG.NodeSystem
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NodeInputAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; } = "";
        public Type[] Type { get; }

        public NodeInputAttribute(string name, Type[] type)
        {
            Name = name;
            Type = type;
        }

        public NodeInputAttribute(string name, string description, Type[] type)
        {
            Name = name;
            Type = type;
            Description = description;
        }
        public NodeInputAttribute(string name, string description, Type type)
        {
            Name = name;
            Type = [type];
            Description = description;
        }

        public NodeInputAttribute(string name, Type type)
        {
            Name = name;
            Type = [type];
        }

    }
}