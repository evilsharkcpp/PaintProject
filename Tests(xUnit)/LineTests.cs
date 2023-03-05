using DataStructures.Geometry;
using Geometry.Figures;
using Geometry.Transforms;
using Interfaces;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;

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

        [Theory]
        [InlineData(0, 0, 5, 5, 6, 6)]
        [InlineData(2, 0, 7, 5, 8, 6)]
        [InlineData(0, 1, 5, 6, 6, 7)]
        [InlineData(-2, 0, 3, 5, 4, 6)]
        [InlineData(0, -1, 5, 4, 6, 5)]
        [InlineData(2, 2, 7, 7, 8, 8)]
        [InlineData(-2, -2, 3, 3, 4, 4)]
        [InlineData(null, null, 5, 5, 6, 6)]
        public void Translate_DiagonalLineAllDirections_TranslateOnly(float translateX, float translateY, float point1X, float point1Y, float point2X, float point2Y)
        {
            // Arrange
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(5, 5), new Point2d(6, 6));
            // Act
            figure?.Translate(new Vector2(translateX, translateY));
            // Assert
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();
            Assert.Equal(point1X, point1?.Value.X);
            Assert.Equal(point1Y, point1?.Value.Y);
            Assert.Equal(point2X, point2?.Value.X);
            Assert.Equal(point2Y, point2?.Value.Y);
        }

        [Theory]
        [InlineData(0, 0, 5, 0, 6, 0)]
        [InlineData(null, null, 5, 0, 6, 0)]
        [InlineData(2, 0, 4.5, 0, 6.5, 0)]
        [InlineData(0, 2, 5, 0, 6, 0)]
        [InlineData(2, 2, 4.5, 0, 6.5, 0)]
        public void Scale_HorizontalLineAllDirections_ScaleOnly(float scaleX, float scaleY, float point1X, float point1Y, float point2X, float point2Y)
        {
            // Arrange
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(5, 0), new Point2d(6, 0));
            // Act
            figure?.Scale(scaleX, scaleY);
            // Assert
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();
            Assert.Equal(point1X, point1?.Value.X);
            Assert.Equal(point1Y, point1?.Value.Y);
            Assert.Equal(point2X, point2?.Value.X);
            Assert.Equal(point2Y, point2?.Value.Y);
        }

        [Theory]
        [InlineData(0, 0, 0, 5, 0, 6)]
        [InlineData(null, null, 0, 5, 0, 6)]
        [InlineData(2, 0, 0, 5, 0, 6)]
        [InlineData(0, 2, 0, 4.5, 0, 6.5)]
        [InlineData(2, 2, 0, 4.5, 0, 6.5)]
        public void Scale_VerticalLineAllDirections_ScaleOnly(float scaleX, float scaleY, float point1X, float point1Y, float point2X, float point2Y)
        {
            // Arrange
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(0, 5), new Point2d(0, 6));
            // Act
            figure?.Scale(scaleX, scaleY);
            // Assert
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();
            Assert.Equal(point1X, point1?.Value.X);
            Assert.Equal(point1Y, point1?.Value.Y);
            Assert.Equal(point2X, point2?.Value.X);
            Assert.Equal(point2Y, point2?.Value.Y);
        }

        [Theory]
        [InlineData(0, 0, 5, 5, 6, 6)]
        [InlineData(null, null, 5, 5, 6, 6)]
        [InlineData(2, 0, 4.5, 5, 6.5, 6)]
        [InlineData(0, 2, 5, 4.5, 6, 6.5)]
        [InlineData(2, 2, 4.5, 4.5, 6.5, 6.5)]


        public void Scale_DiagonalLineAllDirections_ScaleOnly(float scaleX, float scaleY, float point1X, float point1Y, float point2X, float point2Y)
        {
            // Arrange
            var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(5, 5), new Point2d(6, 6));
            // Act
            figure?.Scale(scaleX, scaleY);
            // Assert
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();
            Assert.Equal(point1X, point1?.Value.X);
            Assert.Equal(point1Y, point1?.Value.Y);
            Assert.Equal(point2X, point2?.Value.X);
            Assert.Equal(point2Y, point2?.Value.Y);
        }
    }
}
