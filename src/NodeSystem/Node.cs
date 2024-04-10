using ConsoleApp1.src.Utility;

namespace DEGG.NodeSystem
{

    public class Node
    {
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
            List<NodeInputAttribute> inputAttributes = Reflection.GetAttributes<NodeInputAttribute>(this);
            foreach (NodeInputAttribute attribute in inputAttributes)
            {
                AddInput(attribute.Name);
            }

            // Get a list of all the attributes of type NodeInputAttribute
            List<NodeSettingAttribute> settingAttributes = Reflection.GetAttributes<NodeSettingAttribute>(this);
            foreach (NodeSettingAttribute attribute in settingAttributes)
            {
                Settings[attribute.Name.ToUpper()] = new NodeSetting(attribute);
            }

            // Get a list of all the attributes of type NodeInputAttribute
            List<System.Reflection.PropertyInfo> testAttributes = Reflection.GetProperties<NodeInput2Attribute>(this);
            foreach (System.Reflection.PropertyInfo property in testAttributes)
            {
                NodeInput2Attribute? attribute = property.GetCustomAttributes(true).OfType<NodeInput2Attribute>().FirstOrDefault();
                if (attribute != null)
                {
                    InputNodeConnector input = new InputNodeConnector { Name = name, Parent = this };
                    if (property.DeclaringType != null)
                    {
                        input.ValidTypes.Add(property.DeclaringType);
                    }
                    AddInput(nodeInput);
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
            NodeConnector? existing = GetInputByName(name);
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
            InputNodeConnector? input = Inputs.FirstOrDefault(x => x.Name == name);
            return input;
        }
        public NodeConnector? GetOutputByName(string name)
        {
            return Outputs.FirstOrDefault(x => x.Name == name);
        }



        public T? GetValue<T>()
        {
            if (Value is T val)
            {
                return val;
            }
            return default;
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
            return input.GetValue<T>();
        }

        public void SetValue(object value)
        {
            bool hasChanged = !EqualityComparer<object>.Default.Equals(Value, value);
            Value = value;
            if (hasChanged)
            {
                TriggerChange();
            }
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

        public Dictionary<string, T?> GetValueOfInputs<T>()
        {
            Dictionary<string, T?> values = new Dictionary<string, T?>();
            for (int i = 0; i < Inputs.Count; i++)
            {
                InputNodeConnector input = Inputs[i];
                if (input.IsConnected())
                {
                    values[Inputs[i].Name] = Inputs[i].GetValue<T>();
                }
            }
            return values;
        }

        public bool Execute()
        {
            if (!IsSetup)
            {
                return false;
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
