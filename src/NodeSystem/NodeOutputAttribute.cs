namespace DEGG.NodeSystem
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NodeOutputAttribute : Attribute
    {
        public string Name { get; }
        public Type Type { get; }
        public string Description { get; } = "";

        public NodeOutputAttribute(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public NodeOutputAttribute(string name, string description, Type type)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NodeOutput2Attribute : Attribute
    {
        public string? Name { get; }
        public Type? Type { get; }
        public string Description { get; } = "";
        public NodeOutput2Attribute()
        {
        }

        public NodeOutput2Attribute(string name)
        {
            Name = name;
        }

        public NodeOutput2Attribute(string name, string description, Type type)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}