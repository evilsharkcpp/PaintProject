using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Drawing.Graphics;
using Geometry;
using Geometry.Figures;
using Interfaces;
using Newtonsoft.Json.Linq;
using Splat.ModeDetection;
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

            test__create_two_line__PNG();
            test__create_square__JPEG();
            test__create_rectangle__TIFF();
            test__create_circle__BMP();
            test__create_triangle__GIF();

            test__read_square__SVG();
            test__read_rectangle__SVG();
            test__read_triangle__SVG();
            test__read_ellipse__SVG();
            test__read_circle__SVG();
        }

        public void test__create_two_line__JSON()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(400, 0);

            var line = new ConvertibleLine(p1, p2, 45);

            var i_line = new FigureFabric().CreateFigureFromConvertibleFigure(line);

            Point2d p21 = new Point2d(250, 200);
            Point2d p22 = new Point2d(500, 200);

            var line2 = new ConvertibleLine(p21, p22, 280);

            var i_line2 = new FigureFabric().CreateFigureFromConvertibleFigure(line2);


            IDrawable d1 = new Drawable();
            
            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(255, 115, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 26, 123, 143);

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_line;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_line2;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            JSONConverter jc = new JSONConverter();

            FileStream stream = new FileStream("two_line.json", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_two_line__SVG()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(400, 0);

            var line = new ConvertibleLine(p1, p2, 45);

            var i_line = new FigureFabric().CreateFigureFromConvertibleFigure(line);

            Point2d p21 = new Point2d(250, 200);
            Point2d p22 = new Point2d(500, 200);

            var line2 = new ConvertibleLine(p21, p22, 280);

            var i_line2 = new FigureFabric().CreateFigureFromConvertibleFigure(line2);


            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(255, 115, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 26, 123, 143);

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_line2;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_line;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("two_line.svg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
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
            d1.OutLineColor = new Color(255, 115, 26, 26);

            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 115, 56, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);


            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_square0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_square;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("square.svg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
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
            d1.OutLineColor = new Color(255, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 115, 67, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);


            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_rectangle0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_rectangle;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("rectangle.svg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
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
            d1.OutLineColor = new Color(255, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 75, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_triangle0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_triangle;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("triangle.svg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_circle__SVG()
        {
            Point2d p1 = new Point2d(100, 100);

            var circle = new ConvertibleCircle(p1, radius: 50, 0);

            var i_circle = new FigureFabric().CreateFigureFromConvertibleFigure(circle);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(255, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 65, 26, 26);

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_circle;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj };
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("circle.svg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
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
            d1.OutLineColor = new Color(255, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 75, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_circle0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_circle;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("ellipse.svg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_two_line__PNG()
        {
            Point2d p1 = new Point2d(0, 0);
            Point2d p2 = new Point2d(400, 0);

            var line = new ConvertibleLine(p1, p2, 45);

            var i_line = new FigureFabric().CreateFigureFromConvertibleFigure(line);

            Point2d p21 = new Point2d(250, 200);
            Point2d p22 = new Point2d(500, 200);

            var line2 = new ConvertibleLine(p21, p22, 280);

            var i_line2 = new FigureFabric().CreateFigureFromConvertibleFigure(line2);


            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(255, 115, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 26, 123, 143);

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_line;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_line2;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            PNGConverter jc = new PNGConverter();

            FileStream stream = new FileStream("two_lines.png", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_square__JPEG()
        {
            Point2d p1 = new Point2d(200, 200);

            var square0 = new ConvertibleSquare(p1, width: 100, height: 100, 0);
            var square = new ConvertibleSquare(p1, width: 100, height: 100, 35);

            var i_square0 = new FigureFabric().CreateFigureFromConvertibleFigure(square0);
            var i_square = new FigureFabric().CreateFigureFromConvertibleFigure(square);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(255, 115, 26, 26);

            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 115, 56, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);


            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_square0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_square;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };
            JPEGConverter jc = new JPEGConverter();

            FileStream stream = new FileStream("square.jpeg", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_rectangle__TIFF()
        {
            Point2d p1 = new Point2d(200, 200);

            var rectangle0 = new ConvertibleRectangle(p1, width: 100, height: 300, 0);

            var rectangle = new ConvertibleRectangle(p1, width: 100, height: 300, 40);

            var i_rectangle0 = new FigureFabric().CreateFigureFromConvertibleFigure(rectangle0);
            var i_rectangle = new FigureFabric().CreateFigureFromConvertibleFigure(rectangle);

            IDrawable d1 = new Drawable();

            d1.IsNoOutLine = false;
            d1.OutLineColor = new Color(255, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 115, 67, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);


            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_rectangle0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_rectangle;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };

            TIFFConverter jc = new TIFFConverter();

            FileStream stream = new FileStream("rectangle.tiff", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_triangle__GIF()
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
            d1.OutLineColor = new Color(255, 115, 26, 26);
            d1.IsNoFill = false;
            d1.FillColor = new Color(255, 75, 26, 26);

            IDrawable d2 = new Drawable();

            d2.IsNoOutLine = false;
            d2.OutLineColor = new Color(255, 115, 26, 26);
            d2.IsNoFill = false;
            d2.FillColor = new Color(255, 121, 33, 56);


            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_triangle0;

            DrawableObject d_obj2 = new DrawableObject();
            d_obj2.Drawable = d2;
            d_obj2.Figure = i_triangle;

            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj, d_obj2 };

            GIFConverter jc = new GIFConverter();

            FileStream stream = new FileStream("triangle.gif", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }

        public void test__create_circle__BMP()
        {
            Point2d p1 = new Point2d(100, 100);

            var circle = new ConvertibleCircle(p1, radius: 50, 0);

            var i_circle = new FigureFabric().CreateFigureFromConvertibleFigure(circle);

            IDrawable d1 = new Drawable();

            DrawableObject d_obj = new DrawableObject();
            d_obj.Drawable = d1;
            d_obj.Figure = i_circle;


            IEnumerable<IDrawableObject> array = new List<IDrawableObject>() { d_obj };

            BMPConverter jc = new BMPConverter();

            FileStream stream = new FileStream("circle.bmp", FileMode.Create);

            jc.WriteFile(stream, array);

            stream.Close();
        }


        public void test__read_two_line__JSON()
        {
            JSONConverter jc = new JSONConverter();

            FileStream stream = new FileStream("two_lines.json", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();
        }

        public void test__read_two_line__SVG()
        {
            SVGConverter jc = new SVGConverter();


            FileStream stream = new FileStream("two_line.svg", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();

        }

        public void test__read_square__SVG()
        {
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("square.svg", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();

        }

        public void test__read_rectangle__SVG()
        {
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("rectangle.svg", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();

        }

        public void test__read_triangle__SVG()
        {
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("triangle.svg", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();

        }

        public void test__read_ellipse__SVG()
        {
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("ellipse.svg", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();

        }

        public void test__read_circle__SVG()
        {
            SVGConverter jc = new SVGConverter();

            FileStream stream = new FileStream("circle.svg", FileMode.Open);

            IEnumerable<IDrawableObject> figures = jc.ReadFile(stream);

            stream.Close();

        }

    }
}
