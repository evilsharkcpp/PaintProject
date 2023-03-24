using DataStructures.Geometry;
using Geometry.Figures;

namespace Tests_xUnit_
{
    public class TriangleTests
    {

        private void CheckTriangle(GraphicTester tester, Point2d p1, Point2d p2, Point2d p3)
        {
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Figures.Triangle);
            Figures.Triangle? triangle = tester.Figures[0] as Figures.Triangle;
            Assert.Equal(p1.X, triangle.V1.X, 5);
            Assert.Equal(p1.Y, triangle.V1.Y, 5);
            Assert.Equal(p2.X, triangle.V2.X, 5);
            Assert.Equal(p2.Y, triangle.V2.Y, 5);
            Assert.Equal(p3.X, triangle.V3.X, 5);
            Assert.Equal(p3.Y, triangle.V3.Y, 5);
        }

        [Fact]
        public void TestInitTriangle()
        {
            // Arrange
            Triangle triangle1 = new Triangle();

            // Act
            GraphicTester tester = new GraphicTester();
            triangle1.Draw(tester);

            // Assert
            CheckTriangle(tester, new Point2d(1, 1), new Point2d(-1, 1), new Point2d(0, -1));
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
            Triangle triangle1 = new Triangle();
            Point2d p1 = new Point2d(x_pos + 2, y_pos + 2),
                    p2 = new Point2d(x_pos, y_pos + 2),
                    p3 = new Point2d(x_pos + 1, y_pos);

            // Act
            triangle1.Position = new Point2d(x_pos, y_pos);
            triangle1.Draw(tester);

            // Assert
            CheckTriangle(tester, p1, p2, p3);
        }

        [Theory]
        [InlineData(3, 3)]
        [InlineData(1, 3)]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(10, 10)]
        [InlineData(15, 10)]
        [InlineData(10, 15)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void TestСhangeSize(double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Triangle triangle1 = new Triangle();
            Point2d p1 = new Point2d(-1 + width, -1 + height),
                    p2 = new Point2d(-1, -1 + height),
                    p3 = new Point2d(-1 + width / 2, -1);

            // Act
            triangle1.Size = new Vector2d(width, height);
            triangle1.Draw(tester);

            // Assert
            CheckTriangle(tester, p1, p2, p3);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Math.PI)]
        [InlineData(-2 * Math.PI)]
        [InlineData(-100 * Math.PI)]
        [InlineData(100 * Math.PI)]
        public void TestСhangeAngle(double angle)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Triangle triangle1 = new Triangle();
            Point2d p1 = new Point2d(Math.Cos(angle) - Math.Sin(angle),
                                     Math.Sin(angle) + Math.Cos(angle)),
                    p2 = new Point2d(-Math.Cos(angle) - Math.Sin(angle),
                                     -Math.Sin(angle) + Math.Cos(angle)),
                    p3 = new Point2d(-Math.Sin(angle),
                                     -Math.Cos(angle));

            // Act
            triangle1.Angle = angle;
            triangle1.Draw(tester);

            // Assert
            CheckTriangle(tester, p1, p2, p3);
        }
    }
}
