using DataStructures.Geometry;
using Geometry;
using Interfaces;
using IO;
using Logic.Utils;
using ReactiveUI;
using System;
using System.Numerics;
using System.Reactive.Linq;

namespace Logic.ViewModels
{
    public class MainVM : Logic
    {
        private class Object
        {
            public IDrawableObject? DrawableObject;
            public bool IsSelected;
        }

        private int _currentId = 0;

        private Dictionary<int, Object> _figures;
        private List<int> _sortedFigures;

        private int _selectedFigure;
        private List<int> _selectedFigures;
        private FigureBound _figureBound;

        public override IEnumerable<int> SelectedFigures => _selectedFigures;
        public override IFigureBound FigureBound => _figureBound;

        public MainVM()
        {
            _figures = new Dictionary<int, Object>();
            _sortedFigures = new List<int>();

            _selectedFigures = new List<int>();
            _figureBound = new FigureBound();
        }


        private void ResetSelect()
        {
            foreach (KeyValuePair<int, Object> pair in _figures)
            {
                pair.Value.IsSelected = false;
            }

            _figureBound.Figures = Array.Empty<IFigure>();
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

            _figureBound.Figures = new IFigure?[] { figure };
            return figure;
        }

        protected override int OnAdd(IDrawableObject figure)
        {
            ResetSelect();

            _figures.Add(_currentId, new Object() { DrawableObject = figure, IsSelected = false });
            _sortedFigures.Insert(0, _currentId++);

            _selectedFigure = _currentId - 1;
            _selectedFigures.Clear();
            _selectedFigures.Add(_selectedFigure);
            _figureBound.Figures = _selectedFigures.Select(id => _figures[id].DrawableObject?.Figure);
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
                _sortedFigures.Remove(id);
                _figures.Remove(id);
                successfully = true;
            }

            return successfully;
        }

        protected override int OnSelectFigure(Point2d point)
        {
            bool found = _selectedFigure < 0;
            int selectedFigure = _selectedFigure,
                proxySelectedFigure = -1,
                newSelectedFigure = -1;

            ResetSelect();
            Vector2 p = new Vector2((float)point.X, (float)point.Y);
            for (int i = 0; i < _sortedFigures.Count; i++)
            {
                int id = _sortedFigures[i];
                if (_figures.TryGetValue(id, out Object? @object))
                {
                    if (found)
                    {
                        if (@object.DrawableObject is not null &&
                            @object.DrawableObject.Figure is not null &&
                            @object.DrawableObject.Figure.IsInside(p, 10))
                        {
                            newSelectedFigure = id;
                            break;
                        }
                    }
                    else
                    {
                        if (proxySelectedFigure == -1)
                        {
                            if (@object.DrawableObject is not null &&
                                @object.DrawableObject.Figure is not null &&
                                @object.DrawableObject.Figure.IsInside(p, 10))
                            {
                                proxySelectedFigure = id;
                            }
                        }
                        found = id == selectedFigure;
                    }
                }
            }


            _selectedFigure = newSelectedFigure != -1 ? newSelectedFigure : proxySelectedFigure;
            if (_selectedFigure >= 0)
            {
                _selectedFigures.Add(_selectedFigure);
                _figures[_selectedFigure].IsSelected = true;
            }

            _figureBound.Figures = _selectedFigures.Select(id => _figures[id].DrawableObject?.Figure);
            return _selectedFigure;
        }

        protected override bool OnSelectFigures(Rect rect)
        {
            ResetSelect();

            _selectedFigures.AddRange(_sortedFigures.Where(id =>
            {
                bool successfully = false;

                if (_figures.TryGetValue(id, out Object? @object) &&
                    @object.DrawableObject is not null &&
                    @object.DrawableObject.Figure is not null)
                {
                    successfully = @object.DrawableObject.Figure.InArea(rect, -10);
                    @object.IsSelected = successfully;
                }

                return successfully;
            }));

            _figureBound.Figures = _selectedFigures.Select(id => _figures[id].DrawableObject?.Figure);
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
            if (_figures.TryGetValue(id, out Object? @object))
            {
                _sortedFigures.Remove(id);
                _sortedFigures.Insert(_sortedFigures.Count, id);
            }

            return true;
        }

        private bool SendBackward(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object))
            {
                int index = _sortedFigures.IndexOf(id);
                if (index < _sortedFigures.Count - 1)
                {
                    _sortedFigures.Remove(id);
                    _sortedFigures.Insert(index + 1, id);
                }
            }

            return true;
        }

        private bool BringForward(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object))
            {
                int index = _sortedFigures.IndexOf(id);
                if (index > 0)
                {
                    _sortedFigures.Remove(id);
                    _sortedFigures.Insert(index - 1, id);
                }
            }

            return true;
        }

        private bool BringToFront(int id)
        {
            if (_figures.TryGetValue(id, out Object? @object))
            {
                _sortedFigures.Remove(id);
                _sortedFigures.Insert(0, id);
            }

            return true;
        }

        protected override bool OnSave(Stream a)
        {
            IConverter converter = null;
            switch (Path.GetExtension((a as FileStream).Name))
            {
                case ".svg":
                    converter = new SVGConverter();
                    break;
                case ".json":
                    converter = new JSONConverter();
                    break;
                case ".png":
                    converter = new PNGConverter();
                    break;
                case ".jpeg":
                case ".jpg":
                    converter = new JPEGConverter();
                    break;
                case ".bmp":
                    converter = new BMPConverter();
                    break;
                case ".gif":
                    converter = new GIFConverter();
                    break;
                case ".tiff":
                    converter = new TIFFConverter();
                    break;
                default:
                    return false;
            }
            List<IDrawableObject> objects = new List<IDrawableObject>();
            foreach (var item in _figures)
                objects.Add(item.Value.DrawableObject);
            converter.WriteFile(a, objects);
            return true;
        }

        protected override bool OnLoad(Stream a)
        {
            IConverter converter = null;
            switch(Path.GetExtension((a as FileStream).Name))
            {
                case ".svg":
                    converter = new SVGConverter();
                    break;
                case ".json":
                    converter = new JSONConverter();
                    break;
                default:
                    return false;
            }
            var objects = converter.ReadFile(a);
    
            OnClear();
            foreach(var item in objects)
            {
                OnAdd(item);
            }
            return true;
        }
        
        protected override bool OnClear()
        {
            _figures.Clear();
            _currentId = 0;
            _selectedFigures.Clear();
            return true;
        }
        protected override bool OnUndo()
        {

            return false;
        }

        protected override bool OnRedo()
        {

            return false;
        }

        protected override bool OnDraw(IGraphics graphics)
        {
            for (int i = _sortedFigures.Count - 1; i >= 0; i--)
            {
                int id = _sortedFigures[i];
                if (_figures.TryGetValue(id, out Object? @object) &&
                    @object.DrawableObject is not null &&
                    @object.DrawableObject.Figure is not null &&
                    @object.DrawableObject.Drawable is not null)
                {
                    graphics.GraphicStyle = @object.DrawableObject.Drawable;

                    IFigure figure = @object.DrawableObject.Figure;
                    figure.Draw(graphics);
                }
            }

            if (_selectedFigures.Count > 0)
            {
                graphics.GraphicStyle = SelectionStyle;
                _figureBound.Draw(graphics);
            }

            return true;
        }
    }
}
