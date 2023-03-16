using System.Text;
using Interfaces;
using System.Runtime.Serialization.Json;
using DynamicData;
using Geometry.Figures;
using ExCSS;
using Svg;
using System.Xml.Linq;
using IO.ConvertedFigures;
using System.Data.SqlTypes;
using System.IO;
using DataStructures.Geometry;
using DataStructures;
using DataStructures.ConvertibleFigures;
using Color = DataStructures.Color;

namespace IO
{
    public class JSONConverter : IConverter
    {
        public IEnumerable<ConvertibleFigure> ReadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<ConvertibleFigure>), 
                        new Type[] {
                                typeof(ConvertibleLine),
                                typeof(ConvertibleSquare),
                                typeof(ConvertibleFilledCircle),
                                typeof(ConvertibleEllips),
                                typeof(ConvertibleTriangle),
                                typeof(ConvertibleRectangle),
                                typeof(ConvertibleCircle),
                            });

            IEnumerable<ConvertibleFigure>? deserializedFigures = ser.ReadObject(fs) as IEnumerable<ConvertibleFigure>;
            fs.Close();

            if (deserializedFigures != null)
                return deserializedFigures;
            else
                return Enumerable.Empty<ConvertibleFigure>();
        }

        public void WriteFile(string filename, IEnumerable<ConvertibleFigure> figures)
        {
            FileStream stream = new FileStream(filename + ".json", FileMode.Create);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<ConvertibleFigure>),
                                                new Type[] {
                                                    typeof(ConvertibleLine),
                                                    typeof(ConvertibleSquare),
                                                    typeof(ConvertibleFilledCircle),
                                                    typeof(ConvertibleEllips),
                                                    typeof(ConvertibleTriangle),
                                                    typeof(ConvertibleRectangle),
                                                    typeof(ConvertibleCircle),
                                                });
            ser.WriteObject(stream, figures);
            stream.Close();
        }

    }

    public class SVGConverter : IConverter
    {
        public IEnumerable<ConvertibleFigure> ReadFile(string filename)
        {
            List<ConvertibleFigure> deserializedFigures = new List<ConvertibleFigure>();

            var svgDocument = SvgDocument.Open(filename);

            Point2d p1, p2, p3, center;
            Color color = new Color(0,0,0,1);

            double radius, width, height;

            foreach (SvgElement svg_elem in svgDocument.Children)
            {
                switch (svg_elem)
                {
                    case SvgLine:
                        SvgLine? svg_line = svg_elem as SvgLine;

                        p1 = new Point2d((float)svg_line.StartX, (float)svg_line.StartY);
                        p2 = new Point2d((float)svg_line.EndX, (float)svg_line.EndY);


                        ConvertibleLine line = new ConvertibleLine(p1, p2, color);
                        
                        deserializedFigures.Add(line);

                        break;

                    case SvgRectangle:
                        SvgRectangle? svg_rect = svg_elem as SvgRectangle;

                        p1 = new Point2d((float)svg_rect.X, (float)svg_rect.Y);

                        width = (float)svg_rect.Width;
                        height = (float)svg_rect.Height;

                        // Проверка равеCтва Cторон
                        if (svg_rect.Width == svg_rect.Height)
                        {
                            // ЕCли Cтороны равны - получим квадрат
                            ConvertibleSquare square = new ConvertibleSquare(p1, width, height, color);
                            deserializedFigures.Add(square);
                        }
                        else
                        {
                            // ЕCли Cтороны разные - получим прямоугольник
                            ConvertibleRectangle rectangle = new ConvertibleRectangle(p1, width, height, color);
                            deserializedFigures.Add(rectangle);
                        }
                        break;

                    case SvgPolygon:
                        SvgPolygon? svg_polygon = svg_elem as SvgPolygon;

                        // Проверка чиCла точек
                        if (svg_polygon.Points.Count() == 8)
                        {
                            // ЕCли 8 чиCел (3 точки + 1 точка для замыкания) - поучим треугольник
                            p1 = new Point2d((float)svg_polygon.Points[0], (float)svg_polygon.Points[1]);
                            p2 = new Point2d((float)svg_polygon.Points[3], (float)svg_polygon.Points[4]);
                            p3 = new Point2d((float)svg_polygon.Points[5], (float)svg_polygon.Points[6]);

                            ConvertibleTriangle triangle = new ConvertibleTriangle(p1, p2, p3, color);
                            deserializedFigures.Add(triangle);
                        }
                        else
                        {
                            List<Point2d> points = new List<Point2d>();

                            for (int i = 0; i < svg_polygon.Points.Count(); i += 2)
                                points.Add(new Point2d((float)svg_polygon.Points[i], (float)svg_polygon.Points[i+1]));

                            // Пока не CущеCтвует
/*                          Poligon poligon = new Poligon();
                            deserializedFigures.Add(poligon);*/
                        }

                        break;

                    case SvgCircle:
                        SvgCircle? svg_circle = svg_elem as SvgCircle;

                        center = new Point2d((double)svg_circle.CenterX, (double)svg_circle.CenterY);
                        radius = (double)svg_circle.Radius;

                        if (svg_circle.Fill == SvgPaintServer.None)
                        {
                            // Непубличный клаCC
                            // Circle circle = new Circle(center, radius);
                            // deserializedFigures.Append(circle);
                        }
                        else
                        {
                            // Непубличный клаCC
                            // FilledCircle filled_circle = new FilledCircle(center, radius);
                            // deserializedFigures.Append(filled_circle);
                        }


                        break;

                    case SvgEllipse:
                        SvgEllipse? svg_ellips = svg_elem as SvgEllipse;

                        center = new Point2d((double)svg_ellips.CenterX, (double)svg_ellips.CenterY);
                        double radius_x = (double)svg_ellips.RadiusX;
                        double radius_y = (double)svg_ellips.RadiusY;



                        // Непубличный клаCC
                        // Ellipse ellipse = new SvgEllipse();
                        // deserializedFigures.Append(ellipse);
                        break;
                }
            }

            if (deserializedFigures != null)
                return deserializedFigures;
            else
                return Enumerable.Empty<ConvertibleFigure>();
        }


        public void WriteFile(string filename, IEnumerable<ConvertibleFigure> figures)
        {
            SvgDocument svg_doc = new SvgDocument{ Width = 500, Height = 500};

            foreach (ConvertibleFigure figure in figures)
            {
                
                switch (figure)
                {
                    case ConvertibleLine:
                        ConvertibleLine c_line = (ConvertibleLine)figure;

                        double x1 = c_line.point1.X;
                        double y1 = c_line.point1.Y;
                        double x2 = c_line.point2.X;
                        double y2 = c_line.point2.Y;


/*                        var line = new SVGLine().Line(x1, y1, x2, y2);
                        svg_doc.Children.Add(line);*/
                        break;

                    case ConvertibleRectangle:

                        break;

                    case ConvertibleTriangle:

                        break;

                    case ConvertibleSquare:

                        break;

                    case ConvertibleCircle:

                        break;

                    case ConvertibleEllips:

                        break;

                    case ConvertibleFilledCircle:

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
