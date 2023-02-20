using Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Geometry.Figures;
using System.Collections.ObjectModel;
using DynamicData.Kernel;

namespace GUI_WPF.ViewModels
{
    class MainVM : ILogic
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
            CreateFigure = ReactiveCommand.Create<string, IFigure>(name => {  return new Line(); });
            CreateFigure.Subscribe(value => { Temp = "a"; });
        }
    }
}
