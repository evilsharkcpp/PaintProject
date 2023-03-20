using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Geometry.Figures;
using Interfaces;
using Splat;
using System.Reflection;

namespace Geometry
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
                if (typeof(IFigure).IsAssignableFrom(type))
                {
                    IEnumerable<Attribute> attributes = type.GetCustomAttributes();
                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute is FigureAttribute figureAttribute)
                        {
                            AvailableFigures.Add(figureAttribute.Name, type);
                            break;
                        }
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

        public IFigure CreateFigureFromConvertibleFigure(ConvertibleFigure c_figure)
        {
            IFigure figure;

            string name = c_figure.GetType().Name;

            if (c_figure.IsFilled)
                name = "Filled" + name;

            figure = CreateFigure(name);

            double width = c_figure.Width;
            double heigth = c_figure.Height;

            figure.Size = new Vector2d(width, heigth);

            // Перенос точки позиции из левого верхнего угла в левый нижний
            Point2d c_position = c_figure.position;
            c_position.Y += c_figure.Height;

            figure.Position = c_position;
            figure.Angle = c_figure.angle;


            return figure;
        }
    }
}
