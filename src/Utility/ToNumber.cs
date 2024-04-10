namespace DEGG.NodeSystem
{
    public static partial class Utilities
    {
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
    }
}
