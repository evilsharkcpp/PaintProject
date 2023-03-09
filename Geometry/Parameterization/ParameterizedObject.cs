using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using ReactiveUI;
using System.Reflection;

namespace Geometry.Parameterization
{
    public abstract class ParameterizedObject : ReactiveObject
    {
        private static readonly Dictionary<Type, IList<ParameterInfo>> _parameterInfos = new();

        private static void TryAddType(Type type)
        {
            IList<ParameterInfo> parameterInfos = new List<ParameterInfo>();
            if (_parameterInfos.TryAdd(type, parameterInfos))
            { 
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object[] attributes = property.GetCustomAttributes(true);
                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute is ParameterAttribute parameterAttribute)
                        {
                            parameterInfos.Add(new ParameterInfo(parameterAttribute.Name,
                                                                 property.PropertyType,
                                                                 property.GetMethod,
                                                                 property.SetMethod));

                            break;
                        }
                    }
                }
            }
        }




        private readonly IReadOnlyList<IParameter<object>> _extraProperties;

        public IReadOnlyList<IParameter<object>> ExtraProperties => _extraProperties;

        public ParameterizedObject()
        {
            Type type = GetType();
            TryAddType(type);

            IList<ParameterInfo> parameterInfos = _parameterInfos[type];
            List<IParameter<object>> extraProperties = new List<IParameter<object>>();
            _extraProperties = extraProperties;

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                extraProperties.Add(new Parameter<object>(parameterInfo.Name,
                                                          parameterInfo.GetMethod?.CreateDelegate<Getter<object>>(this),
                                                          parameterInfo.SetMethod?.CreateDelegate<Setter<object>>(this)));
            }
        }
    }
}
