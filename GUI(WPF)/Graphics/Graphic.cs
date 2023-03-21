using DataStructures.Geometry;
using Geometry.Figures;
using Geometry.Transforms;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
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
            _gGroup = new GeometryGroup();
            _gGroup.Children.Add(new EllipseGeometry()
            {
                Center = new Point(start.X,start.Y),
                RadiusX = a, 
                RadiusY = b,
                Transform = new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                                ModelMatrix.M12, ModelMatrix.M22,
                                                ModelMatrix.M13, ModelMatrix.M23)

            });

            _canvas.Children.Add(new System.Windows.Shapes.Path()
            {
                Data = _gGroup,
                Stroke = (GraphicStyle.IsNoOutLine) ? null : new SolidColorBrush(c),
                Fill = (GraphicStyle.IsNoFill) ? null : new SolidColorBrush(c2),
            });
        }

        public void DrawLine(Point2d v1, Point2d v2, bool isFill, bool isOutLine)
        {
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
            _gGroup = new GeometryGroup();
            _gGroup.Children.Add(new LineGeometry()
            {
                StartPoint = new Point(v1.X, v1.Y),
                EndPoint = new Point(v2.X, v2.Y),
                Transform = new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                                ModelMatrix.M12, ModelMatrix.M22,
                                                ModelMatrix.M13, ModelMatrix.M23)

            });

            _canvas.Children.Add(new System.Windows.Shapes.Path()
            {
                Data = _gGroup,
                Stroke = (GraphicStyle.IsNoOutLine) ? null : new SolidColorBrush(c),
                Fill = (GraphicStyle.IsNoFill) ? null : new SolidColorBrush(c2),
            });
        }

        public void DrawPolygon(IEnumerable<Point2d> points, bool isFill, bool isOutLine)
        {

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
            _gGroup = new GeometryGroup();
            StreamGeometry streamGeometry = new StreamGeometry();
            streamGeometry.Transform = new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                                           ModelMatrix.M12, ModelMatrix.M22,
                                                           ModelMatrix.M13, ModelMatrix.M23);
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                var p = points.First();
                geometryContext.BeginFigure(new Point(p.X, p.Y), true, true);
                PointCollection pointsc = new PointCollection();
                foreach(var item in points)
                {
                    pointsc.Add(new Point(item.X, item.Y));
                }
                pointsc.RemoveAt(0);
                geometryContext.PolyLineTo(pointsc, true, true);
            }
            _gGroup.Children.Add(streamGeometry);

            _canvas.Children.Add(new System.Windows.Shapes.Path()
            {
                Data = _gGroup,
                Stroke = (GraphicStyle.IsNoOutLine) ? null : new SolidColorBrush(c),
                Fill = (GraphicStyle.IsNoFill) ? null : new SolidColorBrush(c2),
            });
        }

        public void DrawTriangle(Point2d v1, Point2d v2, Point2d v3, bool isFill, bool isOutLine)
        {
            List<Point2d> points = new List<Point2d> { v1, v2, v3 };
            DrawPolygon(points, isFill, isOutLine);
        }
        private GeometryGroup _gGroup;
        public void DrawRectangle(Point2d start, double a, double b, bool isFill, bool isOutLine)
        {
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
            _gGroup = new GeometryGroup();
            _gGroup.Children.Add(new RectangleGeometry()
            {
                Rect = new System.Windows.Rect()
                {
                    Location = new Point(start.X, start.Y),
                    Width = a,
                    Height = b,
                },
                Transform = new MatrixTransform(ModelMatrix.M11, ModelMatrix.M21,
                                                ModelMatrix.M12, ModelMatrix.M22,
                                                ModelMatrix.M13, ModelMatrix.M23)

            });

            _canvas.Children.Add(new System.Windows.Shapes.Path()
            {
                Data = _gGroup,
                Stroke = (GraphicStyle.IsNoOutLine) ? null : new SolidColorBrush(c),
                Fill = (GraphicStyle.IsNoFill) ? null : new SolidColorBrush(c2),
            });
        }

        public Graphic() { }

        public Graphic(Canvas canvas)
        {
            _canvas = canvas;
        }
    }
}
