using DataStructures.Geometry;
using Interfaces;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GUI_WPF.Graphics
{
    public class Graphic : IGraphics
    {
        public IDrawable GraphicStyle { get; set; }

        private Canvas _canvas;

        public void DrawCircle(Point2d center, double radius, bool isFill, bool isOutLine)
        {
        }

        public void DrawEllipse(Point2d center, double a, double b, bool isFill, bool isOutLine)
        {
            throw new System.NotImplementedException();
        }

        public void DrawLine(Point2d v1, Point2d v2, bool isFill, bool isOutLine)
        {
            var line = new Line();
            line.X1 = v1.X;
            line.Y1 = v1.Y;
            line.X2 = v2.X;
            line.Y2 = v2.Y;
            var c = new Color();
            c.A = GraphicStyle.OutLineColor.A;
            c.R = GraphicStyle.OutLineColor.R;
            c.G = GraphicStyle.OutLineColor.G;
            c.B = GraphicStyle.OutLineColor.B;
            line.Stroke = new SolidColorBrush(c);
            _canvas.Children.Add(line);
        }

        public void DrawPolygon(IEnumerable<Point2d> points, bool isFill, bool isOutLine)
        {
            throw new System.NotImplementedException();
        }

        public void DrawTriangle(Point2d v1, Point2d v2, Point2d v3, bool isFill, bool isOutLine)
        {
            throw new System.NotImplementedException();
        }
        public Graphic() { }

        public Graphic(Canvas canvas)
        {
            _canvas = canvas;
        }
    }
}
