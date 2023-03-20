using DataStructures.Geometry;
using Geometry.Figures;

namespace Tests_xUnit_
{
    public class TriangleTests
    {

        [Fact]
        public void TestInitTriangle()
        {
            // Arrange
            Triangle triangle1 = new Triangle();

            // Act
            GraphicTester tester = new GraphicTester();
            triangle1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Triangle);
            Tests_xUnit_.Figures.Triangle? trangle = tester.Figures[0] as Tests_xUnit_.Figures.Triangle;
            Assert.Equal(-1, trangle.V1.X, 5);
            Assert.Equal(-1, trangle.V1.Y, 5);
            Assert.Equal(1, trangle.V2.X, 5);
            Assert.Equal(-1, trangle.V2.Y, 5);
            Assert.Equal(0, trangle.V3.X, 5);
            Assert.Equal(1, trangle.V3.Y, 5);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(10, 10)]
        [InlineData(-10, 10)]
        [InlineData(10, -10)]
        public void TestСhangePosition(double x_pos, double y_pos)
        {
            // Arrange
            Triangle triangle1 = new Triangle();
            double expectedX1 = x_pos;
            double expectedY1 = y_pos;
            double expectedX2 = 2.0 + x_pos;
            double expectedY2 = y_pos;
            double expectedX3 = 1 + x_pos;
            double expectedY3 = 1 + y_pos;

            // Act
            GraphicTester tester = new GraphicTester();
            triangle1.Position = new Point2d(x_pos, y_pos);
            triangle1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Triangle);
            Tests_xUnit_.Figures.Triangle? trangle = tester.Figures[0] as Tests_xUnit_.Figures.Triangle;
            Assert.Equal(expectedX1, trangle.V1.X, 5);
            Assert.Equal(expectedY1, trangle.V1.Y, 5);
            Assert.Equal(expectedX2, trangle.V2.X, 5);
            Assert.Equal(expectedY2, trangle.V2.Y, 5);
            Assert.Equal(expectedX3, trangle.V3.X, 5);
            Assert.Equal(expectedY3, trangle.V3.Y, 5);
        }
    }
}
