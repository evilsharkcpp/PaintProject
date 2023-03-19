using Geometry.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_xUnit_
{
    public class RectangleTests
    {
        [Fact]
        public void TestInitRectangle()
        {
            // Arrange
            Rectangle rectangle1 = new Rectangle();

            // Act
            GraphicTester tester = new GraphicTester();
            rectangle1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Rectangle);
            Tests_xUnit_.Figures.Rectangle? rectangle = tester.Figures[0] as Tests_xUnit_.Figures.Rectangle;
            Assert.Equal(-1, rectangle.Start.X, 5);
            Assert.Equal(1, rectangle.Start.Y, 5);
            Assert.Equal(2, rectangle.a, 5);
            Assert.Equal(2, rectangle.b, 5);
        }

    }
}
