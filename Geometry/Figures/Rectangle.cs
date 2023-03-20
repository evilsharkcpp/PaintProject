using DataStructures.ConvertibleFigures;
using DataStructures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Rectangle")]
    public class Rectangle : Figure
    {
        protected static Point2d Postition;
        protected static double Width = 2;
        protected static double Height = 2;

        static Rectangle()
        {
            Postition.X = -Width / 2.0;
            Postition.Y = Height / 2.0;
        }

        public Rectangle() { }

        public Rectangle(Rectangle rectangle) : base(rectangle) { }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawRectangle(Postition, Width, Height, true, false);
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            return Postition.X + Math.Abs(p.X) <= eps &&
                   Postition.Y - Math.Abs(p.Y) >= -eps;
        }

        public override IFigure Clone()
        {
            return new Rectangle(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            Point2d convertible_position = Postition;
            
            // В ConvertibleFigure позиция определяется по левой верхней точке
            convertible_position.Y -= Height;

            return new ConvertibleRectangle(convertible_position, Width, Height, Angle);
        }
    }
}
