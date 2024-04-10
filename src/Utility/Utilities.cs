namespace DEGG.NodeSystem
{
    public static partial class Utilities
    {


        public static object? ConvertToType(object? val, Type type)
        {
            if (val == null)
            {
                return null;
            }

            if (val.GetType() == type)
            {
                return val;
            }
            if (type == typeof(float))
            {
                return ToNumber(val);
            }
            if (type == typeof(bool))
            {
                return ToBoolean(val);
            }
            Type otherType = val.GetType();

            // Check if otherType is castable to type
            if (type.IsAssignableFrom(otherType))
            {
                return val;
            }
            return null;
        }
        public static float ToNumber(object? val)
        {
            if (val == null)
            {
                return 0;
            }
            if (val is int)
            {
                return (int)val;
            }
            if (val is float)
            {
                return (float)val;
            }
            if (val is double)
            {
                return (float)(double)val;
            }
            if (val is string)
            {
                return float.Parse((string)val);
            }
            try
            {
                return float.Parse(val?.ToString() ?? "0");
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static bool ToBoolean(object? val)
        {
            if (val == null)
            {
                return false;
            }

            if (val is bool)
            {
                return (bool)val;
            }
            if (val is float f)
            {
                return f != 0;
            }

            try
            {
                return Boolean.Parse(val?.ToString() ?? "0");
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
