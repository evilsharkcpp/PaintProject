using Interfaces;
using System.Reflection;

namespace Geometry.Figures
{
    public class FigureFabric
    {
        public static Assembly Assembly { get; private set; }
        public static Dictionary<string, Type> AvailableFigures { get; private set; }

        static FigureFabric()
        {
            AvailableFigures = new Dictionary<string, Type>();
            Assembly = typeof(FigureFabric).Assembly;
            Type[] types = Assembly.GetTypes();
            foreach (Type type in types)
            {
                IEnumerable<Attribute> attributes = type.GetCustomAttributes();
                foreach (Attribute attribute in attributes)
                {
                    if (attribute is FigureAttribute figureAttribute)
                    {
                        AvailableFigures.Add(figureAttribute.Name, type);
                        continue;
                    }
                }
            }
        }

        public static FigureFabric Create()
        {
            return new FigureFabric();
        }

        public IFigure? CreateFigure(string figureTypeName, params object[] objects)
        {
            IFigure? figure = null;
            if (AvailableFigures.TryGetValue(figureTypeName, out var figureType))
            {
                try
                {
                    figure = (IFigure?)Activator.CreateInstance(figureType, objects);
                }
                catch (Exception)
                {

                }
            }

            return figure;
        }
    }
}
