﻿using DataStructures.Geometry;
using Geometry;
using Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Drawing;
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

        public ReactiveCommand<IFigure, Unit> RemoveFigure { get; }
        public ReactiveCommand<Point2d, Unit> SelectFigure { get; }


        public ReactiveCommand<int, IFigure> GetFigureById => throw new NotImplementedException();

        public IEnumerable<(IFigure, IDrawable)> Figures { get; set; }

        public IEnumerable<(IFigure, IDrawable)> SelectedFigures { get; set; }
        public MainVM()
        {
            Figures = new List<(IFigure, IDrawable)>();
            //Temp = "Hellop";
            //Figures = new ObservableCollection<(IFigure,IDrawable)>().AsEnumerable();
            CreateFigure = ReactiveCommand.Create<string, IFigure>((a) => OnCreate(a));
            AddFigure = ReactiveCommand.Create<(IFigure, IDrawable), int>((a) => OnAdd(a));
            RemoveFigure = ReactiveCommand.Create<IFigure, Unit>((a) => OnRemove(a));
            SelectFigure = ReactiveCommand.Create<Point2d, Unit>((a) => OnSelectFigure(a));


            //Observable.Subscribe()
        }
        IFigure? OnCreate(string name)
        {
            var fabric = FigureFabric.Create();
            return fabric.CreateFigure(name);
        }

        int OnAdd((IFigure, IDrawable) figure)
        {
            Figures = Figures.Append((figure.Item1, figure.Item2));
            SelectedFigures = new List<(IFigure, IDrawable)>() { figure };
            return Figures.Count() - 1;
        }
        private Unit OnRemove(IFigure figure)
        {
            Figures = Figures.Where(item => item.Item1 != figure);
            SelectedFigures = SelectedFigures.Where(item => item.Item1 != figure);
            return Unit.Default;
        }
        private Unit OnSelectFigure(Point2d point)
        {
            List<(IFigure, IDrawable)> selectedFigures = new List<(IFigure, IDrawable)>();

            foreach (var figure in Figures.Reverse())
            {
                if (figure.Item1.IsInside(new Vector2((float)point.X, (float)point.Y), 1e-5))
                {
                    selectedFigures.Add(figure);
                    break;
                }
            }
            SelectedFigures = selectedFigures;
            return Unit.Default;
        }

    }
}
