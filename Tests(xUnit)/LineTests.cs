using DataStructures.Geometry;
using Geometry.Figures;
using Geometry.Transforms;

namespace Tests_xUnit_
{

    public class LineTests
    {
        private void CheckLine(GraphicTester tester, Point2d p1, Point2d p2)
        {
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Figures.Line);
            Figures.Line? line = tester.Figures[0] as Figures.Line;
            Assert.Equal(p1.X, line.V1.X, 5);
            Assert.Equal(p1.Y, line.V1.Y, 5);
            Assert.Equal(p2.X, line.V2.X, 5);
            Assert.Equal(p2.Y, line.V2.Y, 5);
        }

        [Fact]

        public void TestInitLine()
        {
            // Arrange
            Line line1 = new Line();

            // Act
            GraphicTester tester = new GraphicTester();
            line1.Draw(tester);

            // Assert
            CheckLine(tester, new Point2d(-1, -1), new Point2d(1, 1));
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
        public void Test—hangePosition(double x_pos, double y_pos)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(x_pos, y_pos),
                    p2 = new Point2d(2.0 + x_pos, 2.0 + y_pos);

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(10, 10)]
        [InlineData(15, 10)]
        [InlineData(10, 15)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void Test—hangeSize(double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(-1, -1),
                    p2 = new Point2d(-1 + width, -1 + height);

            // Act
            line1.Size = new Vector2d(width, height);
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
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
        public void Test—hangeAngle(double angle)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(Math.Cos(angle) * (-1) - Math.Sin(angle) * (-1),
                                     Math.Sin(angle) * (-1) + Math.Cos(angle) * (-1)),
                    p2 = new Point2d(Math.Cos(angle) * (+1) - Math.Sin(angle) * (+1),
                                     Math.Sin(angle) * (+1) + Math.Cos(angle) * (+1));

            // Act
            line1.Angle = angle;
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
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
     
        public void Test—hangeAngleAndSize(double angle, double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(-1, -1),
                    p2 = new Point2d(+1, +1);

            Transform2D transform1 = new ScaleTransform2D()
            {
                ScaleX = width / 2.0,
                ScaleY = height / 2.0
            };

            transform1.Apply(p1, ref p1);
            transform1.Apply(p2, ref p2);

            Transform2D transform2 = new RotateTransform2D()
            {
                Angle = angle,
            },
            transform3 = new TranslateTransform2D()
            {
                V = new Vector2d(-1, -1) - p1
            };

            transform2.Apply(p1, ref p1);
            transform2.Apply(p2, ref p2);

            transform3.Apply(p1, ref p1);
            transform3.Apply(p2, ref p2);

            // Act
            line1.Angle = angle;
            line1.Size = new Vector2d(width, height);
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
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
        [InlineData(0, -15, - Math.PI / 10)]
        [InlineData(-0.15, 0.1, Math.PI / 3)]
        [InlineData(0.1, -0.15, 4 * Math.PI / 3)]
        [InlineData(-0.1, -0.15, Math.PI / 10)]
        [InlineData(0, -0.15, -Math.PI / 10)]
        public void Test—hangePositionAndAngle(double x_pos, double y_pos,double angle)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(-1, -1),
                    p2 = new Point2d(+1, +1);

            Transform2D transform1 = new RotateTransform2D()
            {
                Angle = angle,
            },
            transform2 = new TranslateTransform2D()
            {
                V = new Vector2d(x_pos, y_pos) - p1
            };


            transform1.Apply(p1, ref p1);
            transform1.Apply(p2, ref p2);

            transform2.Apply(p1, ref p1);
            transform2.Apply(p2, ref p2);

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
            line1.Angle = angle;
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(-1, -1, 0, 10)]
        [InlineData(0, -10, 10, 0)]
        [InlineData(0, -10, 10, 10)]
        [InlineData(10, 0, 0, 1)]
        [InlineData(-15, 10, 4, 5)]
        [InlineData(10, -15, 1, 0)]
        [InlineData(-10,-15, 1, 1)]
        public void Test—hangePositionAndSize(double x_pos, double y_pos, double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(-1, -1),
                    p2 = new Point2d(+1, +1);

            Transform2D transform1 = new ScaleTransform2D()
            {
                ScaleX = width / 2.0,
                ScaleY = height / 2.0
            };

            transform1.Apply(p1, ref p1);
            transform1.Apply(p2, ref p2);

            Transform2D transform2 = new TranslateTransform2D()
            {
                V = new Vector2d(x_pos, y_pos) - p1
            };

            transform2.Apply(p1, ref p1);
            transform2.Apply(p2, ref p2);

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
            line1.Size = new Vector2d(width, height);
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
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
        public void Test—hangePositionAndAngleAndSize(double x_pos, double y_pos, double angle, double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            Point2d p1 = new Point2d(-1, -1),
                    p2 = new Point2d(+1, +1);

            Transform2D transform1 = new ScaleTransform2D()
            {
                ScaleX = width / 2.0,
                ScaleY = height / 2.0
            };

            transform1.Apply(p1, ref p1);
            transform1.Apply(p2, ref p2);

            Transform2D transform2 = new RotateTransform2D()
            {
                Angle = angle,
            },
            transform3 = new TranslateTransform2D()
            {
                V = new Vector2d(x_pos, y_pos) - p1
            };

            transform2.Apply(p1, ref p1);
            transform2.Apply(p2, ref p2);

            transform3.Apply(p1, ref p1);
            transform3.Apply(p2, ref p2);

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
            line1.Size = new Vector2d(width, height);
            line1.Angle = angle;
            line1.Draw(tester);

            // Assert
            CheckLine(tester, p1, p2);
        }
    }
}