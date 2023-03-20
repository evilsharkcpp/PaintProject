using DataStructures.Geometry;
using ReactiveUI;
using System.Reactive;

namespace Interfaces
{
    public interface ILogic
    {
        Point2d DefaultSize { get; set; }
        int StackStateSize { get; set; }
        int StateIndex { get; }

        IEnumerable<int> SelectedFigures { get; }

        ReactiveCommand<string, IFigure?> CreateFigure { get; }
        ReactiveCommand<IDrawableObject, int> AddFigure { get; }
        ReactiveCommand<int, IDrawableObject?> GetFigureByID { get; }
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
