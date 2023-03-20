using DataStructures.Geometry;
using ReactiveUI;
using System.Drawing;
using System.Reactive;

namespace Interfaces
{
    public interface ILogic
    {
        public ReactiveCommand<string, IFigure> CreateFigure { get; }
        public ReactiveCommand<IDrawableObject, int> AddFigure { get; }
        public ReactiveCommand<Point2d, int> SelectFigure { get; }
        public ReactiveCommand<int, Unit> RemoveFigure { get; }
        public ReactiveCommand<int, IDrawableObject> GetFigureById { get; }
        public IReadOnlyDictionary<int, IDrawableObject> Figures { get; }
        IEnumerable<IDrawableObject> SelectedFigures { get; set; }
        public ReactiveCommand<string, Unit> SaveFile { get; }
        public ReactiveCommand<string, IEnumerable<(IFigure, IDrawable)>> OpenFile { get; }

}
}
