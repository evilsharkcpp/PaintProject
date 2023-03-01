using DataStructures.Geometry;
using Interfaces;
using ReactiveUI;
using System.Reflection;

namespace Geometry.Parameterization
{
    public abstract class ParameterizedObject : ReactiveObject
    {
        private enum PropertyType
        {
            Double = 1,
            Point2d,
            Vector2d
        }


        private static readonly Dictionary<Type, Dictionary<PropertyType, IList<ParameterInfo>>> _parameterInfos = new Dictionary<Type, Dictionary<PropertyType, IList<ParameterInfo>>>();

        private static void TryAddType(Type type)
        {
            Dictionary<PropertyType, IList<ParameterInfo>> parameterInfos = new Dictionary<PropertyType, IList<ParameterInfo>>();
            if (_parameterInfos.TryAdd(type, parameterInfos))
            {
                IList<ParameterInfo> doubleParameters = new List<ParameterInfo>();
                IList<ParameterInfo> point2dParameters = new List<ParameterInfo>();
                IList<ParameterInfo> vector2dParameters = new List<ParameterInfo>();

                parameterInfos.Add(PropertyType.Double, doubleParameters);
                parameterInfos.Add(PropertyType.Point2d, point2dParameters);
                parameterInfos.Add(PropertyType.Vector2d, vector2dParameters);

                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    PropertyType propertyType;
                    if (property.PropertyType == typeof(double))
                    {
                        propertyType = PropertyType.Double;
                    }
                    else if (property.PropertyType == typeof(Point2d))
                    {
                        propertyType = PropertyType.Point2d;
                    }
                    else if (property.PropertyType == typeof(Vector2d))
                    {
                        propertyType = PropertyType.Vector2d;
                    }
                    else
                    {
                        continue;
                    }

                    object[] attributes = property.GetCustomAttributes(true);
                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute is ParameterAttribute parameterAttribute)
                        {
                            switch (propertyType)
                            {
                                case PropertyType.Double:
                                    doubleParameters.Add(new ParameterInfo(property.Name,
                                                                           property.GetMethod,
                                                                           property.SetMethod));
                                    break;

                                case PropertyType.Point2d:
                                    point2dParameters.Add(new ParameterInfo(property.Name,
                                                                            property.GetMethod,
                                                                            property.SetMethod));
                                    break;

                                case PropertyType.Vector2d:
                                    vector2dParameters.Add(new ParameterInfo(property.Name,
                                                                             property.GetMethod,
                                                                             property.SetMethod));
                                    break;
                            }

                            continue;
                        }
                    }
                }
            }
        }



        public IEnumerable<IParameter<double>> DoubleParameters { get; protected set; }
        public IEnumerable<IParameter<Point2d>> PointParameters { get; protected set; }
        public IEnumerable<IParameter<Vector2d>> VectorParameters { get; protected set; }

        public ParameterizedObject()
        {
            Type type = GetType();
            TryAddType(type);
            Dictionary<PropertyType, IList<ParameterInfo>> parameterInfos = _parameterInfos[type];

            List<IParameter<double>> doubleParameters = new List<IParameter<double>>();
            List<IParameter<Point2d>> point2dParameters = new List<IParameter<Point2d>>();
            List<IParameter<Vector2d>> vector2dParameters = new List<IParameter<Vector2d>>();

            DoubleParameters = doubleParameters;
            PointParameters = point2dParameters;
            VectorParameters = vector2dParameters;

            foreach (ParameterInfo parameterInfo in parameterInfos[PropertyType.Double])
            {
                doubleParameters.Add(new Parameter<double>(parameterInfo.Name,
                                                           parameterInfo.GetMethod?.CreateDelegate<Getter<double>>(this),
                                                           parameterInfo.SetMethod?.CreateDelegate<Setter<double>>(this)));
            }

            foreach (ParameterInfo parameterInfo in parameterInfos[PropertyType.Point2d])
            {
                point2dParameters.Add(new Parameter<Point2d>(parameterInfo.Name,
                                                             parameterInfo.GetMethod?.CreateDelegate<Getter<Point2d>>(this),
                                                             parameterInfo.SetMethod?.CreateDelegate<Setter<Point2d>>(this)));
            }

            foreach (ParameterInfo parameterInfo in parameterInfos[PropertyType.Vector2d])
            {
                vector2dParameters.Add(new Parameter<Vector2d>(parameterInfo.Name,
                                                               parameterInfo.GetMethod?.CreateDelegate<Getter<Vector2d>>(this),
                                                               parameterInfo.SetMethod?.CreateDelegate<Setter<Vector2d>>(this)));
            }
        }
    }
}
