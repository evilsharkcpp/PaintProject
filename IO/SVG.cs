using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using ExCSS;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Color = System.Drawing.Color;

namespace IO
{
    public class SVG
    {
        DataStructures.Color color = new DataStructures.Color(1, 0, 0, 0);
        DataStructures.Color fill_color = new DataStructures.Color(1, 0, 0, 0);


        //
        // Получить конвертируемые фигуры
        //

        public ConvertibleLine getLine(SvgLine svg_line)
        {
            Point2d p1 = new Point2d((float)svg_line.StartX, (float)svg_line.StartY);
            Point2d p2 = new Point2d((float)svg_line.EndX, (float)svg_line.EndY);

            return new ConvertibleLine(p1, p2, color);
        }

        public ConvertibleEllipse getEllipse(SvgEllipse svg_ellipse)
        {
            Point2d center = new Point2d((double)svg_ellipse.CenterX, (double)svg_ellipse.CenterY);
            double radius_x = (double)svg_ellipse.RadiusX;
            double radius_y = (double)svg_ellipse.RadiusY;

            return new ConvertibleEllipse(center, radius_x, radius_y, color);
        }

        public ConvertibleFilledCircle getFilledCircle(SvgCircle svg_circle)
        {
            Point2d center = new Point2d((double)svg_circle.CenterX, (double)svg_circle.CenterY);
            double radius = (double)svg_circle.Radius;

            return new ConvertibleFilledCircle(center, radius, color, fill_color);
        }

        public ConvertibleCircle getCircle(SvgCircle svg_circle)
        {
            Point2d center = new Point2d((double)svg_circle.CenterX, (double)svg_circle.CenterY);
            double radius = (double)svg_circle.Radius;

            return new ConvertibleCircle(center, radius, color);
        }

        public ConvertibleRectangle getRectangle(SvgRectangle svg_rect)
        {
            Point2d p1 = new Point2d((float)svg_rect.X, (float)svg_rect.Y);
            double width = (float)svg_rect.Width;
            double height = (float)svg_rect.Height;

            return new ConvertibleRectangle(p1, width, height, color);
        }


        public ConvertibleSquare getSquare(SvgRectangle svg_square)
        {
            Point2d p1 = new Point2d((float)svg_square.X, (float)svg_square.Y);
            double width = (float)svg_square.Width;
            double height = (float)svg_square.Height;

            return new ConvertibleSquare(p1, width, height, color);
        }

        public ConvertibleTriangle getTriangle(SvgPolygon svg_polygon)
        {
            Point2d p1 = new Point2d((float)svg_polygon.Points[0], (float)svg_polygon.Points[1]);
            Point2d p2 = new Point2d((float)svg_polygon.Points[3], (float)svg_polygon.Points[4]);
            Point2d p3 = new Point2d((float)svg_polygon.Points[5], (float)svg_polygon.Points[6]);

            return new ConvertibleTriangle(p1, p2, p3, color);
        }




        //
        // Получить SVG фигуры
        //
        
        public SvgLine getSvgLine(ConvertibleLine c_line)
        {
            double x1 = c_line.point1.X;
            double y1 = c_line.point1.Y;
            double x2 = c_line.point2.X;
            double y2 = c_line.point2.Y;


            return new SvgLine
            {
                StartX = (SvgUnit)x1,
                StartY = (SvgUnit)y1,
                EndX = (SvgUnit)x2,
                EndY = (SvgUnit)y2,

                Stroke = new SvgColourServer()
            };
        }

        public SvgEllipse getSvgEllipse(ConvertibleEllipse c_ellipse)
        {

            return new SvgEllipse
            {
                CenterX = (SvgUnit)c_ellipse.center.X,
                CenterY = (SvgUnit)c_ellipse.center.Y,
                RadiusX = (SvgUnit)c_ellipse.radiusX,
                RadiusY = (SvgUnit)c_ellipse.radiusY,

                Stroke = new SvgColourServer(),
                Fill = new SvgColourServer(Color.FromArgb((int)(0), Color.Black)),
            };
        }


        public SvgCircle getSvgCircle(ConvertibleCircle c_circle)
        {
            double cx = c_circle.center.X;
            double cy = c_circle.center.Y;
            double r = c_circle.radius;

            return new SvgCircle
            {
                CenterX = (SvgUnit)cx,
                CenterY = (SvgUnit)cy,
                Radius = (SvgUnit)r,

                Stroke = new SvgColourServer(),
                Fill = new SvgColourServer(Color.FromArgb((int)(0), Color.Black)),
            };
        }

        public SvgRectangle getSvgRectangle(ConvertibleRectangle c_rect)
        {
            double x1 = c_rect.point1.X;
            double y1 = c_rect.point1.Y;

            double width = c_rect.width;
            double height = c_rect.height;


            return new SvgRectangle
            {
                X = new SvgUnit((float)x1),
                Y = new SvgUnit((float)y1),

                Width = new SvgUnit((float)width),
                Height = new SvgUnit((float)height),

                Stroke = new SvgColourServer(),
                Fill = new SvgColourServer(Color.FromArgb((int)(0), Color.Black)),
            };
        }

        public SvgRectangle getSvgSquare(ConvertibleSquare c_rect)
        {
            double x1 = c_rect.point1.X;
            double y1 = c_rect.point1.Y;

            double width = c_rect.width;
            double height = c_rect.height;


            return new SvgRectangle
            {
                X = new SvgUnit((float)x1),
                Y = new SvgUnit((float)y1),

                Width = new SvgUnit((float)width),
                Height = new SvgUnit((float)height),

                Stroke = new SvgColourServer(),
                Fill = new SvgColourServer(Color.FromArgb((int)(0), Color.Black)),
            };
        }



        public SvgPolygon getSvgTriangle(ConvertibleTriangle c_triangle)
        {
            double x1 = c_triangle.point1.X;
            double y1 = c_triangle.point1.Y;

            double x2 = c_triangle.point2.X;
            double y2 = c_triangle.point2.Y;

            double x3 = c_triangle.point3.X;
            double y3 = c_triangle.point3.Y;


            return new SvgPolygon
            {
                Points = new SvgPointCollection
                {
                    new SvgUnit((float)x1), new SvgUnit((float)y1),
                    new SvgUnit((float)x2), new SvgUnit((float)y2),
                    new SvgUnit((float)x3), new SvgUnit((float)y3)
                },


                Stroke = new SvgColourServer(),
                Fill = new SvgColourServer(Color.FromArgb((int)(0), Color.Black)),
            };
        }



    }
}
