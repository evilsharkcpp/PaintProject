using Geometry;
using Interfaces;
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

        public ReactiveCommand<(IFigure, IDrawable), int> AddFigure { get; set; }

        public ReactiveCommand<int, Unit> RemoveFigure { get; }
        public ReactiveCommand<Point, int> SelectFigure { get; }

        public ReactiveCommand<int, (IFigure, IDrawable)?> GetFigureById { get; }

        private Dictionary<int, (IFigure, IDrawable)> _figures;
        public IReadOnlyDictionary<int, (IFigure, IDrawable)> Figures => _figures;

        public IEnumerable<(IFigure, IDrawable)> SelectedFigures { get; set; }
        private int _currentId = 0;
        public MainVM()
        {
            _figures = new Dictionary<int,(IFigure, IDrawable)>();
            //Temp = "Hellop";
            //Figures = new ObservableCollection<(IFigure,IDrawable)>().AsEnumerable();
            CreateFigure = ReactiveCommand.Create<string, IFigure>((a) => OnCreate(a));
            AddFigure = ReactiveCommand.Create<(IFigure, IDrawable), int>((a) => OnAdd(a));
            RemoveFigure = ReactiveCommand.Create<int, Unit>((a) => OnRemove(a));
            SelectFigure = ReactiveCommand.Create<Point, int>((a) => OnSelectFigure(a));
            GetFigureById = ReactiveCommand.Create<int, (IFigure, IDrawable)?>((a) => OnGetFigureById(a));

            //Observable.Subscribe()
        }

        (IFigure, IDrawable)? OnGetFigureById(int id)
        {
            return _figures.TryGetValue(id, out (IFigure, IDrawable) figure) ? figure : null;
        }
        IFigure? OnCreate(string name)
        {
            var fabric = FigureFabric.Create();
            return fabric.CreateFigure(name);
        }

        int OnAdd((IFigure, IDrawable) figure)
        {
            _figures.Add(_currentId++, figure);
            return _currentId - 1;
        }
        private Unit OnRemove(int id)
        {
            _figures.Remove(id);
            return Unit.Default;
        }
        private int OnSelectFigure(Point point)
        {
            KeyValuePair<int, (IFigure, IDrawable)> selectedFigures = _figures.Where(pair => pair.Value.Item1.IsInside(new Vector2((float)point.X, (float)point.Y), 1e-5)).First();
            return selectedFigures.Key;
        }

    }
}
