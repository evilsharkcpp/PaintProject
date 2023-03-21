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
            Assert.False(line.IsOutline);
            Assert.True(line.IsFill);
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

        [Theory]
        [InlineData(0, 0, 0)]
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
            double expectedX1 = -Math.Sin(angle) * (-1) + Math.Cos(angle) * (-1);
            double expectedY1 = Math.Cos(angle) * (-1) + Math.Sin(angle) * (-1);
            double expectedX2 = -Math.Sin(angle) * (1) + Math.Cos(angle) * (1) + width;
            double expectedY2 = Math.Cos(angle) * (1) + Math.Sin(angle) * (1) + height;

            // Act
            line1.Angle = angle;
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
            double expectedX1 = x_pos;
            double expectedY1 = y_pos;
            double expectedX2 = 2.0 + x_pos;
            double expectedY2 = 2.0 + y_pos;
            expectedX1 = -Math.Sin(angle) * (-1) + Math.Cos(angle) * (-1) + expectedX1 + 1;
            expectedY1 = Math.Cos(angle) * (-1) + Math.Sin(angle) * (-1) + expectedY1 + 1;
            expectedX2 = -Math.Sin(angle) * (1) + Math.Cos(angle) * (1) + expectedX1 + 1;
            expectedY2 = Math.Cos(angle) * (1) + Math.Sin(angle) * (1) + expectedY1 + 1;

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
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
            double expectedX1 = x_pos;
            double expectedY1 = y_pos;
            double expectedX2 = 2.0 + x_pos + width;
            double expectedY2 = 2.0 + y_pos + height;

            // Act
            line1.Position = new Point2d(x_pos, y_pos);
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
    }
}