using System.Text;
using Interfaces;
using System.Runtime.Serialization.Json;
using Svg;
using DataStructures.Geometry;
using DataStructures;
using DataStructures.ConvertibleFigures;
using Color = DataStructures.Color;
using System.Collections.Generic;
using Geometry;
using Geometry.Figures;

namespace IO
{
    public class JSONConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<ConvertibleFigure>),
                        new Type[] {
                                typeof(ConvertibleLine),
                                typeof(ConvertibleSquare),
                                typeof(ConvertibleFilledCircle),
                                typeof(ConvertibleEllipse),
                                typeof(ConvertibleTriangle),
                                typeof(ConvertibleRectangle),
                                typeof(ConvertibleCircle),
                            });

            List<ConvertibleFigure>? deserializedFigures = ser.ReadObject(fs) as List<ConvertibleFigure>;
            fs.Close();

            if (deserializedFigures != null)
            {
                FigureConveter fc = new FigureConveter();

                List<IFigure> ifigures = fc.convertToIFigure(deserializedFigures);

                return ifigures;
            }
            else
                return Enumerable.Empty<IFigure>();
        }

        public void WriteFile(string filename, IEnumerable<ConvertibleFigure> figures)
        {
            FileStream stream = new FileStream(filename + ".json", FileMode.Create);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<ConvertibleFigure>),
                                                new Type[] {
                                                    typeof(ConvertibleLine),
                                                    typeof(ConvertibleSquare),
                                                    typeof(ConvertibleFilledCircle),
                                                    typeof(ConvertibleEllipse),
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
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            List<ConvertibleFigure> deserializedFigures = new List<ConvertibleFigure>();

            var svgDocument = SvgDocument.Open(filename);
            SVG svg_convert = new SVG();

            foreach (SvgElement svg_elem in svgDocument.Children)
            {
                switch (svg_elem)
                {
                    case SvgLine:
                        SvgLine? svg_line = svg_elem as SvgLine;
                        ConvertibleLine line = svg_convert.getLine(svg_line);
                        deserializedFigures.Add(line);
                        break;

                    case SvgRectangle:
                        SvgRectangle? svg_rect = svg_elem as SvgRectangle;

                        // Проверка равеcтва cторон
                        if (svg_rect.Width == svg_rect.Height)
                        {
                            // Еcли cтороны равны - получим квадрат
                            ConvertibleSquare square = svg_convert.getSquare(svg_rect);
                            deserializedFigures.Add(square);
                        }
                        else
                        {
                            // Еcли cтороны разные - получим прямоугольник
                            ConvertibleRectangle rectangle = svg_convert.getRectangle(svg_rect);
                            deserializedFigures.Add(rectangle);
                        }
                        break;

                    case SvgPolygon:
                        SvgPolygon? svg_polygon = svg_elem as SvgPolygon;

                        // Проверка чиcла точек
                        if (svg_polygon.Points.Count() == 8)
                        {
                            ConvertibleTriangle triangle = svg_convert.getTriangle(svg_polygon);
                            deserializedFigures.Add(triangle);
                        }
                        else
                        {
                            List<Point2d> points = new List<Point2d>();

                            for (int i = 0; i < svg_polygon.Points.Count(); i += 2)
                                points.Add(new Point2d((float)svg_polygon.Points[i], (float)svg_polygon.Points[i + 1]));

                        }

                        break;

                    case SvgCircle:
                        SvgCircle? svg_circle = svg_elem as SvgCircle;

                        if (svg_circle.Fill == SvgPaintServer.None)
                        {
                            ConvertibleCircle circle = svg_convert.getCircle(svg_circle);
                            deserializedFigures.Append(circle);
                        }
                        else
                        {
                            ConvertibleFilledCircle fill_circle = svg_convert.getFilledCircle(svg_circle);
                            deserializedFigures.Append(fill_circle);
                        }
                        break;

                    case SvgEllipse:
                        SvgEllipse? svg_ellipse = svg_elem as SvgEllipse;
                        ConvertibleEllipse ellips = svg_convert.getEllipse(svg_ellipse);
                        deserializedFigures.Append(ellips);
                        break;
                }
            }

            if (deserializedFigures != null)
            {
                FigureConveter fc = new FigureConveter();

                List<IFigure> ifigures = fc.convertToIFigure(deserializedFigures);

                return ifigures;
            }
            else
                return Enumerable.Empty<IFigure>();
        }


        public void WriteFile(string filename, IEnumerable<ConvertibleFigure> figures)
        {
            SvgDocument svg_doc = new SvgDocument { Width = 500, Height = 500 };
            SVG svg_convert = new SVG();


            foreach (ConvertibleFigure figure in figures)
            {

                switch (figure)
                {
                    case ConvertibleLine:
                        ConvertibleLine c_line = (ConvertibleLine)figure;

                        var line = svg_convert.getSvgLine(c_line);
                        svg_doc.Children.Add(line);

                        break;

                    case ConvertibleRectangle:
                        ConvertibleRectangle c_rectangle = (ConvertibleRectangle)figure;

                        var rectangle = svg_convert.getSvgRectangle(c_rectangle);
                        svg_doc.Children.Add(rectangle);

                        break;

                    case ConvertibleTriangle:
                        ConvertibleTriangle c_triangle = (ConvertibleTriangle)figure;

                        var triangle = svg_convert.getSvgTriangle(c_triangle);
                        svg_doc.Children.Add(triangle);

                        break;

                    case ConvertibleSquare:
                        ConvertibleSquare c_square = (ConvertibleSquare)figure;

                        var square = svg_convert.getSvgSquare(c_square);
                        svg_doc.Children.Add(square);

                        break;

                    case ConvertibleCircle:
                        ConvertibleCircle c_circle = (ConvertibleCircle)figure;

                        var circle = svg_convert.getSvgCircle(c_circle);
                        svg_doc.Children.Add(circle);

                        break;

                    case ConvertibleEllipse:
                        ConvertibleEllipse c_ellipse = (ConvertibleEllipse)figure;

                        var ellipse = svg_convert.getSvgEllipse(c_ellipse);
                        svg_doc.Children.Add(ellipse);

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
