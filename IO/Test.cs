using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Drawing.Graphics;
using Geometry;
using Geometry.Figures;
using Interfaces;
using Logic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class TestIO
    {
        public void TestStarter()
        {
            test__create_two_line__JSON();
            test__read_two_line__JSON();
            test__create_two_line__SVG();
            test__read_two_line__SVG();

            test__create_square__SVG();
            test__create_rectangle__SVG();
            test__create_circle__SVG();
            test__create_ellipse__SVG();
            test__create_triangle__SVG();
        }

        public void test__create_two_line__JSON()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(1, 1);

            var line = new ConvertibleLine(p1, p2, 45);

            var i_line = new FigureFabric().CreateFigureFromConvertibleFigure(line);

            Point2d p21 = new Point2d(5, 7);
            Point2d p22 = new Point2d(3, 8);

            var line2 = new ConvertibleLine(p21, p22, 45);

            var i_line2 = new FigureFabric().CreateFigureFromConvertibleFigure(line2);


            IDrawable d1 = new Drawable();
            
            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(1, 26, 123, 143);

            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_line, d1), (i_line2, d2) };

            JSONConverter jc = new JSONConverter();

            jc.WriteFile("two_lines", array);
        }

        public void test__create_two_line__SVG()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(1, 1);

            var line = new ConvertibleLine(p1, p2, 45);

            var i_line = new FigureFabric().CreateFigureFromConvertibleFigure(line);

            Point2d p21 = new Point2d(5, 7);
            Point2d p22 = new Point2d(3, 8);

            var line2 = new ConvertibleLine(p21, p22, 45);

            var i_line2 = new FigureFabric().CreateFigureFromConvertibleFigure(line2);


            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(1, 26, 123, 143);

            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_line, d1), (i_line2, d2) };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("two_lines", array);
        }

        public void test__create_square__SVG()
        {
            Point2d p1 = new Point2d(200, 200);

            var square0 = new ConvertibleSquare(p1, width: 100, height: 100, 0);
            var square = new ConvertibleSquare(p1, width: 100, height: 100, 35);

            var i_square0 = new FigureFabric().CreateFigureFromConvertibleFigure(square0);
            var i_square = new FigureFabric().CreateFigureFromConvertibleFigure(square);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);

            d1.IsNoFill = false;
            d1.FillColor = new Color(1, 115, 26, 26);


            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_square, d1), (i_square0, d1) };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("square", array);
        }

        public void test__create_rectangle__SVG()
        {
            Point2d p1 = new Point2d(200, 200);

            var rectangle0 = new ConvertibleRectangle(p1, width: 100, height: 300, 0);

            var rectangle = new ConvertibleRectangle(p1, width: 100, height: 300, 40);

            var i_rectangle0 = new FigureFabric().CreateFigureFromConvertibleFigure(rectangle0);
            var i_rectangle = new FigureFabric().CreateFigureFromConvertibleFigure(rectangle);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(1, 115, 26, 26);


            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_rectangle, d1), (i_rectangle0, d1) };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("rectangle", array);
        }

        public void test__create_triangle__SVG()
        {
            Point2d p1 = new Point2d(10, 100);
            Point2d p2 = new Point2d(60, 20);
            Point2d p3 = new Point2d(110, 100);

            var triangle0 = new ConvertibleTriangle(p1, p2, p3, 0);

            var triangle = new ConvertibleTriangle(p1, p2, p3, 270);

            var i_triangle0 = new FigureFabric().CreateFigureFromConvertibleFigure(triangle0);
            var i_triangle = new FigureFabric().CreateFigureFromConvertibleFigure(triangle);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(1, 75, 26, 26);


            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_triangle, d1), (i_triangle0, d1) };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("triangle", array);
        }

        public void test__create_circle__SVG()
        {
            Point2d p1 = new Point2d(100, 100);

            var circle = new ConvertibleCircle(p1, radius: 50, 0);

            var i_circle = new FigureFabric().CreateFigureFromConvertibleFigure(circle);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(1, 65, 26, 26);

            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_circle, d1) };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("circle", array);
        }


        public void test__create_ellipse__SVG()
        {
            Point2d p1 = new Point2d(150, 150);

            var circle0 = new ConvertibleEllipse(p1, radiusX: 100, radiusY: 50, 0);

            var circle = new ConvertibleEllipse(p1, radiusX: 100, radiusY: 50, 45);

            var i_circle0 = new FigureFabric().CreateFigureFromConvertibleFigure(circle0);
            var i_circle = new FigureFabric().CreateFigureFromConvertibleFigure(circle);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(1, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(1, 75, 26, 26);

            IEnumerable<(IFigure, IDrawable)> array = new List<(IFigure, IDrawable)>() { (i_circle, d1), (i_circle0, d1) };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("ellipse", array);
        }


        public void test__read_two_line__JSON()
        {
            JSONConverter jc = new JSONConverter();

            IEnumerable<(IFigure, IDrawable)> figures = jc.ReadFile("two_lines.json");

            Console.WriteLine("конец...");

        }

        public void test__read_two_line__SVG()
        {
            SVGConverter jc = new SVGConverter();

            IEnumerable<(IFigure, IDrawable)> figures = jc.ReadFile("two_lines.svg");

            Console.WriteLine("конец...");

        }
    }
}
