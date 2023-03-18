using DataStructures.Geometry;
using Geometry.Figures;
using Geometry.Transforms;
using Interfaces;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GUI_WPF.Graphics
{
    public class Graphic : IGraphics
    {
        public IDrawable GraphicStyle { get; set; }

        public Matrix3d ModelMatrix { get; set; }

      private Canvas _canvas;

        public void DrawEllipse(Point2d start, double a, double b, bool isFill, bool isOutLine)
        {
            var elllipse = new System.Windows.Shapes.Ellipse();
            elllipse.Height = a;
            elllipse.Width = b;
            var c = new Color();
            var c2 = new Color();
            c.A = GraphicStyle.OutLineColor.A;
            c.R = GraphicStyle.OutLineColor.R;
            c.G = GraphicStyle.OutLineColor.G;
            c.B = GraphicStyle.OutLineColor.B;

            c2.A = GraphicStyle.FillColor.A;
            c2.R = GraphicStyle.FillColor.R;
            c2.G = GraphicStyle.FillColor.G;
            c2.B = GraphicStyle.FillColor.B;
            elllipse.StrokeThickness = 0.01;
            if (!GraphicStyle.IsNoOutLine)
                elllipse.Stroke = new SolidColorBrush(c);

            if (!GraphicStyle.IsNoFill)
                elllipse.Fill = new SolidColorBrush(c2);
            elllipse.RenderTransform = new TransformGroup()
            {
                Children = new TransformCollection(new Transform[]
                {
                    new TranslateTransform(start.X, start.Y),
                    new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                        ModelMatrix.M12, ModelMatrix.M22,
                                        ModelMatrix.M13, ModelMatrix.M23)
                })
            };
            _canvas.Children.Add(elllipse);
        }

        public void DrawLine(Point2d v1, Point2d v2, bool isFill, bool isOutLine)
        {
            var line = new System.Windows.Shapes.Line();
            line.X1 = v1.X;
            line.Y1 = v1.Y;
            line.X2 = v2.X;
            line.Y2 = v2.Y;
            var c = new Color();
            var c2 = new Color();
            c.A = GraphicStyle.OutLineColor.A;
            c.R = GraphicStyle.OutLineColor.R;
            c.G = GraphicStyle.OutLineColor.G;
            c.B = GraphicStyle.OutLineColor.B;

            c2.A = GraphicStyle.FillColor.A;
            c2.R = GraphicStyle.FillColor.R;
            c2.G = GraphicStyle.FillColor.G;
            c2.B = GraphicStyle.FillColor.B;
            line.StrokeThickness = 0.01;
            if (!GraphicStyle.IsNoOutLine)
                line.Stroke = new SolidColorBrush(c);

            if (!GraphicStyle.IsNoFill)
                line.Fill = new SolidColorBrush(c2);
            //var trans = new TransformGroup();
            //trans.Children.Add(new ScaleTransform(ModelMatrix.M11, ModelMatrix.M12));
            //trans.Children.Add(new RotateTransform(ModelMatrix.M21, ModelMatrix.M22,s));
            //trans.Children.Add(new TranslateTransform(ModelMatrix.M31, ModelMatrix.M32));
            //line.StrokeThickness = 0.01;
            //line.Stroke = new SolidColorBrush(c);
            line.RenderTransform = new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                                           ModelMatrix.M12, ModelMatrix.M22,
                                                           ModelMatrix.M13, ModelMatrix.M23);
            _canvas.Children.Add(line);
        }

        public void DrawPolygon(IEnumerable<Point2d> points, bool isFill, bool isOutLine)
        {
            Polygon poly = new Polygon();
            PointCollection polyPoints = new PointCollection();
            foreach (var p in points)
                polyPoints.Add(new Point(p.X, p.Y));
            poly.Points = polyPoints;
            poly.StrokeThickness = 0.01;
            var c = new Color();
            var c2 = new Color();
            c.A = GraphicStyle.OutLineColor.A;
            c.R = GraphicStyle.OutLineColor.R;
            c.G = GraphicStyle.OutLineColor.G;
            c.B = GraphicStyle.OutLineColor.B;

            c2.A = GraphicStyle.FillColor.A;
            c2.R = GraphicStyle.FillColor.R;
            c2.G = GraphicStyle.FillColor.G;
            c2.B = GraphicStyle.FillColor.B;
            if (!GraphicStyle.IsNoOutLine)
                poly.Stroke = new SolidColorBrush(c);

            if (!GraphicStyle.IsNoFill)
                poly.Fill = new SolidColorBrush(c2);
            poly.RenderTransform = new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                                           -ModelMatrix.M12, -ModelMatrix.M22,
                                                           ModelMatrix.M13, ModelMatrix.M23);
            _canvas.Children.Add(poly);
        }

        public void DrawTriangle(Point2d v1, Point2d v2, Point2d v3, bool isFill, bool isOutLine)
        {
            List<Point2d> points = new List<Point2d> { v1, v2, v3 };
            DrawPolygon(points, isFill, isOutLine);
        }

        public void DrawRectangle(Point2d start, double a, double b, bool isFill, bool isOutLine)
        {
            //start = start * ModelMatrix;
            var rect = new System.Windows.Shapes.Rectangle();
            rect.Height = a;
            rect.Width = b;
            var c = new Color();
            var c2 = new Color();
            c.A = GraphicStyle.OutLineColor.A;
            c.R = GraphicStyle.OutLineColor.R;
            c.G = GraphicStyle.OutLineColor.G;
            c.B = GraphicStyle.OutLineColor.B;
            
            c2.A = GraphicStyle.FillColor.A;
            c2.R = GraphicStyle.FillColor.R;
            c2.G = GraphicStyle.FillColor.G;
            c2.B = GraphicStyle.FillColor.B;
            rect.StrokeThickness = 0.01;
            if (!GraphicStyle.IsNoOutLine)
                rect.Stroke = new SolidColorBrush(c);
            
            if(!GraphicStyle.IsNoFill)
                rect.Fill = new SolidColorBrush(c2);
            //rect. 

            rect.RenderTransform = new TransformGroup()
            {
                Children = new TransformCollection(new Transform[]
                {
                    new TranslateTransform(start.X, start.Y),
                    new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                        ModelMatrix.M12, ModelMatrix.M22,
                                        ModelMatrix.M13, ModelMatrix.M23)
                })
            };
            _canvas.Children.Add(rect);
        }

        public Graphic() { }

        public Graphic(Canvas canvas)
        {
            _canvas = canvas;
        }
    }
}
