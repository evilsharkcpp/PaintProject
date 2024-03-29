using Interfaces;
using Logic.ViewModels;
using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Controls;
using Drawing.Graphics;
using DataStructures.Geometry;
using System.Windows.Media;
using Microsoft.Win32;
using ReactiveUI;
using System.Collections.Generic;

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
      Point tmpPoint;
      public Point2d startFigurePosition;
      public Vector2d startFigureSize;
      Vector2d newFigureSize;
      private Point2d startFigureMovePosition;
      private Dictionary<int, Point2d> selectedFiguresStartMovePosition = new Dictionary<int, Point2d>();
      double _canvasScale = 1.0;
      double _selectedFigureX, _selectedFigureY, _selectedFigureW, _selectedFigureH, _selectedFigureAngle;
      string _scaleString = "100%";

      public double SelectedFigureAngle
      {
         get { return _selectedFigureAngle; }
         set
         {
            _selectedFigureAngle = value;
            SelectedFigure.Figure.Angle = _selectedFigureAngle * Math.PI / 180.0;
            OnPropertyChanged();
         }
      }

      public FigureChangeDirection direction;

      public double SelectedFigureX
      {
         get { return _selectedFigureX; }
         set
         {
            _selectedFigureX = value;
            Point2d p = new Point2d(_selectedFigureX, SelectedFigure.Figure.Position.Y);
            SelectedFigure.Figure.Position = p;
            OnPropertyChanged();
         }
      }

      public double SelectedFigureY
      {
         get { return _selectedFigureY; }
         set
         {
            _selectedFigureY = value;
            Point2d p = new Point2d(SelectedFigure.Figure.Position.X, _selectedFigureY);
            SelectedFigure.Figure.Position = p;
            OnPropertyChanged();
         }
      }

      public double SelectedFigureW
      {
         get { return _selectedFigureW; }
         set
         {
            if (value > 0)
            {
               _selectedFigureW = value;
               Vector2d v = new Vector2d(_selectedFigureW, SelectedFigure.Figure.Size.Y);
               SelectedFigure.Figure.Size = v;
               OnPropertyChanged();
            }
         }
      }

      public double SelectedFigureH
      {
         get { return _selectedFigureH; }
         set
         {
            if (value > 0)
            {
               _selectedFigureH = value;
               Vector2d v = new Vector2d(SelectedFigure.Figure.Size.X, _selectedFigureH);
               SelectedFigure.Figure.Size = v;
               OnPropertyChanged();
            }
         }
      }

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
      public IDrawableObject? SelectedFigure
      {
         get
         {
            if (_vm == null)
               return null;
            if (_vm.SelectedFigures == null)
               return null;
            var selected = _vm.SelectedFigures.Count() == 0;
            if (selected)
            {
               ParamVisibility = Visibility.Hidden;
               return null;
            }
            else
               ParamVisibility = Visibility.Visible;
            IDrawableObject? b = null;
            _vm.GetFigureByID.Execute(_vm.SelectedFigures.Last()).Subscribe(a => b = a);
            return b;
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
      enum MoveState
      {
         CANVAS_MOVE,
         FIGURE_MOVE,
         DRAW_FIGURE,
         CREATING_FIGURE,
         RESIZING_FIGURE,
         SELECT,
         MULTIPLE_SELECT
      }
      MoveState state = MoveState.SELECT;

      DispatcherTimer _timer;
      public MainWindow()
      {
         InitializeComponent();
         _vm = new MainVM();
         _vm.CreateFigure.Subscribe(AddFigure);
         _graphics = new Graphics.Graphic(canvas);
         DataContext = _vm;
         canvasTransStartPoint = new Point();
         newFigureSize = new Vector2d(0.5, 0.5);
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
         _vm.Draw.Execute(_graphics).Subscribe();
      }
      private void canvas_MouseMove(object sender, MouseEventArgs e)
      {
         var point = e.GetPosition(canvas);
         PreviousPoint = new Point(point.X, point.Y);
         if (Keyboard.IsKeyDown(Key.LeftShift) && state == MoveState.CANVAS_MOVE)
         {
            mouseMovePoint = e.GetPosition(this);
            var sub = Point.Subtract(mouseMovePoint, mouseDownPoint);
            canvasTranslate.X = canvasTransStartPoint.X + sub.X;
            canvasTranslate.Y = canvasTransStartPoint.Y + sub.Y;
         }
         else if (state == MoveState.FIGURE_MOVE && _vm.SelectedFigures.Count() > 0)
         {
            foreach (var selectedFigureId in _vm.SelectedFigures)
            {
               IDrawableObject? selectedFigure = null;
               _vm.GetFigureByID.Execute(selectedFigureId).Subscribe(a => selectedFigure = a);
               var sub = new Point2d(selectedFiguresStartMovePosition[selectedFigureId].X + e.GetPosition(canvas).X - mouseDownPoint.X, selectedFiguresStartMovePosition[selectedFigureId].Y + e.GetPosition(canvas).Y - mouseDownPoint.Y);
               selectedFigure.Figure.Position = sub;
               updateSelectedFigurePosition();
               OnPropertyChanged("SelectedFigure");
               //bound.setProperties(SelectedFigure.Figure.Position, SelectedFigure.Figure.Size);
            }
            Cursor = Cursors.SizeAll;
         }
         else if (state == MoveState.RESIZING_FIGURE && SelectedFigure != null)
         {
            //if (Math.Abs(SelectedFigure.Figure.Position.X - point.X) > 1.0 || Math.Abs(SelectedFigure.Figure.Position.Y - point.Y) > 1.0
            _vm.FigureBound.Resize(new Vector2d(point.X - tmpPoint.X, tmpPoint.Y - point.Y), direction);
            changeCursor();
            updateSelectedFigurePosition();
            updateSelectedFigureSize();
            tmpPoint = point;
         }
         else if (state == MoveState.CREATING_FIGURE)
         {
            newFigureSize.X = point.X - mouseDownPoint.X;
            newFigureSize.Y = point.Y - mouseDownPoint.Y;
            if (newFigureSize.Y < 0 && newFigureSize.X < 0)
            {
               var p = new Point2d();
               p.X = mouseDownPoint.X + newFigureSize.X;
               p.Y = mouseDownPoint.Y + newFigureSize.Y;
               newFigureSize.Y = -newFigureSize.Y;
               newFigureSize.X = -newFigureSize.X;
               SelectedFigure.Figure.Size = newFigureSize;
               SelectedFigure.Figure.Position = p;
               updateSelectedFigurePosition();
               updateSelectedFigureSize();


            }
            else if (newFigureSize.Y < 0)
            {
               var p = new Point2d();
               p.X = mouseDownPoint.X;
               p.Y = mouseDownPoint.Y + newFigureSize.Y;
               newFigureSize.Y = -newFigureSize.Y;
               SelectedFigure.Figure.Size = newFigureSize;
               SelectedFigure.Figure.Position = p;
               updateSelectedFigurePosition();
               updateSelectedFigureSize();
            }
            else if (newFigureSize.X < 0)
            {
               var p = new Point2d();
               p.Y = mouseDownPoint.Y;
               p.X = mouseDownPoint.X + newFigureSize.X;
               newFigureSize.X = -newFigureSize.X;
               SelectedFigure.Figure.Size = newFigureSize;
               SelectedFigure.Figure.Position = p;
               updateSelectedFigurePosition();
               updateSelectedFigureSize();
            }
            else
            {
               SelectedFigure.Figure.Size = newFigureSize;
               updateSelectedFigureSize();
            }
            //bound.setProperties(SelectedFigure.Figure.Position, SelectedFigure.Figure.Size);
            newFigureSize.X = 0.5;
            newFigureSize.Y = 0.5;
            OnPropertyChanged("SelectedFigure");
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
            figure.Angle = 0.0;
            figure.Size = newFigureSize;
            figure.Position = new DataStructures.Geometry.Point2d(MouseDownPoint.X, MouseDownPoint.Y);
            _vm.AddFigure.Execute(new DrawableObject(figure, new Drawable(new DataStructures.Color(main1.SelectedColor.A, main1.SelectedColor.R, main1.SelectedColor.G, main1.SelectedColor.B),
                new DataStructures.Color(main2.SelectedColor.A, main2.SelectedColor.R, main2.SelectedColor.G, main2.SelectedColor.B)))).Subscribe();
            updateSelectedFigurePosition();
            updateSelectedFigureSize();
            //bound.setProperties(SelectedFigure.Figure.Position, SelectedFigure.Figure.Size);
            OnPropertyChanged("SelectedFigure");
         }
      }
      private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
      {
         var point = e.GetPosition(canvas);
         MouseDownPoint = new Point(point.X, point.Y);
         var button = commands.Items.OfType<RadioButton>().Where(x => x.IsChecked == true).FirstOrDefault();
         if (button != null)
         {
            if (button.Name == select.Name)
            {
               mouseDownPoint = e.GetPosition(canvas);
               if (SelectedFigure != null)
               {
                  startFigureSize = SelectedFigure.Figure.Size;
                  startFigurePosition = SelectedFigure.Figure.Position;
                  // ��������� �� �������
                  if (_vm.FigureBound.OnBound(new Point2d(mouseDownPoint.X, mouseDownPoint.Y), 10f, out direction))
                  {
                     state = MoveState.RESIZING_FIGURE;
                     tmpPoint = point;
                  }
               }
               if (state != MoveState.RESIZING_FIGURE)
               {
                  if (Keyboard.IsKeyDown(Key.LeftCtrl))
                  {
                     state = MoveState.MULTIPLE_SELECT;
                  }
                  else
                  {
                     _vm.SelectFigure.Execute(new DataStructures.Geometry.Point2d(point.X, point.Y)).Subscribe();
                     _ = SelectedFigure;
                     if (SelectedFigure != null)
                     {
                        selectedFiguresStartMovePosition.Clear();
                        foreach (var selectedFigureId in _vm.SelectedFigures)
                        {
                           IDrawableObject? selectedFigure = null;
                           _vm.GetFigureByID.Execute(selectedFigureId).Subscribe(a => selectedFigure = a);
                           selectedFiguresStartMovePosition.Add(selectedFigureId, new Point2d(selectedFigure.Figure.Position.X, selectedFigure.Figure.Position.Y));
                        }
                        state = MoveState.FIGURE_MOVE;
                        updateSelectedFigurePosition();
                        SelectedFigureAngle = SelectedFigure.Figure.Angle * 180.0 / Math.PI;
                     }
                     OnPropertyChanged("SelectedFigure");
                  }
               }

               if (SelectedFigure != null)
               {
                  changeFillColor();
                  changeOutlineColor();
                  startFigureMovePosition = SelectedFigure.Figure.Position;
               }
            }
            var param = button.CommandParameter;
            if (param != null)
            {
               _vm.CreateFigure.Execute(param.ToString()).Subscribe();
               mouseDownPoint = e.GetPosition(canvas);
               state = MoveState.CREATING_FIGURE;
            }
         }
         if (Keyboard.IsKeyDown(Key.LeftShift))
         {
            mouseDownPoint = e.GetPosition(this);
            canvasTransStartPoint.X = canvasTranslate.X;
            canvasTransStartPoint.Y = canvasTranslate.Y;
            state = MoveState.CANVAS_MOVE;
         }
      }

      public void updateSelectedFigureSize()
      {
         SelectedFigureW = SelectedFigure.Figure.Size.X;
         SelectedFigureH = SelectedFigure.Figure.Size.Y;
         OnPropertyChanged();
      }

      public void updateSelectedFigurePosition()
      {
         SelectedFigureX = SelectedFigure.Figure.Position.X;
         SelectedFigureY = SelectedFigure.Figure.Position.Y;
         OnPropertyChanged();
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
         if (state == MoveState.MULTIPLE_SELECT)
         {
            var point = e.GetPosition(canvas);
            _vm.SelectFigures.Execute(new DataStructures.Geometry.Rect(new Point2d(mouseDownPoint.X, mouseDownPoint.Y), new Point2d(point.X, point.Y))).Subscribe();
            //_ = SelectedFigure;
            OnPropertyChanged("SelectedFigure");
            selectedFiguresStartMovePosition.Clear();
            foreach (var selectedFigureId in _vm.SelectedFigures)
            {
               IDrawableObject? selectedFigure = null;
               _vm.GetFigureByID.Execute(selectedFigureId).Subscribe(a => selectedFigure = a);
               selectedFiguresStartMovePosition.Add(selectedFigureId, new Point2d(selectedFigure.Figure.Position.X, selectedFigure.Figure.Position.Y));
            }
         }
         else
         {
            state = MoveState.SELECT;
            //bound.setNone();
            Cursor = Cursors.Arrow;
            OnPropertyChanged("SelectedFigure");
            //_vm.SelectFigure.Execute(new DataStructures.Geometry.Point2d(e.GetPosition(canvas).X, e.GetPosition(canvas).Y)).Subscribe();
         }
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

      private void main1_ColorChanged(object sender, RoutedEventArgs e)
      {
         var picker = sender as ColorPicker.StandardColorPicker;
         if (SelectedFigure != null)
         {
            SelectedFigure.Drawable.FillColor = new DataStructures.Color(picker.SelectedColor.A, picker.SelectedColor.R, picker.SelectedColor.G, picker.SelectedColor.B);
            //changeFillColor();
         }
      }

      private void main2_ColorChanged(object sender, RoutedEventArgs e)
      {
         var picker = sender as ColorPicker.StandardColorPicker;
         if (SelectedFigure != null)
         {
            SelectedFigure.Drawable.OutLineColor = new DataStructures.Color(picker.SelectedColor.A, picker.SelectedColor.R, picker.SelectedColor.G, picker.SelectedColor.B);
            //changeOutlineColor();
         }
      }

      private void changeFillColor()
      {
         Color clr = new Color()
         {
            A = SelectedFigure.Drawable.FillColor.A,
            R = SelectedFigure.Drawable.FillColor.R,
            G = SelectedFigure.Drawable.FillColor.G,
            B = SelectedFigure.Drawable.FillColor.B
         };
         main1.SelectedColor = clr;
      }

      private void changeOutlineColor()
      {
         Color clr = new Color()
         {
            A = SelectedFigure.Drawable.OutLineColor.A,
            R = SelectedFigure.Drawable.OutLineColor.R,
            G = SelectedFigure.Drawable.OutLineColor.G,
            B = SelectedFigure.Drawable.OutLineColor.B
         };
         main2.SelectedColor = clr;
      }

      private void mainWindow_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.X && SelectedFigure != null)
            _vm.RemoveFigure.Execute(_vm.SelectedFigures.Last()).Subscribe();
      }

      private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
      {
         if (SelectedFigure != null)
         {
            ContextMenu context = new ContextMenu();
            canvas.ContextMenu = context;
            IEnumerable<(string, ReactiveCommand<Point2d, bool>)> parameters = null;
            _vm.GetContextCommands.Execute().Subscribe(a => parameters = a);
            context.Items.Clear();
            foreach (var p in parameters)
            {
               MenuItem mi = new MenuItem();
               mi.Header = p.Item1;
               mi.Click += MenuItem_Click;
               context.Items.Add(mi);
            }
         }
      }

      private void MenuItem_Click(object sender, RoutedEventArgs e)
      {
         if (SelectedFigure != null)
         {
            var mi = sender as MenuItem;
            IEnumerable<(string, ReactiveCommand<Point2d, bool>)> parameters = null;
            _vm.GetContextCommands.Execute().Subscribe(a => parameters = a);
            foreach (var p in parameters)
            {
               if (mi.Header.ToString() == p.Item1)
               {
                  p.Item2.Execute().Subscribe();
                  break;
               }
            }
            canvas.ContextMenu = null;
         }
      }

      private void changeCursor()
      {
         if (direction == FigureChangeDirection.Right || direction == FigureChangeDirection.Left)
            Cursor = Cursors.SizeWE;
         else if (direction == FigureChangeDirection.Top || direction == FigureChangeDirection.Bottom)
            Cursor = Cursors.SizeNS;
               
      }

      private void canvas_MouseLeave(object sender, MouseEventArgs e)
      {
         state = MoveState.SELECT;
         this.Cursor = Cursors.Arrow;
      }

        private void Create_Button(object sender, RoutedEventArgs e)
        {
            _vm.Clear.Execute().Subscribe();
            OnPropertyChanged("SelectedFigure");
        }
        private void Open_Button(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.json; *.svg)|*.json;*.svg";
            if (openFileDialog.ShowDialog() == true)
                _vm.Load.Execute(openFileDialog.OpenFile()).Subscribe();
            OnPropertyChanged("SelectedFigure");
        }
        private void Save_Button(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TIFF(*.tiff)|*.tiff|PNG(*.png)|*.png|BMP(*.bmp)|*.bmp|JPEG(*.jpeg)|*.jpeg|GIF(*.gif)|*.gif|JSON(*.json)|*.json|SVG(*.svg)|*.svg";
            saveFileDialog.FilterIndex = 7;
            if (saveFileDialog.ShowDialog() == true)
                _vm.Save.Execute(saveFileDialog.OpenFile()).Subscribe();
        }
    }
}
