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
using DynamicData;
using System.Linq;
using IO.SVGFigures;
using Logic.Graphics;

namespace IO
{
    public class IFigureConverter
    {
        public List<(IFigure, IDrawable)> getFigureList(List<(ConvertibleFigure, IDrawable)> ConvertibleFigures)
        {
            List<(IFigure, IDrawable)> ifigures = new List<(IFigure, IDrawable)>();

            FigureFabric figure_fabric = new FigureFabric();

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

            var svgDocument = SvgDocument.Open(filename);
            SVG svg_convert = new SVG();

            foreach (SvgElement svg_elem in svgDocument.Children)
            {
                switch (svg_elem)
                {
                    case SvgLine:
                        SvgLine? svg_line = svg_elem as SvgLine;
                        ConvertibleLine line = svg_convert.getLine(svg_line);

                        drawable = new DrawableConverter().getDrawable(svg_line);

                        deserializedFigures.Add((line, drawable));
                        break;

                    case SvgRectangle:
                        SvgRectangle? svg_rect = svg_elem as SvgRectangle;

                        // Проверка равеcтва cторон
                        if (svg_rect.Width == svg_rect.Height)
                        {
                            // Еcли cтороны равны - получим квадрат
                            ConvertibleSquare square = svg_convert.getSquare(svg_rect);

                            if (svg_rect.Fill != null)
                                square.IsFilled = true;


                            drawable = new DrawableConverter().getDrawable(svg_rect);

                            deserializedFigures.Add((square, drawable));
                        }
                        else
                        {
                            // Еcли cтороны разные - получим прямоугольник
                            ConvertibleRectangle rectangle = svg_convert.getRectangle(svg_rect);

                            if (svg_rect.Fill != null)
                                rectangle.IsFilled = true;

                            drawable = new DrawableConverter().getDrawable(svg_rect);

                            deserializedFigures.Add((rectangle, drawable));
                        }
                        break;

                    case SvgPolygon:
                        SvgPolygon? svg_polygon = svg_elem as SvgPolygon;

                        // Проверка чиcла точек
                        if (svg_polygon.Points.Count() == 8)
                        {
                            ConvertibleTriangle triangle = svg_convert.getTriangle(svg_polygon);

                            if (svg_polygon.Fill != null)
                                triangle.IsFilled = true;

                            drawable = new DrawableConverter().getDrawable(svg_polygon);

                            deserializedFigures.Add((triangle, drawable));
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

                        ConvertibleCircle circle = svg_convert.getCircle(svg_circle);

                        if (svg_circle.Fill != null)
                            circle.IsFilled = true;

                        drawable = new DrawableConverter().getDrawable(svg_circle);

                        deserializedFigures.Add((circle, drawable)); break;

                    case SvgEllipse:
                        SvgEllipse? svg_ellipse = svg_elem as SvgEllipse;
                        ConvertibleEllipse ellips = svg_convert.getEllipse(svg_ellipse);

                        if (svg_ellipse.Fill != null)
                            ellips.IsFilled = true;

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

                        break;

                    case ConvertibleRectangle:
                        ConvertibleRectangle c_rectangle = (ConvertibleRectangle)figure;

                        var rectangle = svg_convert.getSvgRectangle(c_rectangle);

                        rectangle = (SvgRectangle)svg_convert.ApplayDrawable(rectangle, drawable);


                        svg_doc.Children.Add(rectangle);

                        break;

                    case ConvertibleTriangle:
                        ConvertibleTriangle c_triangle = (ConvertibleTriangle)figure;

                        var triangle = svg_convert.getSvgTriangle(c_triangle);

                        triangle = (SvgPolygon)svg_convert.ApplayDrawable(triangle, drawable);

                        svg_doc.Children.Add(triangle);

                        break;

                    case ConvertibleSquare:
                        ConvertibleSquare c_square = (ConvertibleSquare)figure;

                        var square = svg_convert.getSvgSquare(c_square);

                        square = (SvgRectangle)svg_convert.ApplayDrawable(square, drawable);


                        svg_doc.Children.Add(square);

                        break;

                    case ConvertibleCircle:
                        ConvertibleCircle c_circle = (ConvertibleCircle)figure;

                        var circle = svg_convert.getSvgCircle(c_circle);

                        circle = (SvgCircle)svg_convert.ApplayDrawable(circle, drawable);


                        svg_doc.Children.Add(circle);

                        break;

                    case ConvertibleEllipse:
                        ConvertibleEllipse c_ellipse = (ConvertibleEllipse)figure;

                        var ellipse = svg_convert.getSvgEllipse(c_ellipse);

                        ellipse = (SvgEllipse)svg_convert.ApplayDrawable(ellipse, drawable);

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

