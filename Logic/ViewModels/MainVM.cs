using Geometry.Figures;
using Interfaces;
using ReactiveUI;
using System.Reactive;

namespace Logic.ViewModels
{
    public class MainVM : ILogic
    {
        public string Temp { get; set; } = "aaaa";
        public ReactiveCommand<string, IFigure> CreateFigure { get; set; }

        public ReactiveCommand<IFigure, int> AddFigure => throw new NotImplementedException();

        public ReactiveCommand<IFigure, Unit> RemoveFigure => throw new NotImplementedException();

        public ReactiveCommand<int, IFigure> GetFigureById => throw new NotImplementedException();

        public IEnumerable<(IFigure, IDrawable)> Figures { get; }

        public IEnumerable<(IFigure, IDrawable)> SelectedFigures => throw new NotImplementedException();
        public MainVM()
        {
            //Temp = "Hellop";
            //Figures = new ObservableCollection<(IFigure,IDrawable)>().AsEnumerable();
            CreateFigure = ReactiveCommand.Create<string, IFigure>(OnCreate);
            CreateFigure.Subscribe(value => { Temp = "a"; });
        }
        IFigure OnCreate(string name)
        {
            return new Line();
        }
    }
}
