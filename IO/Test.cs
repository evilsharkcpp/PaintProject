using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Interfaces;
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

            Point2d p21 = new Point2d(5, 7);
            Point2d p22 = new Point2d(3, 8);

            var line2 = new ConvertibleLine(p21, p22, 45);

            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { line, line2 };

            JSONConverter jc = new JSONConverter();

            jc.WriteFile("two_lines", array);
        }

        public void test__create_two_line__SVG()
        {
            Point2d p1 = new Point2d(10, 10);
            Point2d p2 = new Point2d(200, 10);

            var line = new ConvertibleLine(p1, p2, 45);

            Point2d p21 = new Point2d(130, 200);
            Point2d p22 = new Point2d(200, 200);

            var line2 = new ConvertibleLine(p21, p22, 78);


            var line3 = new ConvertibleLine(p1, p2, 0);

            var line4 = new ConvertibleLine(p21, p22, 0);

            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { line, line2, line3, line4 };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("two_lines", array);
        }

        public void test__create_square__SVG()
        {
            Point2d p1 = new Point2d(200, 200);

            var square0 = new ConvertibleSquare(p1, width: 100, height: 100, 0);
            var square = new ConvertibleSquare(p1, width: 100, height: 100, 35);


            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { square, square0 };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("square", array);
        }

        public void test__create_rectangle__SVG()
        {
            Point2d p1 = new Point2d(200, 200);

            var rectangle0 = new ConvertibleRectangle(p1, width: 100, height: 300, 0);

            var rectangle = new ConvertibleRectangle(p1, width: 100, height: 300, 40);


            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { rectangle, rectangle0 };

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


            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { triangle, triangle0 };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("triangle", array);
        }

        public void test__create_circle__SVG()
        {
            Point2d p1 = new Point2d(100, 100);

            var circle = new ConvertibleCircle(p1, radius: 50, 0);

            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { circle };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("circle", array);
        }


        public void test__create_ellipse__SVG()
        {
            Point2d p1 = new Point2d(150, 150);

            var circle0 = new ConvertibleEllipse(p1, radiusX: 100, radiusY: 50, 0);

            var circle = new ConvertibleEllipse(p1, radiusX: 100, radiusY: 50, 45);

            IEnumerable<ConvertibleFigure> array = new List<ConvertibleFigure>() { circle, circle0 };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("ellipse", array);
        }


        public void test__read_two_line__JSON()
        {
            JSONConverter jc = new JSONConverter();

            IEnumerable<IFigure> figures = jc.ReadFile("two_lines.json");

            Console.WriteLine("конец...");

        }

        public void test__read_two_line__SVG()
        {
            SVGConverter jc = new SVGConverter();

            IEnumerable<IFigure> figures = jc.ReadFile("two_lines.svg");

            Console.WriteLine("конец...");

        }
    }
}
