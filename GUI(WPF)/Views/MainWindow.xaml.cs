using Interfaces;
using Logic.ViewModels;
using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReactiveUI;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Controls;
using Logic.Graphics;

namespace GUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ILogic _vm { get; set; }
        IGraphics _graphics;
        IFigure? _selectedFigure;
        Point _previousPoint;
        Point _mouseDownPoint;
        Point canvasTransStartPoint;
        Point mouseDownPoint;
        Point mouseMovePoint;
        double _canvasScale = 1.0;
        string _scaleString = "100%";
        bool canvasTranslateState = false;

        public string ScaleString
        {
            get { return _scaleString; }
            set
            {
                _scaleString = value;
                OnPropertyChanged();
            }
        }

        public double CanvasScale
        {
            get { return _canvasScale; }
            set
            {
                _canvasScale = value;
                OnPropertyChanged();
            }
        }

        public Point PreviousPoint
        {
            get { return _previousPoint; }
            set
            {
                _previousPoint.X = Math.Round(value.X, 2);
                _previousPoint.Y = Math.Round(value.Y, 2);
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
        public (IFigure, IDrawable) SelectedFigure
        {
            get
            {
                var selected = _vm.SelectedFigures.LastOrDefault();
                if (selected.Item1 == null)
                    ParamVisibility = Visibility.Hidden;
                else
                    ParamVisibility = Visibility.Visible;
                return selected;
            }
        }
        private Visibility _paramVisibility = Visibility.Hidden;
        public Visibility ParamVisibility
        {
            get
            {
                return _paramVisibility;
            }
            set
            {
                _paramVisibility = value;
                OnPropertyChanged();
            }
        }

        DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainVM();
            _vm.CreateFigure.Subscribe(AddFigure);
            _graphics = new Graphics.Graphic(canvas);
            DataContext = _vm;
            canvasTransStartPoint = new Point();
            mouseDownPoint = new Point();
            mouseMovePoint = new Point();
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(Draw);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 17);
            _timer.Start();
        }
        void Draw(object sender, EventArgs e)
        {
            canvas.Children.Clear();
            foreach(var item in _vm.Figures)
            {
                _graphics.GraphicStyle = item.Item2;
                item.Item1.Draw(_graphics);
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(canvas);
            PreviousPoint = new Point(point.X, point.Y);
            if (Keyboard.IsKeyDown(Key.LeftShift) && canvasTranslateState)
            {
                mouseMovePoint = e.GetPosition(this);
                var sub = Point.Subtract(mouseMovePoint, mouseDownPoint);
                canvasTranslate.X = canvasTransStartPoint.X + sub.X;
                canvasTranslate.Y = canvasTransStartPoint.Y + sub.Y;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void AddFigure(IFigure? figure)
        {
            if (figure != null)
            {
                //figure.Angle = Math.PI / 4.0;
                figure.Size = new DataStructures.Geometry.Vector2d(500, 340);
                figure.Position = new DataStructures.Geometry.Point2d(MouseDownPoint.X, MouseDownPoint.Y);
                _vm.AddFigure.Execute((figure, new Drawable(new DataStructures.Color(main1.SelectedColor.A, main1.SelectedColor.R, main1.SelectedColor.G, main1.SelectedColor.B),
                    new DataStructures.Color(main2.SelectedColor.A, main2.SelectedColor.R, main2.SelectedColor.G, main2.SelectedColor.B)))).Subscribe();
                _ = SelectedFigure;
            }
        }
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            var point = e.GetPosition(canvas);
            MouseDownPoint = new Point(point.X, point.Y);
            var button = commands.Items.OfType<RadioButton>().Where(x => x.IsChecked == true).FirstOrDefault();
            if (button != null)
            {
                var param = button.CommandParameter;
                if(param != null)
                {
                    _vm.CreateFigure.Execute(param.ToString()).Subscribe();
                    
                }
                select.IsChecked = true;
            }
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                mouseDownPoint = e.GetPosition(this);
                canvasTransStartPoint.X = canvasTranslate.X;
                canvasTransStartPoint.Y = canvasTranslate.Y;
                canvasTranslateState = true;
            }
        }

        private void scaleUp()
        {
            CanvasScale = CanvasScale + 0.05;
            ScaleString = Math.Round(CanvasScale * 100).ToString() + "%";
        }

        private void scaleDown()
        {
            CanvasScale = CanvasScale - 0.05;
            ScaleString = Math.Round(CanvasScale * 100).ToString() + "%";
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && CanvasScale < 2.5)
            {
                canvasST.CenterX = PreviousPoint.X;
                canvasST.CenterY = PreviousPoint.Y;
                scaleUp();
            }
            else if (CanvasScale > 0.6)
            {
                canvasST.CenterX = PreviousPoint.X;
                canvasST.CenterY = PreviousPoint.Y;
                scaleDown();
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            canvasTranslateState = false;
        }

        private void scaleDownButtonDown(object sender, RoutedEventArgs e)
        {
            if (CanvasScale > 0.6)
            {
                canvasST.CenterX = canvas.ActualWidth / 2.0;
                canvasST.CenterY = canvas.ActualHeight / 2.0;
                scaleDown();
            }
        }

        private void scaleUpButtonDown(object sender, RoutedEventArgs e)
        {
            if (CanvasScale < 2.5)
            {
                canvasST.CenterX = canvas.ActualWidth / 2.0;
                canvasST.CenterY = canvas.ActualHeight / 2.0;
                scaleUp();
            }
        }
    }
}
