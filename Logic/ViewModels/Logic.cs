using DataStructures.Geometry;
using Interfaces;
using Logic.Graphics;
using ReactiveUI;
using System.Reactive;

namespace Logic.ViewModels
{
    public abstract class Logic : ReactiveObject, ILogic
    {
        public Point2d DefaultSize { get; set; }
        public IDrawable SelectionStyle { get; set; } = new Drawable(new DataStructures.Color(0, 0, 0, 0), new DataStructures.Color(255, 0, 0, 255), 3)
        {
            IsNoFill = true,
            IsNoOutLine = false
        };

        public int StackStateSize { get; set; }
        public int StateIndex { get; protected set; }

        public abstract IEnumerable<int> SelectedFigures { get; }

        public ReactiveCommand<string, IFigure?> CreateFigure { get; }
        public ReactiveCommand<IDrawableObject, int> AddFigure { get; }
        public ReactiveCommand<int, IDrawableObject?> GetFigureByID { get; }
        public ReactiveCommand<int, bool> RemoveFigure { get; }

        public ReactiveCommand<Point2d, int> SelectFigure { get; }
        public ReactiveCommand<Rect, bool> SelectFigures { get; }

        public ReactiveCommand<Unit, IEnumerable<(string CommandName, ReactiveCommand<Point2d, bool> Command)>> GetContextCommands { get; }

        public ReactiveCommand<Stream, bool> Save { get; }
        public ReactiveCommand<Stream, bool> Load { get; }

        public ReactiveCommand<Unit, bool> Undo { get; }
        public ReactiveCommand<Unit, bool> Redo { get; }

        public ReactiveCommand<IGraphics, bool> Draw { get; }



        public Logic()
        {
            CreateFigure = ReactiveCommand.Create<string, IFigure?>(a => OnCreate(a));
            AddFigure = ReactiveCommand.Create<IDrawableObject, int>(a => OnAdd(a));
            GetFigureByID = ReactiveCommand.Create<int, IDrawableObject?>(a => OnGetFigureByID(a));
            RemoveFigure = ReactiveCommand.Create<int, bool>(a => OnRemove(a));

            SelectFigure = ReactiveCommand.Create<Point2d, int>(a => OnSelectFigure(a));
            SelectFigures = ReactiveCommand.Create<Rect, bool>(a => OnSelectFigures(a));

            GetContextCommands = ReactiveCommand.Create<Unit, IEnumerable<(string, ReactiveCommand<Point2d, bool>)>>(a => OnGetContextCommands());

            Save = ReactiveCommand.Create<Stream, bool>(a => OnSave(a));
            Load = ReactiveCommand.Create<Stream, bool>(a => OnLoad(a));

            Undo = ReactiveCommand.Create<Unit, bool>(a => OnUndo());
            Redo = ReactiveCommand.Create<Unit, bool>(a => OnRedo());

            Draw = ReactiveCommand.Create<IGraphics, bool>(a => OnDraw(a));
        }



        protected abstract IFigure? OnCreate(string name);

        protected abstract IDrawableObject? OnGetFigureByID(int id);

        protected abstract int OnAdd(IDrawableObject figure);

        protected abstract bool OnRemove(int id);

        protected abstract int OnSelectFigure(Point2d point);

        protected abstract bool OnSelectFigures(Rect rect);

        protected abstract IEnumerable<(string, ReactiveCommand<Point2d, bool>)> OnGetContextCommands();

        protected abstract bool OnSave(Stream a);

        protected abstract bool OnLoad(Stream a);

        protected abstract bool OnUndo();

        protected abstract bool OnRedo();

        protected abstract bool OnDraw(IGraphics graphics);
    }
}
