using DataStructures.Geometry;
using Geometry.Figures;
using Interfaces;
using Logic.ViewModels;
using System;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using GUI_WPF.Graphics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace GUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ILogic _vm;
        IGraphics _graphics;
        IFigure _test;
        Point _previousPoint;
        Point _mouseDownPoint;
        public Point PreviousPoint
        {
            get { return _previousPoint; }
            set
            {
                _previousPoint = value;
                OnPropertyChanged();
            }
        }
        public Point MouseDownPoint
        {
            get { return _mouseDownPoint; }
            set
            {
                _mouseDownPoint = value;
                OnPropertyChanged();
            }
        }

        DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainVM();
            _graphics = new Graphics.Graphic(canvas);
            DataContext = _vm;
            _vm.CreateFigure.Subscribe();
            var fabric = FigureFabric.Create();
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(Draw);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 17);
            _timer.Start();
            _test = fabric?.CreateFigure("Line", new Point2d(0, 0), new Point2d(100, 100));
        }
        private void Draw(object sender, EventArgs e)
        {
            canvas.Children.Clear();
            if (_vm.Figures == null) return;
            foreach (var item in _vm.Figures)
            {
                _graphics.GraphicStyle = item.Item2;
                item.Item1.Draw(_graphics);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.Figures = _vm.Figures.Append((_test, new Drawable(new DataStructures.Color(main.SelectedColor.A, main.SelectedColor.R, main.SelectedColor.G, main.SelectedColor.B),
                new DataStructures.Color(main.SelectedColor.A, main.SelectedColor.R, main.SelectedColor.G, main.SelectedColor.B))));
            //_test.Draw(_graphics);
        }

        private void canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var point = e.GetPosition(this);
            PreviousPoint = new Point(point.X, point.Y);
        }

    }
}
