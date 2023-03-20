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
using System.Reactive.Linq;
using IO;

namespace Logic.ViewModels
{
    public class MainVM : Logic
    {
        private int _currentId = 0;

        private Dictionary<int, IDrawableObject> _figures;
        private SortedSet<int> _sortedFiguresID;

        private Dictionary<int, bool> _isSelected;
        private int _selectedFigure;
        private List<int> _selectedFigures;

        public override IReadOnlyDictionary<int, IDrawableObject> Figures => _figures;
        public override IEnumerable<int> SelectedFigures => _selectedFigures;


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

            _isSelected = new Dictionary<int, bool>();
            _selectedFigures = new List<int>();
            //Observable.Subscribe()
    }


        private void ResetSelect()
        {
            foreach (KeyValuePair<int, IDrawableObject> pair in _figures)
            {
                _isSelected[pair.Key] = false;
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

            _figures.Add(_currentId, figure);
            _sortedFiguresID.Add(_currentId);
            _isSelected.Add(_currentId++, false);
            _selectedFigure = _currentId - 1;
            _selectedFigures.Clear();
            _selectedFigures.Add(_selectedFigure);
            _isSelected[_selectedFigure] = true;
            return _currentId - 1;
        }

        protected override bool OnRemove(int id)
        {
            ResetSelect();

            bool successfully = false;
            if (_figures.Remove(id))
            {
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
            int proxySelectedFigureID = -1, selectedFigureID = -1;
            Vector2 p = new Vector2((float)point.X, (float)point.Y);
            foreach (KeyValuePair<int, IDrawableObject> pair in _figures)
            {
                if (found)
                {
                    if (pair.Value.Figure is not null &&
                        pair.Value.Figure.IsInside(p, 3))
                    {
                        selectedFigureID = pair.Key;
                        break;
                    }
                }
                else
                {
                    if (proxySelectedFigureID == -1)
                    {
                        if (pair.Value.Figure is not null &&
                            pair.Value.Figure.IsInside(p, 3))
                        {
                            proxySelectedFigureID = pair.Key;
                        }
                    }
                    found = pair.Key == _selectedFigure;
                }
            }


            _selectedFigure = selectedFigureID != -1 ? selectedFigureID : proxySelectedFigureID;
            _selectedFigures.Clear();
            if (_selectedFigure >= 0)
            {
                _selectedFigures.Add(_selectedFigure);
                _isSelected[_selectedFigure] = true;
            }
            return _selectedFigure;
        }

        protected override bool OnSelectFigures(Rect rect)
        {
            ResetSelect();
            _selectedFigures.AddRange(_figures.Where(pair =>
            {
                bool successfully = false;

                if (pair.Value.Figure is not null)
                {
                    successfully = pair.Value.Figure.InArea(rect, 3);
                    _isSelected[pair.Key] = successfully;
                }

                return successfully;
            }).Select(pair => pair.Key));

            return false;
        }

        protected override IEnumerable<(string, ReactiveCommand<Point2d, bool>)> OnGetContextCommands()
        {

            return new List<(string, ReactiveCommand<Point2d, bool>)>();
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
            foreach (int id in _sortedFiguresID)
            {
                IDrawableObject drawableObject = _figures[id];
                if (drawableObject.Drawable is not null &&
                    drawableObject.Figure is not null)
                {
                    graphics.GraphicStyle = drawableObject.Drawable;

                    IFigure figure = drawableObject.Figure;
                    figure.Draw(graphics);

                    if (_isSelected[id])
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
