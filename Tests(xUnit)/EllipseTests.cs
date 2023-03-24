using DataStructures.Geometry;
using Geometry.Figures;
using Geometry.Transforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tests_xUnit_
{
    public class EllipseTests
    {
        private void CheckEllipse(GraphicTester tester, Point2d p1, Point2d p2)
        {
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Ellipse);
            Tests_xUnit_.Figures.Ellipse? ellipse1 = tester.Figures[0] as Tests_xUnit_.Figures.Ellipse;
            Assert.Equal(p1.X, ellipse1.Start.X, 5);
            Assert.Equal(p1.Y, ellipse1.Start.Y, 5);
            Assert.Equal(p2.X, ellipse1.a, 5);
            Assert.Equal(p2.Y, ellipse1.b, 5);
        }

        [Fact]
        public void TestInitEllipse()
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();

            // Act

            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, new Point2d(0, 0), new Point2d(1, 1));
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(0, -10)]
        [InlineData(10, 0)]
        [InlineData(-10, 0)]
        [InlineData(10, 15)]
        [InlineData(-10, 15)]
        [InlineData(10, -15)]
        [InlineData(-10, -15)]
        [InlineData(0, 0.31)]
        [InlineData(0, -0.31)]
        [InlineData(0.15, 0)]
        [InlineData(-0.15, 0)]
        [InlineData(0.15, 0.31)]
        [InlineData(-0.15, 0.31)]
        [InlineData(0.15, -0.31)]
        [InlineData(-0.15, -0.31)]
        public void TestСhangePosition(double x_pos, double y_pos)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();

            // Act
            ellipse.Position = new Point2d(x_pos, y_pos);
            ellipse.Draw(tester);


            // Assert
            CheckEllipse(tester, new Point2d(x_pos + 1, y_pos + 1), new Point2d(1, 1));
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(4, 4)]
        [InlineData(6, 6)]
        [InlineData(15, 10)]
        [InlineData(10, 15)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0.12)]
        [InlineData(0.12, 0)]
        [InlineData(0.12, 0.12)]
        public void TestСhangeSize(double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();
            Point2d p1 = new Point2d(-1 + width / 2, -1+ height / 2 );

            // Act
            ellipse.Size = new Vector2d(width, height);
            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, p1, new Point2d(width / 2, height / 2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Math.PI / 4)]
        [InlineData(Math.PI)]
        [InlineData(-2 * Math.PI)]
        [InlineData(-Math.PI / 2)]
        [InlineData(3 * Math.PI / 4)]
        [InlineData(7 * Math.PI / 4)]

        [InlineData(Math.PI / 7)]
        [InlineData(-Math.PI / 5)]
        [InlineData(3 * Math.PI / 7)]
        [InlineData(-6 * Math.PI / 5)]
        [InlineData(-100 * Math.PI)]
        [InlineData(100 * Math.PI)]

        public void TestСhangeAngle(double angle)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();
            Point2d p1 = new Point2d(Math.Cos(angle) * (0) - Math.Sin(angle) * (0),
                                    Math.Sin(angle) * (0) + Math.Cos(angle) * (0));

            // Act
            ellipse.Angle = angle;
            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, p1, new Point2d(1, 1));
        }


        [Theory]
        [InlineData(-1, -1, 0)]
        [InlineData(0, 0, Math.PI / 2)]
        [InlineData(0, 10, Math.PI / 4)]
        [InlineData(10, 0, 7 * Math.PI / 4)]
        [InlineData(10, 10, 2 * Math.PI)]
        [InlineData(-15, 10, Math.PI / 3)]
        [InlineData(10, -15, 4 * Math.PI / 3)]
        [InlineData(-10, -15, Math.PI / 10)]
        [InlineData(0, -15, -Math.PI / 10)]
        [InlineData(-0.15, 0.1, Math.PI / 3)]
        [InlineData(0.1, -0.15, 4 * Math.PI / 3)]
        [InlineData(-0.1, -0.15, Math.PI / 10)]
        [InlineData(0, -0.15, -Math.PI / 10)]
        public void TestСhangePositionAndAngle(double x_pos, double y_pos, double angle)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();
            Point2d p1 = new Point2d(0, 0);

            Transform2D transform1 = new RotateTransform2D()
            {
                Angle = angle,
            },
            transform2 = new TranslateTransform2D()
            {
                V = new Vector2d(x_pos + 1, y_pos + 1) - p1
            };


            transform1.Apply(p1, ref p1);

            transform2.Apply(p1, ref p1);

            // Act
            ellipse.Position = new Point2d(x_pos, y_pos);
            ellipse.Angle = angle;
            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, p1, new Point2d(1, 1));
        }


        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(-1, -1, 0, 10)]
        [InlineData(0, -10, 10, 0)]
        [InlineData(0, -10, 10, 10)]
        [InlineData(10, 0, 0, 1)]
        [InlineData(-15, 10, 4, 5)]
        [InlineData(10, -15, 1, 0)]
        [InlineData(-10, -15, 1, 1)]
        public void TestСhangePositionAndSize(double x_pos, double y_pos, double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse(); 
            Point2d p1 = new Point2d(0, 0);

            Transform2D transform1 = new ScaleTransform2D()
            {
                ScaleX = width / 2.0,
                ScaleY = height / 2.0
            };

            transform1.Apply(p1, ref p1);

            Transform2D transform2 = new TranslateTransform2D()
            {
                V = new Vector2d(x_pos + width / 2, y_pos + height / 2) - p1
            };

            transform2.Apply(p1, ref p1);

            // Act
            ellipse.Position = new Point2d(x_pos, y_pos);
            ellipse.Size = new Vector2d(width, height);
            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, p1, new Point2d(width / 2, height / 2));
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 2, 2)]
        [InlineData(Math.PI / 4, 0, 10)]
        [InlineData(Math.PI, 10, 0)]
        [InlineData(2 * Math.PI, 10, 10)]
        [InlineData(Math.PI / 2, 0, 1)]
        [InlineData(-Math.PI / 3, 4, 5)]
        [InlineData(-3 * Math.PI / 4, 1, 0)]
        [InlineData(-7 * Math.PI / 4, 1, 1)]

        public void TestСhangeAngleAndSize(double angle, double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();

            Point2d p1 = new Point2d(0, 0);

            Transform2D transform1 = new ScaleTransform2D()
            {
                ScaleX = width / 2.0,
                ScaleY = height / 2.0
            };

            transform1.Apply(p1, ref p1);

            Transform2D transform2 = new RotateTransform2D()
            {
                Angle = angle,
            },
            transform3 = new TranslateTransform2D()
            {
                V = new Vector2d(-1+ width / 2.0, -1+ height / 2.0) - p1
            };

            transform2.Apply(p1, ref p1);

            transform3.Apply(p1, ref p1);

            // Act
            ellipse.Angle = angle;
            ellipse.Size = new Vector2d(width, height);
            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, p1, new Point2d(width / 2.0, height / 2.0));
        }


        [Theory]
        [InlineData(-1, -1, 0, 2, 2)]
        [InlineData(0, 0, Math.PI / 2, 2, 2)]
        [InlineData(0, 10, Math.PI / 4, 2, 2)]
        [InlineData(10, 0, 7 * Math.PI / 4, 2, 2)]
        [InlineData(10, 10, 2 * Math.PI, 2, 2)]
        [InlineData(-15, 10, Math.PI / 3, 2, 2)]
        [InlineData(10, -15, 4 * Math.PI / 3, 2, 2)]
        [InlineData(-10, -15, Math.PI / 10, 2, 2)]
        [InlineData(0, -15, -Math.PI / 10, 2, 2)]
        [InlineData(-0.15, 0.1, Math.PI / 3, 2, 2)]
        [InlineData(0.1, -0.15, 4 * Math.PI / 3, 2, 2)]
        [InlineData(-0.1, -0.15, Math.PI / 10, 2, 2)]
        [InlineData(0, -0.15, -Math.PI / 10, 2, 2)]
        public void TestСhangePositionAndAngleAndSize(double x_pos, double y_pos, double angle, double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Ellipse ellipse = new Ellipse();
            Point2d p1 = new Point2d(0, 0);

            Transform2D transform1 = new ScaleTransform2D()
            {
                ScaleX = width / 2.0,
                ScaleY = height / 2.0
            };

            transform1.Apply(p1, ref p1);

            Transform2D transform2 = new RotateTransform2D()
            {
                Angle = angle,
            },
            transform3 = new TranslateTransform2D()
            {
                V = new Vector2d(x_pos+width / 2.0, y_pos+height / 2.0) - p1
            };

            transform2.Apply(p1, ref p1);

            transform3.Apply(p1, ref p1);

            // Act
            ellipse.Position = new Point2d(x_pos, y_pos);
            ellipse.Size = new Vector2d(width, height);
            ellipse.Angle = angle;
            ellipse.Draw(tester);

            // Assert
            CheckEllipse(tester, p1, new Point2d(width / 2 , height / 2.0));
        }

    }
}
