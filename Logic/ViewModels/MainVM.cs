using DataStructures.Geometry;
using Geometry;
using Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reactive.Linq;
using IO;

namespace Logic.ViewModels
{
    public class MainVM : Logic
    {
        private class Object
        {
            public IDrawableObject? DrawableObject;
            public int ZIndex;
            public bool IsSelected;
        }

        private int _currentId = 0;

        private Dictionary<int, Object> _figures;
        private List<SortedSet<int>> _sortedFigures;

        private int _selectedFigure;
        private List<int> _selectedFigures;

        public override IEnumerable<int> SelectedFigures => _selectedFigures;


        public IEnumerable<IDrawableObject> SelectedFigures { get; set; }
        private int _currentId = 0;
        private readonly Dictionary<int, (IFigure, IDrawable)> _shapes;
        public ReactiveCommand<string, Unit> SaveFile { get; }
        public ReactiveCommand<string, IEnumerable<(IFigure, IDrawable)>> OpenFile { get; }
    public MainVM()
        {
            _figures = new Dictionary<int, Object>();
            _sortedFigures = new List<SortedSet<int>>() { new SortedSet<int>() };
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
             _sortedFiguresID = new SortedSet<int>(Comparer<int>.Create((id1, id2) =>
            {
                int cmp = -1;
                if (_figures[id1].Figure is not null &&
                    _figures[id2].Figure is not null)
                {
                    cmp = _figures[id1].Figure!.ZIndex.CompareTo(_figures[id2].Figure!.ZIndex);
                    if (cmp == 0)
                        cmp = -1;
                }
                return cmp;
            }));

            _selectedFigures = new List<int>();
            //Observable.Subscribe()
    }


        private void ResetSelect()
        {
            foreach (KeyValuePair<int, Object> pair in _figures)
            {
                pair.Value.IsSelected = false;
            }

            _selectedFigure = -1;
            _selectedFigures.Clear();
        }


        protected override IFigure? OnCreate(string name)
        {
            ResetSelect();

            FigureFabric fabric = FigureFabric.Create();
            IFigure? figure = fabric.CreateFigure(name);
            if (figure is not null)
            {
                figure.Size = DefaultSize;
            }

            return fabric.CreateFigure(name);
        }

        protected override int OnAdd(IDrawableObject figure)
        {
            ResetSelect();

            _figures.Add(_currentId, new Object() { DrawableObject = figure, IsSelected = true, ZIndex = _sortedFigures.Count - 1 });
            _sortedFigures.Last().Add(_currentId++);

            _selectedFigure = _currentId - 1;
            _selectedFigures.Clear();
            _selectedFigures.Add(_selectedFigure);

            return _currentId - 1;
        }

        protected override IDrawableObject? OnGetFigureByID(int id)
        {
            return _figures.TryGetValue(id, out Object? value) ? value.DrawableObject : null;
        }

        protected override bool OnRemove(int id)
        {
            ResetSelect();

            bool successfully = false;
            if (_figures.TryGetValue(id, out Object? @object))
            {
                _sortedFigures[@object.ZIndex].Remove(id);
                _figures.Remove(id);
               // _sortedFiguresID.Remove(id);
                _isSelected.Remove(id);
                successfully = true;
            }

            return successfully;
        }

        protected override int OnSelectFigure(Point2d point)
        {
            ResetSelect();
            bool found = _selectedFigure < 0;
            int selectedFigure = _selectedFigure,
                proxySelectedFigure = -1,
                newSelectedFigure = -1;

            ResetSelect();
            Vector2 p = new Vector2((float)point.X, (float)point.Y);
            foreach (KeyValuePair<int, Object> pair in _figures)
            {
                if (found)
                {
                    if (pair.Value.DrawableObject is not null && 
                        pair.Value.DrawableObject.Figure is not null &&
                        pair.Value.DrawableObject.Figure.IsInside(p, 3))
                    {
                        newSelectedFigure = pair.Key;
                        break;
                    }
                }
                else
                {
                    if (proxySelectedFigure == -1)
                    {
                        if (pair.Value.DrawableObject is not null &&
                            pair.Value.DrawableObject.Figure is not null &&
                            pair.Value.DrawableObject.Figure.IsInside(p, 3))
                        {
                            proxySelectedFigure = pair.Key;
                        }
                    }
                    found = pair.Key == selectedFigure;
                }
            }


            _selectedFigure = newSelectedFigure != -1 ? newSelectedFigure : proxySelectedFigure;
            if (_selectedFigure >= 0)
            {
                _selectedFigures.Add(_selectedFigure);
                _figures[_selectedFigure].IsSelected = true;
            }

            return _selectedFigure;
        }

        protected override bool OnSelectFigures(Rect rect)
        {
            ResetSelect();

            _selectedFigures.AddRange(_figures.Where(pair =>
            {
                bool successfully = false;

                if (pair.Value.DrawableObject is not null &&
                    pair.Value.DrawableObject.Figure is not null)
                {
                    successfully = pair.Value.DrawableObject.Figure.InArea(rect, 3);
                    pair.Value.IsSelected = successfully;
                }

                return successfully;
            }).Select(pair => pair.Key));

            return false;
        }

        protected override IEnumerable<(string, ReactiveCommand<Point2d, bool>)> OnGetContextCommands()
        {
            List<(string, ReactiveCommand<Point2d, bool>)> commands = new List<(string, ReactiveCommand<Point2d, bool>)>();

            if (_selectedFigure >= 0)
            {
                commands.AddRange(new (string, ReactiveCommand<Point2d, bool>)[]
                {
                    ("Удалить", ReactiveCommand.Create<Point2d, bool>(point => OnRemove(_selectedFigure))),

                    ("На задний план", ReactiveCommand.Create<Point2d, bool>(point => SendToBack(_selectedFigure))),
                    ("Назад", ReactiveCommand.Create<Point2d, bool>(point => SendBackward(_selectedFigure))),
                    ("Вперёд", ReactiveCommand.Create<Point2d, bool>(point => BringForward(_selectedFigure))),
                    ("На передний план", ReactiveCommand.Create<Point2d, bool>(point => BringToFront(_selectedFigure))),
                });

                if (_figures.TryGetValue(_selectedFigure, out Object? @object) &&
                    @object.DrawableObject is not null &&
                    @object.DrawableObject.Figure is not null)
                {
                    commands.AddRange(@object.DrawableObject.Figure.Commands);
                }
            }

            return commands;
        }

        private bool SendToBack(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object) && @object.ZIndex > 0)
            {
                int currentZIndex = @object.ZIndex;

                _sortedFigures[@object.ZIndex].Remove(id);
                _sortedFigures[@object.ZIndex = 0].Add(id);

                if (_sortedFigures.Last().Count == 0)
                    _sortedFigures.RemoveAt(_sortedFigures.Count - 1);

                if (_sortedFigures[currentZIndex].Count == 0)
                {
                    for (int i = currentZIndex; i < _sortedFigures.Count; i++)
                    {
                        foreach (int id_ in _sortedFigures[i])
                        {
                            if (_figures.TryGetValue(id_, out Object? @object_))
                                @object_.ZIndex--;
                        }

                        _sortedFigures[i - 1] = _sortedFigures[i];
                    }
                }
            }

            return true;
        }

        private bool SendBackward(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object) && @object.ZIndex > 0)
            {
                int currentZIndex = @object.ZIndex;

                _sortedFigures[@object.ZIndex].Remove(id);
                _sortedFigures[--@object.ZIndex].Add(id);

                if (_sortedFigures.Last().Count == 0)
                    _sortedFigures.RemoveAt(_sortedFigures.Count - 1);

                if (_sortedFigures[currentZIndex].Count == 0)
                {
                    for (int i = currentZIndex; i < _sortedFigures.Count; i++)
                    {
                        foreach (int id_ in _sortedFigures[i])
                        {
                            if (_figures.TryGetValue(id_, out Object? @object_))
                                @object_.ZIndex--;
                        }

                        _sortedFigures[i - 1] = _sortedFigures[i];
                    }
                }
            }

            return true;
        }

        private bool BringForward(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object) &&
                (_sortedFigures.Count > @object.ZIndex ||
                 _sortedFigures.Last().Count > 1))
            {
                int currentZIndex = @object.ZIndex;

                _sortedFigures[@object.ZIndex].Remove(id);

                @object.ZIndex++;
                if (_sortedFigures.Count <= @object.ZIndex)
                {
                    _sortedFigures.Add(new SortedSet<int>() { id });
                }
                else
                {
                    _sortedFigures[@object.ZIndex].Add(id);
                }

                if (_sortedFigures[currentZIndex].Count == 0)
                {
                    for (int i = currentZIndex; i < _sortedFigures.Count; i++)
                    {
                        foreach (int id_ in _sortedFigures[i])
                        {
                            if (_figures.TryGetValue(id_, out Object? @object_))
                                @object_.ZIndex--;
                        }

                        _sortedFigures[i - 1] = _sortedFigures[i];
                    }
                }
            }

            return true;
        }

        private bool BringToFront(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object) &&
                (_sortedFigures.Count > @object.ZIndex ||
                 _sortedFigures.Last().Count > 1))
            {
                int currentZIndex = @object.ZIndex;

                _sortedFigures[@object.ZIndex].Remove(id);
                @object.ZIndex = _sortedFigures.Count;
                _sortedFigures.Add(new SortedSet<int>() { id });

                if (_sortedFigures[currentZIndex].Count == 0)
                {
                    for (int i = currentZIndex; i < _sortedFigures.Count; i++)
                    {
                        foreach (int id_ in _sortedFigures[i])
                        {
                            if (_figures.TryGetValue(id_, out Object? @object_))
                                @object_.ZIndex--;
                        }

                        _sortedFigures[i - 1] = _sortedFigures[i];
                    }
                }
            }

            return true;
        }

        protected override bool OnSave(Stream a)
        {

            return false;
        }

        protected override bool OnLoad(Stream a)
        {

            return false;
        }

        protected override bool OnUndo()
        {

            return false;
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
            
        protected override bool OnRedo()
        {

            return false;
        }

        protected override bool OnDraw(IGraphics graphics)
        {
            Point2d start = new Point2d(double.MaxValue, double.MaxValue),
                    end = new Point2d(double.MinValue, double.MinValue);

            foreach (SortedSet<int> set in _sortedFigures)
            {
                foreach (int id in set)
                {
                    if (_figures.TryGetValue(id, out Object? @object) &&
                        @object.DrawableObject is not null &&
                        @object.DrawableObject.Figure is not null &&
                        @object.DrawableObject.Drawable is not null)
                    {
                        graphics.GraphicStyle = @object.DrawableObject.Drawable;

                        IFigure figure = @object.DrawableObject.Figure;
                        figure.Draw(graphics);

                        if (@object.IsSelected)
                        {
                            if (figure.Position.X < start.X)
                                start.X = figure.Position.X;

                            if (figure.Position.Y < start.Y)
                                start.Y = figure.Position.Y;

                            if (figure.Position.X + figure.Size.X > end.X)
                                end.X = figure.Position.X + figure.Size.X;

                            if (figure.Position.Y + figure.Size.Y > end.Y)
                                end.Y = figure.Position.Y + figure.Size.Y;
                        }
                    }
                }
            }

            if (_selectedFigures.Count > 0)
            {
                start.X -= 5;
                start.Y -= 5;
                end.X += 5;
                end.Y += 5;

                graphics.GraphicStyle = SelectionStyle;
                graphics.ModelMatrix = new Matrix3d();
                graphics.DrawRectangle(start, end.X - start.X, end.Y - start.Y, false, true);
            }

            return true;
        }
    }
}
