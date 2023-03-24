using DataStructures.Geometry;
using Geometry.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests_xUnit_.Figures;

namespace Tests_xUnit_
{
    public class RectangleTests
    {
        [Fact]
        public void TestInitRectangle()
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Geometry.Figures.Rectangle rectangle1 = new Geometry.Figures.Rectangle();

            // Act
            rectangle1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Rectangle);
            Tests_xUnit_.Figures.Rectangle? rectangle = tester.Figures[0] as Tests_xUnit_.Figures.Rectangle;
            Assert.Equal(-1, rectangle.Start.X, 5);
            Assert.Equal(-1, rectangle.Start.Y, 5);
            Assert.Equal(2, rectangle.a, 5);
            Assert.Equal(2, rectangle.b, 5);
            Assert.False(rectangle.IsOutline);
            Assert.True(rectangle.IsFill);
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
            Geometry.Figures.Rectangle rectangle1 = new Geometry.Figures.Rectangle();
            double expectedX1 = x_pos;
            double expectedY1 = y_pos;
            double expecteda = 2.0;
            double expectedb = 2.0;

            // Act
            rectangle1.Position = new Point2d(x_pos, y_pos);
            rectangle1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Rectangle);
            Tests_xUnit_.Figures.Rectangle? rectangle = tester.Figures[0] as Tests_xUnit_.Figures.Rectangle;
            Assert.Equal(expectedX1, rectangle.Start.X, 5);
            Assert.Equal(expectedY1, rectangle.Start.Y, 5);
            Assert.Equal(expecteda, rectangle.a, 5);
            Assert.Equal(expectedb, rectangle.b, 5); 
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
        [InlineData(0, 0.12)]
        [InlineData(0.12, 0)]
        [InlineData(0.12, 0.12)]
        public void TestСhangeSize(double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Geometry.Figures.Rectangle rectangle1 = new Geometry.Figures.Rectangle();
            double expectedX1 = -1;
            double expectedY1 = -1;
            double expecteda = width;
            double expectedb = height;

            // Act
            rectangle1.Size = new Vector2d(width, height);
            rectangle1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Rectangle);
            Tests_xUnit_.Figures.Rectangle? rectangle = tester.Figures[0] as Tests_xUnit_.Figures.Rectangle;
            Assert.Equal(expectedX1, rectangle.Start.X, 5);
            Assert.Equal(expectedY1, rectangle.Start.Y, 5);
            Assert.Equal(expecteda, rectangle.a, 5);
            Assert.Equal(expectedb, rectangle.b, 5);
        }
    }
}
