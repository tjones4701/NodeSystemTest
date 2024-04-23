using ConsoleApp1.src.Utility;

namespace DEGG.NodeSystem
{
    public class Node
    {
        public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public NodeNetwork? Network { get; set; }
        public List<InputNodeConnector> Inputs { get; set; } = new();
        public List<NodeConnector> Outputs { get; set; } = new();

        public Dictionary<string, NodeSetting> Settings { get; set; } = new();
        public object? Value { get; set; }
        public bool IsSetup { get; set; } = false;
        // Custom getter for NodeInformationAttribute
        public NodeInformationAttribute NodeInformation => Reflection.GetAttribute<NodeInformationAttribute>(this) ?? new NodeInformationAttribute("Unknown", "Unknown");

        public T? GetSetting<T>(string key)
        {
            string keyUpper = key.ToUpper();
            if (Settings.ContainsKey(keyUpper))
            {
                return Settings[keyUpper].GetValue<T>();
            }
            return default;
        }

        public bool SetSetting<T>(string key, T? value)
        {
            string keyUpper = key.ToUpper();
            if (Settings.ContainsKey(keyUpper))
            {
                NodeSetting setting = Settings[keyUpper];
                T? oldValue = setting.GetValue<T>();
                if (EqualityComparer<T>.Default.Equals(oldValue, value))
                {
                    return false;
                }
                bool result = setting.SetValue(value);

                if (result)
                {
                    OnSettingchange(setting);
                }
                return result;

            }
            return false;
        }

        public virtual void OnSettingchange(NodeSetting setting)
        {
        }
        public void Setup()
        {
            if (IsSetup)
            {
                return;
            }

            Logging.Log("Setting up node: " + this + " of type " + GetType().Name);

            // Get a list of all the attributes of type NodeOutputAttribute
            List<NodeOutputAttribute> outputAttributes = Reflection.GetAttributes<NodeOutputAttribute>(this);
            foreach (NodeOutputAttribute attribute in outputAttributes)
            {
                AddOutput(attribute.Name);
            }


            // Get a list of all the attributes of type NodeInputAttribute
            List<NodeSettingAttribute> settingAttributes = Reflection.GetAttributes<NodeSettingAttribute>(this);
            foreach (NodeSettingAttribute attribute in settingAttributes)
            {
                Settings[attribute.Name.ToUpper()] = new NodeSetting(attribute);
            }

            // Get a list of all the attributes of type NodeInputAttribute
            List<System.Reflection.PropertyInfo> testAttributes = Reflection.GetProperties<NodeInputAttribute>(this);
            foreach (System.Reflection.PropertyInfo property in testAttributes)
            {
                NodeInputAttribute? attribute = property.GetCustomAttributes(true).OfType<NodeInputAttribute>().FirstOrDefault();
                if (attribute != null)
                {
                    InputNodeConnector input = new InputNodeConnector(this, attribute, property);
                    if (property.DeclaringType != null)
                    {
                        input.ValidTypes.Add(property.DeclaringType);
                    }
                    AddInput(input);
                }
            }

            OnSetup();
            IsSetup = true;
        }
        public virtual void OnSetup()
        {

        }

        public void AddInput(InputNodeConnector input)
        {
            Inputs.Add(input);
        }

        public Dictionary<string, object?> GetValues()
        {
            Dictionary<string, object?> outputs = new Dictionary<string, object?>();
            foreach (NodeConnector output in Outputs)
            {
                outputs[output.Name] = output.Value;
            }

            return outputs;

        }

        public InputNodeConnector AddInput<T>(string name) where T : InputNodeConnector, new()
        {
            InputNodeConnector? existing = GetInputByName(name);
            if (existing != null)
            {
                return existing;
            }
            T input = new T { Name = name, Parent = this };
            Inputs.Add(input);
            return input;
        }

        public NodeConnector AddOutput(string name)
        {
            return AddOutput<NodeConnector>(name);
        }

        public void Connect(Node other, string inputName, string outputName)
        {
            NodeConnector? output = other.GetOutputByName(outputName);
            InputNodeConnector? input = GetInputByName(inputName);
            if (output == null)
            {
                throw new InvalidOperationException($"Output {outputName} not found on node {this}");
            }
            if (input == null)
            {
                throw new InvalidOperationException($"Input {inputName} not found on node {other}");
            }

            output.Connect(input);
        }


        public NodeConnector AddOutput<T>(string name) where T : NodeConnector, new()
        {
            NodeConnector? existing = GetOutputByName(name);
            if (existing != null)
            {
                return existing;
            }

            T output = new T { Name = name, Parent = this };
            Outputs.Add(output);
            return output;
        }

        public InputNodeConnector? GetInputByName(string name)
        {
            name = name.ToUpper();
            InputNodeConnector? input = Inputs.FirstOrDefault(x => (x.Name?.ToUpper() == name || x?.Attribute?.Name?.ToUpper() == name || x?.PropertyInfo?.Name?.ToUpper() == name));
            return input;
        }
        public NodeConnector? GetOutputByName(string name)
        {
            return Outputs.FirstOrDefault(x => x.Name == name);
        }

        public object? GetValueOfInput(string name)
        {
            return GetValueOfInput<object>(name);
        }

        public T? GetValueOfInput<T>(string name)
        {
            InputNodeConnector? input = GetInputByName(name);
            if (input == null)
            {
                throw new InvalidOperationException($"Input {name} not found on node {this}");
            }
            object? obj = input?.PropertyInfo?.GetValue(this);
            if (obj == null)
            {
                return default;
            }
            return (T?)Utilities.ConvertToType(obj, typeof(T));
        }

        public void SetValue(string name, object value)
        {
            NodeConnector? outputConnector = GetOutputByName(name);
            outputConnector?.SetValue(value);
        }

        public void SelfExecute()
        {
            Network?.Add(this);
        }

        public void TriggerChange()
        {
            for (int i = 0; i < Outputs.Count; i++)
            {
                Outputs[i].OnChange();
            }
        }

        public bool Execute()
        {
            if (!IsSetup)
            {
                return false;
            }
            foreach (InputNodeConnector i in Inputs)
            {
                if (!i.IsConnected())
                {
                    return false;
                }
                Type? propertyType = i.PropertyInfo?.PropertyType;
                if (propertyType == null)
                {
                    continue;
                }
                object? data = Utilities.ConvertToType(i.GetValue(), propertyType);
                i.PropertyInfo?.SetValue(this, data);
            }

            return OnExecute();
        }

        /**
         * This method is called when the node is executed.
         * It should return true if the node has finished executing.
         * If it returns false then the node will be added back to the end of network and executed again later.
         **/
        public virtual bool OnExecute()
        {
            return true;
        }

        public void Tick()
        {
            if (!IsSetup)
            {
                return;
            }
            OnTick();
        }

        public virtual void OnTick()
        {

        }
    }
}
