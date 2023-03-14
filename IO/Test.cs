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
        }

        public void test__create_two_line__JSON()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(1, 1);

            var line = new Geometry.Figures.Line(p1, p2);

            Point2d p21 = new Point2d(5, 7);
            Point2d p22 = new Point2d(3, 8);

            var line2 = new Geometry.Figures.Line(p21, p22);

            IEnumerable<IFigure> array = new List<IFigure>() { line, line2 };

            JSONConverter jc = new JSONConverter();

            jc.WriteFile("two_lines", array);
        }

        public void test__create_two_line__SVG()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(95, 95);

            var line = new Geometry.Figures.Line(p1, p2);

            Point2d p21 = new Point2d(130, 200);
            Point2d p22 = new Point2d(200, 160);

            var line2 = new Geometry.Figures.Line(p21, p22);

            IEnumerable<IFigure> array = new List<IFigure>() { line, line2 };

            SVGConverter jc = new SVGConverter();

            jc.WriteFile("two_lines", array);
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
