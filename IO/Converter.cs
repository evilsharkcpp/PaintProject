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
using Drawing.Graphics;
using System.Drawing.Imaging;

namespace IO
{
    public class IFigureConverter
    {
        public List<IDrawableObject> getFigureList(List<(ConvertibleFigure, IDrawable)> ConvertibleFigures)
        {
            List<IDrawableObject> ifigures = new List<IDrawableObject>();

            FigureFabric figure_fabric = new FigureFabric();

            foreach ((ConvertibleFigure figure, IDrawable drawable) in ConvertibleFigures)
            {
                IFigure f = figure_fabric.CreateFigureFromConvertibleFigure(figure);
                IDrawable d = drawable;

                DrawableObject d_obj = new DrawableObject();
                d_obj.Drawable = d;
                d_obj.Figure = f;

                ifigures.Add(d_obj);
            }
            return ifigures;
        }

        public List<(ConvertibleFigure, IDrawable)> getConvertibleFigureList(IEnumerable<IDrawableObject> Figures)
        {
            List<(ConvertibleFigure, IDrawable)> c_figures = new List<(ConvertibleFigure, IDrawable)>();

            foreach (IDrawableObject figure in Figures)
                c_figures.Add((figure.Figure.ToConvertibleFigure(), figure.Drawable));

            return c_figures;
        }
    }


    public class JSONConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            var ser = new DataContractJsonSerializer(typeof(List<(ConvertibleFigure, IDrawable)>),
                        new Type[] {
                                typeof(ConvertibleLine),
                                typeof(ConvertibleSquare),
                                typeof(ConvertibleEllipse),
                                typeof(ConvertibleTriangle),
                                typeof(ConvertibleRectangle),
                                typeof(ConvertibleCircle),
                                typeof(Drawable),

                            });

            List<(ConvertibleFigure, IDrawable)>? deserializedFigures = ser.ReadObject(stream) as List<(ConvertibleFigure, IDrawable)>;

            if (deserializedFigures != null)
            {
                List<IDrawableObject> ifigures = new IFigureConverter().getFigureList(deserializedFigures);

                return ifigures;
            }
            else
                return Enumerable.Empty<IDrawableObject>();
        }

        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            List<(ConvertibleFigure, IDrawable)>? c_figures = new IFigureConverter().getConvertibleFigureList(figures);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<(ConvertibleFigure, IDrawable)>),
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
        }

    }

    public class SVGConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            List<(ConvertibleFigure, IDrawable)> deserializedFigures = new List<(ConvertibleFigure, IDrawable)>();

            IDrawable drawable;

            var svgDocument = SvgDocument.Open<SvgDocument>(stream);
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
                List<IDrawableObject> ifigures = new IFigureConverter().getFigureList(deserializedFigures);

                return ifigures;
            }
            else
                return Enumerable.Empty<IDrawableObject>();
        }


        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            SVG svg_convert = new SVG();

            SvgDocument svg_doc = svg_convert.getSvgDocument(figures);

            MemoryStream ms = new MemoryStream();
            svg_doc.Write(ms);

            stream.Write(ms.GetBuffer());
        }
    }

    public class PNGConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            SVG svg_convert = new SVG();

            SvgDocument svg_doc = svg_convert.getSvgDocument(figures);
            svg_doc.Draw().Save(stream, ImageFormat.Png);
        }
    }

    public class JPEGConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            SVG svg_convert = new SVG();

            SvgDocument svg_doc = svg_convert.getSvgDocument(figures);
            svg_doc.Draw().Save(stream, ImageFormat.Jpeg);
        }
    }

    public class BMPConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            SVG svg_convert = new SVG();

            SvgDocument svg_doc = svg_convert.getSvgDocument(figures);
            svg_doc.Draw().Save(stream, ImageFormat.Bmp);
        }
    }

    public class GIFConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            SVG svg_convert = new SVG();

            SvgDocument svg_doc = svg_convert.getSvgDocument(figures);
            svg_doc.Draw().Save(stream, ImageFormat.Gif);
        }
    }

    public class TIFFConverter : IConverter
    {
        public IEnumerable<IDrawableObject> ReadFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(Stream stream, IEnumerable<IDrawableObject> figures)
        {
            SVG svg_convert = new SVG();

            SvgDocument svg_doc = svg_convert.getSvgDocument(figures);
            svg_doc.Draw().Save(stream, ImageFormat.Tiff);
        }
    }
}

