using System.Reflection;

namespace RPN
{
    public class InstanceFactory
    {
        public Dictionary<string, T> CreateDictionary<T>() where T : InterfaceDictKey
        {
            var typesImplementingInterface = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

            var instanceDictionary = new Dictionary<string, T>();

            foreach (var type in typesImplementingInterface)
            {
                var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
                if (defaultConstructor != null)
                {
                    var instance = (T)defaultConstructor.Invoke(null);
                    if (instance != null && !string.IsNullOrWhiteSpace(instance.Symbol))
                    {
                        instanceDictionary[instance.Symbol] = instance;
                    }
                }
            }

            return instanceDictionary;
        }
    }
}