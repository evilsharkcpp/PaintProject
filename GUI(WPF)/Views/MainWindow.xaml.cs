using DataStructures.Geometry;
using Geometry.Figures;
using Interfaces;
using Logic.ViewModels;
using System;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using GUI_WPF.Graphics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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
         get { return _canvasScale;  }
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

      DispatcherTimer _timer;
      public MainWindow()
      {
         InitializeComponent();
         _vm = new MainVM();
         _graphics = new Graphics.Graphic(canvas);
         DataContext = _vm;
         _vm.CreateFigure.Subscribe();
         //var fabric = FigureFabric.Create();
         _timer = new DispatcherTimer();
         _timer.Tick += new EventHandler(Draw);
         _timer.Interval = new TimeSpan(0, 0, 0, 0, 17);
         _timer.Start();
         canvasTransStartPoint = new Point();
         mouseDownPoint = new Point();
         mouseMovePoint = new Point();
         //_selectedFigure = fabric?.CreateFigure("Line", new Point2d(0, 0), new Point2d(100, 100));
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
         if (_selectedFigure != null)
            _selectedFigure.Draw(_graphics);
      }
      private void Button_Click(object sender, RoutedEventArgs e)
      {
         var fabric = FigureFabric.Create();
         _selectedFigure = fabric?.CreateFigure("Line", new Point2d(0, 0), new Point2d(0, 0));
         _vm.Figures = _vm.Figures.Append((_selectedFigure, new Drawable(new DataStructures.Color(main.SelectedColor.A, main.SelectedColor.R, main.SelectedColor.G, main.SelectedColor.B),
             new DataStructures.Color(main.SelectedColor.A, main.SelectedColor.R, main.SelectedColor.G, main.SelectedColor.B))));
         //_test.Draw(_graphics);
      }

      private void canvas_MouseMove(object sender, MouseEventArgs e)
      {
         var point = e.GetPosition(canvas);
         PreviousPoint = new Point(point.X, point.Y);
         if (Keyboard.IsKeyDown(Key.LeftShift) && canvasTranslateState)
         {
            mouseMovePoint = e.GetPosition(this);
            var sub = Point.Subtract(mouseMovePoint, mouseDownPoint);
            Trace.WriteLine(sub.X + " " + sub.Y);
            Trace.WriteLine(canvasTransStartPoint.X + " " + canvasTransStartPoint.Y);
            canvasTranslate.X = canvasTransStartPoint.X + sub.X;
            canvasTranslate.Y = canvasTransStartPoint.Y + sub.Y;
         }
         if (_selectedFigure != null)
            _selectedFigure.PointParameters.Where(p => p.Name == "Point2").First().Value = new Point2d(PreviousPoint.X, PreviousPoint.Y);
      }

      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged([CallerMemberName] string name = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
      }

      private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
      {
         var point = e.GetPosition(canvas);
         MouseDownPoint = new Point(point.X, point.Y);
         if (Keyboard.IsKeyDown(Key.LeftShift))
         {
            mouseDownPoint = e.GetPosition(this);
            canvasTransStartPoint.X = canvasTranslate.X;
            canvasTransStartPoint.Y = canvasTranslate.Y;
            canvasTranslateState = true;
         }
         if (_selectedFigure != null)
            _selectedFigure.PointParameters.Where(p => p.Name == "Point1").First().Value = new Point2d(MouseDownPoint.X, MouseDownPoint.Y);
      }

      private void scaleUp()
      {
         if (CanvasScale < 2.5)
         {
            canvasST.CenterX = PreviousPoint.X;
            canvasST.CenterY = PreviousPoint.Y;
            CanvasScale = CanvasScale + 0.05;
            ScaleString = Math.Round(CanvasScale * 100).ToString() + "%";
         }
      }

      private void scaleDown()
      {
         if (CanvasScale > 0.6)
         {
            canvasST.CenterX = PreviousPoint.X;
            canvasST.CenterY = PreviousPoint.Y;
            CanvasScale = CanvasScale - 0.05;
            ScaleString = Math.Round(CanvasScale * 100).ToString() + "%";
         }
      }

      private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         if (e.Delta > 0)
            scaleUp();
         else
            scaleDown();
      }

      private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         canvasTranslateState = false;
      }

      private void scaleDownButtonDown(object sender, MouseButtonEventArgs e)
      {
         scaleDown();
      }

      private void scaleUpButtonDown(object sender, MouseButtonEventArgs e)
      {
         scaleUp();
      }
   }
}
