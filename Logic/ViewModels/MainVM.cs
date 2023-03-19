using DataStructures.Geometry;
using Geometry;
using Interfaces;
using Logic.Graphics;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;

namespace Logic.ViewModels
{
    public class MainVM : ReactiveObject, ILogic
    {
        [Reactive] public string Temp { get; set; }
        public ReactiveCommand<string, IFigure> CreateFigure { get; set; }

        public ReactiveCommand<IDrawableObject, int> AddFigure { get; set; }

        public ReactiveCommand<int, Unit> RemoveFigure { get; }
        public ReactiveCommand<Point2d, int> SelectFigure { get; }

        public ReactiveCommand<int, IDrawableObject?> GetFigureById { get; }

        private Dictionary<int, IDrawableObject> _figures;
        public IReadOnlyDictionary<int, IDrawableObject> Figures => _figures;

        public Point2d DefaultSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int StackStateSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int StateIndex => throw new NotImplementedException();

        public IEnumerable<int> SelectedFigures { get; set; }

        ReactiveCommand<int, bool> ILogic.RemoveFigure => throw new NotImplementedException();

        public ReactiveCommand<Rect, int> SelectFigures => throw new NotImplementedException();

        public ReactiveCommand<Stream, bool> Save => throw new NotImplementedException();

        public ReactiveCommand<Stream, bool> Load => throw new NotImplementedException();

        public ReactiveCommand<Unit, bool> Undo => throw new NotImplementedException();

        public ReactiveCommand<Unit, bool> Redo => throw new NotImplementedException();

        public ReactiveCommand<IGraphics, bool> Draw => throw new NotImplementedException();

        private int _currentId = 0;
        public MainVM()
        {
            _figures = new Dictionary<int,IDrawableObject>();
            SelectedFigures = new List<int>();
            //Temp = "Hellop";
            //Figures = new ObservableCollection<(IFigure,IDrawable)>().AsEnumerable();
            CreateFigure = ReactiveCommand.Create<string, IFigure>((a) => OnCreate(a));
            AddFigure = ReactiveCommand.Create<IDrawableObject, int>((a) => OnAdd(a));
            RemoveFigure = ReactiveCommand.Create<int, Unit>((a) => OnRemove(a));
            SelectFigure = ReactiveCommand.Create<Point2d, int>((a) => OnSelectFigure(a));
            GetFigureById = ReactiveCommand.Create<int, IDrawableObject?>((a) => OnGetFigureById(a));

            //Observable.Subscribe()
        }

        IDrawableObject? OnGetFigureById(int id)
        {
            return _figures.TryGetValue(id, out IDrawableObject figure) ? figure : null;
        }
        IFigure? OnCreate(string name)
        {
            var fabric = FigureFabric.Create();
            return fabric.CreateFigure(name);
        }

        int OnAdd(IDrawableObject figure)
        {
            _figures.Add(_currentId++, figure);
            SelectedFigures = SelectedFigures.Append(_currentId - 1);
            return _currentId - 1;
        }
        private Unit OnRemove(int id)
        {
            _figures.Remove(id);
            return Unit.Default;
        }
        private int OnSelectFigure(Point2d point)
        {
            KeyValuePair<int, IDrawableObject> selectedFigures = _figures.Where(pair => pair.Value.Figure.IsInside(new Vector2((float)point.X, (float)point.Y), (float)1e-5)).First();
            return selectedFigures.Key;
        }
    }
}
