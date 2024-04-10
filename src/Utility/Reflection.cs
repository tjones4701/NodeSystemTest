namespace ConsoleApp1.src.Utility
{

    public static class Reflection
    {

        public static List<OfT> GetAttributes<T, OfT>() where OfT : Attribute
        {
            return GetAttributes<OfT>(typeof(T));
        }
        public static List<OfT> GetAttributes<OfT>(Type t) where OfT : Attribute
        {
            List<Attribute> attributes = Attribute.GetCustomAttributes(t, true).ToList();
            return attributes.OfType<OfT>().ToList();
        }
        public static List<OfT> GetAttributes<OfT>(object t) where OfT : Attribute
        {
            return GetAttributes<OfT>(t.GetType());
        }

        public static OfT? GetAttribute<T, OfT>() where OfT : Attribute
        {
            return GetAttribute<OfT>(typeof(T));
        }
        public static OfT? GetAttribute<OfT>(Type t) where OfT : Attribute
        {
            return GetAttributes<OfT>(t).FirstOrDefault();
        }
        public static OfT? GetAttribute<OfT>(object t) where OfT : Attribute
        {
            return GetAttribute<OfT>(t.GetType());
        }

        public static List<System.Reflection.PropertyInfo> GetProperties<T>(object t)
        {
            IEnumerable<System.Reflection.PropertyInfo> props = t.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(T)));

            return props.ToList();

        }
    }
}
