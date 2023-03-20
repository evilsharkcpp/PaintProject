using DataStructures.Geometry;
using Geometry;
using Interfaces;
using Logic.Graphics;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using IO;

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

        public IEnumerable<IDrawableObject> SelectedFigures { get; set; }
        private int _currentId = 0;
        private readonly Dictionary<int, (IFigure, IDrawable)> _shapes;
        public ReactiveCommand<string, Unit> SaveFile { get; }
        public ReactiveCommand<string, IEnumerable<(IFigure, IDrawable)>> OpenFile { get; }
    public MainVM()
        {
            _figures = new Dictionary<int,IDrawableObject>();
            SelectedFigures= new List<IDrawableObject>();
            //Temp = "Hellop";
            //Figures = new ObservableCollection<(IFigure,IDrawable)>().AsEnumerable();
            CreateFigure = ReactiveCommand.Create<string, IFigure>((a) => OnCreate(a));
            AddFigure = ReactiveCommand.Create<IDrawableObject, int>((a) => OnAdd(a));
            RemoveFigure = ReactiveCommand.Create<int, Unit>((a) => OnRemove(a));
            SelectFigure = ReactiveCommand.Create<Point2d, int>((a) => OnSelectFigure(a));
            GetFigureById = ReactiveCommand.Create<int, IDrawableObject?>((a) => OnGetFigureById(a));
            SaveFile = ReactiveCommand.Create<string, Unit>(OnSaveFile);
            OpenFile = ReactiveCommand.Create < string, IEnumerable < (IFigure, IDrawable)>>(OnOpenFile);
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
            SelectedFigures = SelectedFigures.Append(figure);
            return _currentId - 1;
        }
        private Unit OnRemove(int id)
        {
            _figures.Remove(id);
            return Unit.Default;
        }
        private int OnSelectFigure(Point2d point)
        {
            KeyValuePair<int, IDrawableObject> selectedFigures = _figures.Where(pair => pair.Value.Figure.IsInside(new Vector2((float)point.X, (float)point.Y), 1e-5)).First();
            return selectedFigures.Key;
        }
        private Unit OnSaveFile(string filePath)
        {
            IConverter converter = GetConverterForFilePath(filePath);

            converter.WriteFile(filePath, _shapes.Values);

            return Unit.Default;
        }

        private IEnumerable<(IFigure, IDrawable)> OnOpenFile(string filePath)
        {
            IConverter converter = GetConverterForFilePath(filePath);

            IEnumerable<(IFigure, IDrawable)> shapes = converter.ReadFile(filePath);

            _shapes.Clear();
            foreach (var shape in shapes)
            {
                _shapes.Add(_currentId++, shape);
            }

            return shapes;
        }
        private IConverter GetConverterForFilePath(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (FileValidator.JSON_EXTENSION.Contains(extension))
            {
                return new JSONConverter();
            }
            else if (FileValidator.SVG_EXTENSION.Contains(extension))
            {
                return new SVGConverter();
            }
            else
            {
                throw new NotSupportedException($"File extension {extension} is not supported.");
            }
        }
    }
}
