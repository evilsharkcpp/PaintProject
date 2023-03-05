using DataStructures.Geometry;
using Geometry.Figures;
using Geometry.Transforms;
using Interfaces;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;

namespace Tests_xUnit_
{

    public class LineTests
    {
        [Theory]
        [InlineData(2.5 * Math.PI)]
        [InlineData(Math.PI / 2)]
        [InlineData(-1.5 * Math.PI)]
        public void Rotate_VerticalLineRotate90_HorizontalLine(float angle)
        {
            // Arrange
            int expectedX1 = 0;
            int expectedY1 = -1;
            int expectedX2 = 0;
            int expectedY2 = 1;
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(-1, 0), new Point2d(1, 0));

            // Act
            figure?.Rotate(angle);
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();

            // Assert
            Assert.Equal(expectedX1, Math.Round(point1.Value.X));
            Assert.Equal(expectedY1, Math.Round(point1.Value.Y));
            Assert.Equal(expectedX2, Math.Round(point2.Value.X));
            Assert.Equal(expectedY2, Math.Round(point2.Value.Y));
        }

        [Theory]
        [InlineData(2.5 * Math.PI)]
        [InlineData(Math.PI / 2)]
        [InlineData(-1.5 * Math.PI)]
        [InlineData(0)]
        public void Rotate_SameAngleBackAndForth_Baseline(float angle)
        {
            // Arrange
            int expectedX1 = -1;
            int expectedY1 = 0;
            int expectedX2 = 1;
            int expectedY2 = 0;
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(-1, 0), new Point2d(1, 0));

            // Act
            figure?.Rotate(angle);
            figure?.Rotate(-angle);
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();

            // Assert
            Assert.Equal(expectedX1, Math.Round(point1.Value.X));
            Assert.Equal(expectedY1, Math.Round(point1.Value.Y));
            Assert.Equal(expectedX2, Math.Round(point2.Value.X));
            Assert.Equal(expectedY2, Math.Round(point2.Value.Y));
        }

        [Theory]
        [InlineData(0, 0.0001, 0.00005)]
        [InlineData(1, 0.01, 0.005)]
        [InlineData(0, 4, 2)]
        public void IsInside_PointOutside_False(float x, float y, float eps)
        {
 
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(-1, 0), new Point2d(1, 0));

            // Act
            bool isInside = figure.IsInside(new Point2d(x, y), eps);
            // Assert
            Assert.False(isInside);
        }

        [Theory]
        [InlineData(0, 1E-5, 2E-5)]
        [InlineData(1, 1e-3, 2e-3)]
        [InlineData(-1, -1e-3, 2e-3)]
        public void IsInside_PointInside_True(float x, float y, float eps)
        {

            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(-1, 0), new Point2d(1, 0));

            // Act
            bool isInside = figure.IsInside(new Point2d(x, y), eps);
            // Assert
            Assert.True(isInside);
        }
    }
}
    