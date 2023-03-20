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
using DataStructures;
using DataStructures.ConvertibleFigures;
using Color = DataStructures.Color;
using System.Collections.Generic;
using Geometry;
using Geometry.Figures;
using DynamicData;
using System.Linq;
using IO.SVGFigures;
using Logic.Graphics;

namespace IO
{
    public class JSONConverter : IConverter
    {
        public List<(IFigure, IDrawable)> getFigureList(List<(ConvertibleFigure, IDrawable)> ConvertibleFigures)
        {
            List<(IFigure, IDrawable)> ifigures = new List<(IFigure, IDrawable)>();

        //    IEnumerable<IFigure>? deserializedFigures = ser.ReadObject(fs) as IEnumerable<IFigure>;
        //    fs.Close();

            foreach ((ConvertibleFigure figure, IDrawable drawable) in ConvertibleFigures)
                ifigures.Add((figure_fabric.CreateFigureFromConvertibleFigure(figure), drawable));

            return ifigures;
        }

        public List<(ConvertibleFigure, IDrawable)> getConvertibleFigureList(IEnumerable<(IFigure, IDrawable)> Figures)
        {
            List<(ConvertibleFigure, IDrawable)> c_figures = new List<(ConvertibleFigure, IDrawable)>();

            foreach ((IFigure figure, IDrawable drawable) in Figures)
                c_figures.Add((figure.ToConvertibleFigure(), drawable));

            return c_figures;
        }
    }

        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>),
        //                                        new Type[] { 
        //                                            typeof(Line),
        //                                            typeof(Rectangle),
        //                                            typeof(Triangle),
        //                                            typeof(Square),
        //                                        });
        //    ser.WriteObject(stream, figures);
        //    stream.Close();
        //}

    public class JSONConverter : IConverter
    {
        public IEnumerable<(IFigure, IDrawable)> ReadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<(ConvertibleFigure, IDrawable)>),
                        new Type[] {
                                typeof(ConvertibleLine),
                                typeof(ConvertibleSquare),
                                typeof(ConvertibleEllipse),
                                typeof(ConvertibleTriangle),
                                typeof(ConvertibleRectangle),
                                typeof(ConvertibleCircle),
                                typeof(Drawable),

                            });

            List<(ConvertibleFigure, IDrawable)>? deserializedFigures = ser.ReadObject(fs) as List<(ConvertibleFigure, IDrawable)>;
            fs.Close();

            if (deserializedFigures != null)
            {
                List<(IFigure, IDrawable)> ifigures = new IFigureConverter().getFigureList(deserializedFigures);

                return ifigures;
            }
            else
                return Enumerable.Empty<(IFigure, IDrawable)>();
        }

        public void WriteFile(string filename, IEnumerable<(IFigure, IDrawable)> figures)
        {
            List<(ConvertibleFigure, IDrawable)>? c_figures = new IFigureConverter().getConvertibleFigureList(figures);

            FileStream stream = new FileStream(filename + ".json", FileMode.Create);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<(ConvertibleFigure, IDrawable)>),
                                                new Type[] {
                                                    typeof(ConvertibleLine),
                                                    typeof(ConvertibleSquare),
                                                    typeof(ConvertibleEllipse),
                                                    typeof(ConvertibleTriangle),
                                                    typeof(ConvertibleRectangle),
                                                    typeof(ConvertibleCircle),
                                                    typeof(Drawable),
                                                });

            ser.WriteObject(stream, c_figures);
            stream.Close();
        }
    }

    public class SVGConverter : IConverter
    {
        public IEnumerable<(IFigure, IDrawable)> ReadFile(string filename)
        {
            List<(ConvertibleFigure, IDrawable)> deserializedFigures = new List<(ConvertibleFigure, IDrawable)>();

            IDrawable drawable;

//            var svgDocument = SvgDocument.Open(filename);

//            Point2d p1, p2, p3, center;
//            double radius;

                        drawable = new DrawableConverter().getDrawable(svg_line);

                        deserializedFigures.Add((line, drawable));
                        break;

//                        p1 = new Point2d((float)svg_line.StartX, (float)svg_line.StartY);
//                        p2 = new Point2d((float)svg_line.EndX, (float)svg_line.EndY);

//                        Line line = new Line(p1, p2);
//                        deserializedFigures.Add(line);

//                        break;


                            drawable = new DrawableConverter().getDrawable(svg_rect);

                            deserializedFigures.Add((square, drawable));
                        }
                        else
                        {
                            // Еcли cтороны разные - получим прямоугольник
                            ConvertibleRectangle rectangle = svg_convert.getRectangle(svg_rect);

//                        // Проверка равества сторон
//                        if (svg_rect.Width == svg_rect.Height)
//                        {
//                            // Если стороны равны - получим квадрат
//                            Square square = new Square(p1, p2);
//                            deserializedFigures.Add(square);
//                        }
//                        else
//                        {
//                            // Если стороны разные - получим прямоугольник
//                            Rectangle rectangle = new Rectangle(p1, p2);
//                            deserializedFigures.Add(rectangle);
//                        }
//                        break;

                            drawable = new DrawableConverter().getDrawable(svg_rect);

                            deserializedFigures.Add((rectangle, drawable));
                        }
                        break;

//                        // Проверка числа точек
//                        if (svg_polygon.Points.Count() == 8)
//                        {
//                            // Если 8 чисел (3 точки + 1 точка для замыкания) - поучим треугольник
//                            p1 = new Point2d((float)svg_polygon.Points[0], (float)svg_polygon.Points[1]);
//                            p2 = new Point2d((float)svg_polygon.Points[3], (float)svg_polygon.Points[4]);
//                            p3 = new Point2d((float)svg_polygon.Points[5], (float)svg_polygon.Points[6]);

                            drawable = new DrawableConverter().getDrawable(svg_polygon);

                            deserializedFigures.Add((triangle, drawable));
                        }
                        else
                        {
                            List<Point2d> points = new List<Point2d>();

//                            for (int i = 0; i < svg_polygon.Points.Count(); i += 2)
//                                points.Add(new Point2d((float)svg_polygon.Points[i], (float)svg_polygon.Points[i+1]));

//                            // Пока не существует
///*                          Poligon poligon = new Poligon();
//                            deserializedFigures.Add(poligon);*/
//                        }

//                        break;

//                    case "SvgCircle":
//                        SvgCircle? svg_circle = svg_elem as SvgCircle;

//                        center = new Point2d((double)svg_circle.CenterX, (double)svg_circle.CenterY);
//                        radius = (double)svg_circle.Radius;

//                        if (svg_circle.Fill == SvgPaintServer.None)
//                        {
//                            // Непубличный класс
//                            // Circle circle = new Circle(center, radius);
//                            // deserializedFigures.Append(circle);
//                        }
//                        else
//                        {
//                            // Непубличный класс
//                            // FilledCircle filled_circle = new FilledCircle(center, radius);
//                            // deserializedFigures.Append(filled_circle);
//                        }

                        drawable = new DrawableConverter().getDrawable(svg_circle);

                        deserializedFigures.Add((circle, drawable)); break;

                    case SvgEllipse:
                        SvgEllipse? svg_ellipse = svg_elem as SvgEllipse;
                        ConvertibleEllipse ellips = svg_convert.getEllipse(svg_ellipse);

//                    case "SvgEllips":
//                        SvgEllipse? svg_ellips = svg_elem as SvgEllipse;

                        drawable = new DrawableConverter().getDrawable(svg_ellipse);

                        deserializedFigures.Add((ellips, drawable)); break;
                }
            }

            if (deserializedFigures != null)
            {
                List<(IFigure, IDrawable)> ifigures = new IFigureConverter().getFigureList(deserializedFigures);

                return ifigures;
            }
            else
                return Enumerable.Empty<(IFigure, IDrawable)>();
        }

//                        // Непубличный класс
//                        // Ellipse ellipse = new SvgEllipse();
//                        // deserializedFigures.Append(ellipse);
//                        break;
//                }
//            }

        public void WriteFile(string filename, IEnumerable<(IFigure, IDrawable)> figures)
        {
            SvgDocument svg_doc = new SvgDocument { Width = 500, Height = 500 };
            SVG svg_convert = new SVG();

            List<(ConvertibleFigure, IDrawable)> c_Figures = new IFigureConverter().getConvertibleFigureList(figures);


            foreach ((ConvertibleFigure figure, IDrawable drawable) in c_Figures)
            {

                switch (figure)
                {
                    case ConvertibleLine:
                        ConvertibleLine c_line = (ConvertibleLine)figure;

                        var line = svg_convert.getSvgLine(c_line);

                        line = (SvgLine)svg_convert.ApplayDrawable(line, drawable);

                        svg_doc.Children.Add(line);


//                        var line = new SVGLine().Line(x1, y1, x2, y2);
//                        svg_doc.Children.Add(line);
//                        break;

                        var rectangle = svg_convert.getSvgRectangle(c_rectangle);

                        rectangle = (SvgRectangle)svg_convert.ApplayDrawable(rectangle, drawable);


                        svg_doc.Children.Add(rectangle);

//                        break;

//                    case "Triangle":

                        var triangle = svg_convert.getSvgTriangle(c_triangle);

                        triangle = (SvgPolygon)svg_convert.ApplayDrawable(triangle, drawable);

                        svg_doc.Children.Add(triangle);

//                    case "Square":

//                        break;

                        var square = svg_convert.getSvgSquare(c_square);

                        square = (SvgRectangle)svg_convert.ApplayDrawable(square, drawable);


                        svg_doc.Children.Add(square);

//                        break;

//                    case "Ellips":

                        var circle = svg_convert.getSvgCircle(c_circle);

                        circle = (SvgCircle)svg_convert.ApplayDrawable(circle, drawable);


                        svg_doc.Children.Add(circle);

//                    case "FilledCicrle":

//                        break;
//                }

                        var ellipse = svg_convert.getSvgEllipse(c_ellipse);

                        ellipse = (SvgEllipse)svg_convert.ApplayDrawable(ellipse, drawable);

                        svg_doc.Children.Add(ellipse);

//                string svg_string = Encoding.UTF8.GetString(stream.GetBuffer());

//                using (StreamWriter writer = new StreamWriter(filename + ".svg"))
//                {
//                    writer.Write(svg_string);
//                }

//            }

//        }

        public void WriteFile(string filename, IEnumerable<(IFigure, IDrawable)> figures)
        {
            throw new NotImplementedException();
        }

        IEnumerable<(IFigure, IDrawable)> IConverter.ReadFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}

