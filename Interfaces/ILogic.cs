using DataStructures.Geometry;
using ReactiveUI;
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

        Point2d DefaultSize { get; set; }
        int StackStateSize { get; set; }
        int StateIndex { get; }

        IReadOnlyDictionary<int, IDrawableObject> Figures { get; }
        IEnumerable<int> SelectedFigures { get; }

        ReactiveCommand<string, IFigure?> CreateFigure { get; }

        ReactiveCommand<IDrawableObject, int> AddFigure { get; }
        ReactiveCommand<int, bool> RemoveFigure { get; }

        ReactiveCommand<Point2d, int> SelectFigure { get; }
        ReactiveCommand<Rect, bool> SelectFigures { get; }

        ReactiveCommand<Unit, IEnumerable<(string CommandName, ReactiveCommand<Point2d, bool> Command)>> GetContextCommands { get; }

        ReactiveCommand<Stream, bool> Save { get; }
        ReactiveCommand<Stream, bool> Load { get; }

        ReactiveCommand<Unit, bool> Undo { get; }
        ReactiveCommand<Unit, bool> Redo { get; }

        ReactiveCommand<IGraphics, bool> Draw { get; }
    }
}
