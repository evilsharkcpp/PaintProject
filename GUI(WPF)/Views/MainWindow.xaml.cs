using Interfaces;
using Logic.ViewModels;
using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
      public MainWindow()
      {
         InitializeComponent();
         _vm = new MainVM();
         _graphics = new Graphics.Graphic(canvas);
         DataContext = _vm;
         canvasTransStartPoint = new Point();
         mouseDownPoint = new Point();
         mouseMovePoint = new Point();
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
