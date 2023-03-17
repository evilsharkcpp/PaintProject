using DataStructures.Geometry;
using ReactiveUI;
using System.Reactive;

namespace Interfaces
{
    public interface ILogic
    {
        public ReactiveCommand<string, IFigure> CreateFigure { get; }
        public ReactiveCommand<(IFigure, IDrawable), int> AddFigure { get; }
        public ReactiveCommand<Point2d, Unit> SelectFigure { get; }
        public ReactiveCommand<IFigure, Unit> RemoveFigure { get; }
        public ReactiveCommand<int, IFigure> GetFigureById { get; }
        IEnumerable<(IFigure, IDrawable)> Figures { get; set; }
        IEnumerable<(IFigure, IDrawable)> SelectedFigures { get; set; }

    }
}
