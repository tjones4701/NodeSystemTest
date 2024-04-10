namespace DEGG.NodeSystem
{
    public class NodeSetting
    {
        public NodeSettingAttribute Parent { get; set; }

        public string Name => Parent?.Name ?? "ERROR";
        public Type Type => Parent.Type;
        public object? Value { get; set; }

        public NodeSetting(NodeSettingAttribute parent)
        {
            Parent = parent;
        }

        public bool SetValue(object? value)
        {
            if (value?.GetType() == Type || value == null)
            {
                Value = value;
                return true;
            }
            return false;
        }

        public T? GetValue<T>()
        {
            if (Value is T val)
            {
                return val;
            }
            return default;
        }
    }
}
