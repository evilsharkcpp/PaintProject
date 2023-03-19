using DataStructures.Geometry;
using Geometry.Figures;

namespace Tests_xUnit_
{

    public class LineTests
    {
        [Fact]

        public void TestInitLine()
        {
            // Arrange
            Line line1 = new Line();

            // Act
            GraphicTester tester = new GraphicTester();
            line1.Draw(tester);


            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Line);
            Tests_xUnit_.Figures.Line? line = tester.Figures[0] as Tests_xUnit_.Figures.Line;
            Assert.Equal(-1, line.V1.X, 5);
            Assert.Equal(-1, line.V1.Y, 5);
            Assert.Equal(1, line.V2.X, 5);
            Assert.Equal(1, line.V2.Y, 5);
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(10, 10)]
        [InlineData(-10, 10)]
        [InlineData(10, -10)]
        public void Test—hangePosition(double x_pos, double y_pos)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            double expectedX1 = x_pos;
            double expectedY1 = y_pos;
            double expectedX2 = 2.0 + x_pos;
            double expectedY2 = 2.0 + y_pos;

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
            line1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Line);
            Tests_xUnit_.Figures.Line? line = tester.Figures[0] as Tests_xUnit_.Figures.Line;
            Assert.Equal(expectedX1, line.V1.X, 5);
            Assert.Equal(expectedY1, line.V1.Y, 5);
            Assert.Equal(expectedX2, line.V2.X, 5);
            Assert.Equal(expectedY2, line.V2.Y, 5);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(10, 10)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void Test—hangeSize(double width, double height)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            double expectedX1 = -1;
            double expectedY1 = -1 ;
            double expectedX2 = -1 + width;
            double expectedY2 = -1 + height;

            // Act
            line1.Size = new Vector2d(width, height);
            line1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Line);
            Tests_xUnit_.Figures.Line? line = tester.Figures[0] as Tests_xUnit_.Figures.Line;
            Assert.Equal(expectedX1, line.V1.X, 5);
            Assert.Equal(expectedY1, line.V1.Y, 5);
            Assert.Equal(expectedX2, line.V2.X, 5);
            Assert.Equal(expectedY2, line.V2.Y, 5);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Math.PI / 4)]
        [InlineData(Math.PI)]
        [InlineData(2 * Math.PI)]
        [InlineData(Math.PI / 2)]
        [InlineData(3 * Math.PI / 4)]
        [InlineData(7 * Math.PI / 4)]
        public void Test—hangeAngle(double angle)
        {
            // Arrange
            GraphicTester tester = new GraphicTester();
            Line line1 = new Line();
            double expectedX1 = -Math.Sin(angle) * (-1) + Math.Cos(angle) * (-1);
            double expectedY1 = Math.Cos(angle) * (-1) + Math.Sin(angle) * (-1);
            double expectedX2 = -Math.Sin(angle) * (1) + Math.Cos(angle) * (1);
            double expectedY2 = Math.Cos(angle) * (1) + Math.Sin(angle) * (1);

            // Act
            line1.Angle = angle;
            line1.Draw(tester);

            // Assert
            Assert.Single(tester.Figures);
            Assert.True(tester.Figures[0] is Tests_xUnit_.Figures.Line);
            Tests_xUnit_.Figures.Line? line = tester.Figures[0] as Tests_xUnit_.Figures.Line;
            Assert.Equal(expectedX1, line.V1.X, 5);
            Assert.Equal(expectedY1, line.V1.Y, 5);
            Assert.Equal(expectedX2, line.V2.X, 5);
            Assert.Equal(expectedY2, line.V2.Y, 5);
        }
    }
}