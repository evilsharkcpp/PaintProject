using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


using Interfaces;
using System.Runtime.Serialization.Json;
using Splat.ModeDetection;
using System.Runtime.ConstrainedExecution;
using DynamicData;
using System.Runtime.Serialization;
using System.Xml;
using Geometry.Figures;
using ExCSS;
using Svg;
using System.Xml.Linq;
using IO.SVGFigures;
using System.Data.SqlTypes;
using System.IO;
using DataStructures.Geometry;

namespace IO
{
    public class JSONConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>), 
                        new Type[] {
                                typeof(Line),
                                typeof(Rectangle),
                                typeof(Triangle),
                                typeof(Square),
                            });

            IEnumerable<IFigure>? deserializedFigures = ser.ReadObject(fs) as IEnumerable<IFigure>;
            fs.Close();

            if (deserializedFigures != null)
                return deserializedFigures;
            else
                return Enumerable.Empty<IFigure>();
        }

        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {
            FileStream stream = new FileStream(filename + ".json", FileMode.Create);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>),
                                                new Type[] { 
                                                    typeof(Line),
                                                    typeof(Rectangle),
                                                    typeof(Triangle),
                                                    typeof(Square),
                                                });
            ser.WriteObject(stream, figures);
            stream.Close();
        }

    }

    public class SVGConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            List<IFigure> deserializedFigures = new List<IFigure>();

            var svgDocument = SvgDocument.Open(filename);

            Point2d p1, p2, p3, center;
            double radius;

            foreach (SvgElement svg_elem in svgDocument.Children)
            {
                switch (svg_elem.GetType().Name)
                {
                    case "SvgLine":
                        SvgLine? svg_line = svg_elem as SvgLine;

                        p1 = new Point2d((float)svg_line.StartX, (float)svg_line.StartY);
                        p2 = new Point2d((float)svg_line.EndX, (float)svg_line.EndY);

                        Line line = new Line(p1, p2);
                        deserializedFigures.Add(line);

                        break;

                    case "SvgRectangle":
                        SvgRectangle? svg_rect = svg_elem as SvgRectangle;

                        p1 = new Point2d((float)svg_rect.X, (float)svg_rect.Y);
                        p2 = new Point2d(p1.X + (float)svg_rect.Width, p1.Y + (float)svg_rect.Height);

                        // Проверка равества сторон
                        if (svg_rect.Width == svg_rect.Height)
                        {
                            // Если стороны равны - получим квадрат
                            Square square = new Square(p1, p2);
                            deserializedFigures.Add(square);
                        }
                        else
                        {
                            // Если стороны разные - получим прямоугольник
                            Rectangle rectangle = new Rectangle(p1, p2);
                            deserializedFigures.Add(rectangle);
                        }
                        break;

                    case "SvgPolygon":
                        SvgPolygon? svg_polygon = svg_elem as SvgPolygon;

                        // Проверка числа точек
                        if (svg_polygon.Points.Count() == 8)
                        {
                            // Если 8 чисел (3 точки + 1 точка для замыкания) - поучим треугольник
                            p1 = new Point2d((float)svg_polygon.Points[0], (float)svg_polygon.Points[1]);
                            p2 = new Point2d((float)svg_polygon.Points[3], (float)svg_polygon.Points[4]);
                            p3 = new Point2d((float)svg_polygon.Points[5], (float)svg_polygon.Points[6]);

                            Triangle triangle = new Triangle(p1, p2, p3);
                            deserializedFigures.Add(triangle);
                        }
                        else
                        {
                            List<Point2d> points = new List<Point2d>();

                            for (int i = 0; i < svg_polygon.Points.Count(); i += 2)
                                points.Add(new Point2d((float)svg_polygon.Points[i], (float)svg_polygon.Points[i+1]));

                            // Пока не существует
/*                          Poligon poligon = new Poligon();
                            deserializedFigures.Add(poligon);*/
                        }

                        break;

                    case "SvgCircle":
                        SvgCircle? svg_circle = svg_elem as SvgCircle;

                        center = new Point2d((double)svg_circle.CenterX, (double)svg_circle.CenterY);
                        radius = (double)svg_circle.Radius;

                        // Непубличный класс
                        // Circle circle = new Circle(center, radius);
                        // deserializedFigures.Append(circle);
                        break;

                    case "SvgEllips":
                        SvgEllipse? svg_ellips = svg_elem as SvgEllipse;

                        center = new Point2d((double)svg_ellips.CenterX, (double)svg_ellips.CenterY);
                        double radius_x = (double)svg_ellips.RadiusX;
                        double radius_y = (double)svg_ellips.RadiusY;



                        // Непубличный класс
                        // Ellipse ellipse = new SvgEllipse();
                        // deserializedFigures.Append(ellipse);
                        break;
                }
            }

            if (deserializedFigures != null)
                return deserializedFigures;
            else
                return Enumerable.Empty<IFigure>();
        }


        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {
            SvgDocument svg_doc = new SvgDocument{ Width = 500, Height = 500};

            foreach (IFigure figure in figures)
            {
                
                switch (figure.GetType().Name)
                {
                    case "Line":
                        double x1 = figure.PointParameters.ElementAt(0).Value.X;
                        double y1 = figure.PointParameters.ElementAt(0).Value.Y;
                        double x2 = figure.PointParameters.ElementAt(1).Value.X;
                        double y2 = figure.PointParameters.ElementAt(1).Value.Y;


                        var line = new SVGLine().Line(x1, y1, x2, y2);
                        svg_doc.Children.Add(line);
                        break;

                    case "Rectangle":

                        break;

                    case "Triangle":

                        break;

                    case "Square":

                        break;

                    case "Cicle":

                        break;

                    case "Ellips":

                        break;

                    case "FilledCicrle":

                        break;
                }

                MemoryStream stream = new MemoryStream();
                svg_doc.Write(stream);

                string svg_string = Encoding.UTF8.GetString(stream.GetBuffer());

                using (StreamWriter writer = new StreamWriter(filename + ".svg"))
                {
                    writer.Write(svg_string);
                }

            }

        }

    }
}
