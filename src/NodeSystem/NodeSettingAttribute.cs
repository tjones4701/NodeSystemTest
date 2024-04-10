namespace DEGG.NodeSystem
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NodeSettingAttribute : Attribute
    {
        public string Name { get; }
        public Type Type { get; }
        public string Description { get; } = "";

        public NodeSettingAttribute(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public NodeSettingAttribute(string name, string description, Type type)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}