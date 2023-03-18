using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Geometry.Figures;
using Geometry;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class FigureConveter
    {
        public List<IFigure> convertToIFigure(List<ConvertibleFigure> convertibleFigures)
        {
            List<IFigure> ifigures = new List<IFigure>();

            FigureFabric figure_fabric = new FigureFabric();
            IFigure ifigure;

            foreach (ConvertibleFigure figure in convertibleFigures)
            {

                switch (figure)
                {
                    case ConvertibleLine:
                        ConvertibleLine c_line = (ConvertibleLine)figure;

                        ifigure = figure_fabric.CreateFigure("Line");

                        double width = c_line.point2.X - c_line.point1.X;
                        double heigth = c_line.point2.Y - c_line.point1.Y;

                        ifigure.Size = new Vector2d(width, heigth);
                        ifigure.Position = new Point2d(c_line.point1.X, c_line.point2.Y);


                        ifigures.Add(ifigure);
                        break;

                    case ConvertibleRectangle:
                        ConvertibleRectangle c_rectangle = (ConvertibleRectangle)figure;

                        ifigure = figure_fabric.CreateFigure("Rectangle");

                        width = c_rectangle.width;
                        heigth = c_rectangle.height;

                        ifigure.Size = new Vector2d(width, heigth);
                        ifigure.Position = c_rectangle.point1;


                        ifigures.Append(ifigure);
                        break;

                    case ConvertibleTriangle:
                        ConvertibleTriangle c_triangle = (ConvertibleTriangle)figure;

                        ifigure = figure_fabric.CreateFigure("Triangle");

                        width = c_triangle.point3.X - c_triangle.point1.X;
                        heigth = c_triangle.point2.Y - c_triangle.point1.Y;

                        ifigure.Size = new Vector2d(width, heigth);
                        ifigure.Position = new Point2d(c_triangle.point1.X, c_triangle.point2.Y);
                        


                        ifigures.Append(ifigure);
                        break;
                    case ConvertibleSquare:
                        ConvertibleSquare c_square = (ConvertibleSquare)figure;

                        ifigure = figure_fabric.CreateFigure("Square");

                        width = c_square.width;
                        heigth = c_square.height;

                        ifigure.Size = new Vector2d(width, heigth);
                        ifigure.Position = c_square.point1;


                        ifigures.Append(ifigure);
                        break;
                    case ConvertibleCircle:
                        ConvertibleCircle c_circle = (ConvertibleCircle)figure;

                        ifigure = figure_fabric.CreateFigure("Circle");

                        width = c_circle.radius * 2;
                        heigth = c_circle.radius * 2;

                        ifigure.Size = new Vector2d(width, heigth);
                        ifigure.Position = new Point2d(c_circle.center.X - c_circle.radius, c_circle.center.Y - c_circle.radius);


                        ifigures.Append(ifigure);
                        break;

                    case ConvertibleEllipse:
                        ConvertibleEllipse c_ellipse = (ConvertibleEllipse)figure;

                        ifigure = figure_fabric.CreateFigure("Ellipse");

                        width = c_ellipse.radiusX * 2;
                        heigth = c_ellipse.radiusY * 2;

                        ifigure.Size = new Vector2d(width, heigth);
                        ifigure.Position = new Point2d(c_ellipse.center.X - c_ellipse.radiusX, c_ellipse.center.Y - c_ellipse.radiusY);


                        ifigures.Append(ifigure);
                        break;

                }

            }

            return ifigures;

        }
    }
}
