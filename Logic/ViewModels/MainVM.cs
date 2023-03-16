using Geometry;
using Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
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

    }
}
