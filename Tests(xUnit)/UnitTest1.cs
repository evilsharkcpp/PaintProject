using DataStructures.Geometry;
using Geometry.Figures;
using Interfaces;

namespace Tests_xUnit_
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
			var fabric = FigureFabric.Create();
            IFigure? figure = fabric?.CreateFigure("Line", new Point2d(0, 0), new Point2d(10, 10));
            var point1 = figure?.PointParameters.Where(q => q.Name == "Point1").First();
            var point2 = figure?.PointParameters.Where(q => q.Name == "Point2").First();
            Assert.Equal(0, point1?.Value.X);
            Assert.Equal(0, point1?.Value.Y);
            Assert.Equal(10, point2?.Value.X);
            Assert.Equal(10, point2?.Value.Y);
        }
    }
}