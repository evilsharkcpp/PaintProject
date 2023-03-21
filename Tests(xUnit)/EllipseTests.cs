using Geometry.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_xUnit_
{
    public class EllipseTests
    {
        [Fact]
        public void TestInitEllipse()
        {
            // Arrange
            Ellipse ellipse = new Ellipse();

            // Act
            GraphicTester tester = new GraphicTester();
            ellipse.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Ellipse);
            Tests_xUnit_.Figures.Ellipse? ellipse1 = tester.Figures[0] as Tests_xUnit_.Figures.Ellipse;
            Assert.Equal(-1, ellipse1.Start.X, 5);
            Assert.Equal(-1, ellipse1.Start.Y, 5);
            Assert.Equal(1, ellipse1.a, 5);
            Assert.Equal(1, ellipse1.b, 5);
        }

    }
}
