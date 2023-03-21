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
using Drawing.Graphics;
using System.Security.Cryptography.X509Certificates;
using DataStructures.Geometry;
using System.Windows.Media;
using Microsoft.Win32;

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
        Vector2d startFigureSize;
        Vector2d newFigureSize;
        Point2d startFigurePosition;
        private Point2d startFigureMovePosition;
        double _canvasScale = 1.0;
        double _selectedFigureX, _selectedFigureY, _selectedFigureW, _selectedFigureH, _selectedFigureAngle;
        string _scaleString = "100%";
        enum ChangeFigureSize
        {
            None,
            UpLeftPoint,
            DownLeftPoint,
            UpRightPoint,
            DownRightPoint,
            LeftSide,
            RightSide,
            UpSide,
            DownSide,
            Rotate
        }
        ChangeFigureSize changeFigureSize = ChangeFigureSize.None;

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
            SELECT
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
            else if (state == MoveState.FIGURE_MOVE && SelectedFigure != null)
            {
                var sub = new Point2d(startFigureMovePosition.X + e.GetPosition(canvas).X - mouseDownPoint.X, startFigureMovePosition.Y + e.GetPosition(canvas).Y - mouseDownPoint.Y);
                SelectedFigure.Figure.Position = sub;
                updateSelectedFigurePosition();
                OnPropertyChanged("SelectedFigure");
                this.Cursor = Cursors.SizeAll;
            }
            else if (changeFigureSize != ChangeFigureSize.None && SelectedFigure != null)
            {
                switch (changeFigureSize)
                {
                    case ChangeFigureSize.UpLeftPoint:
                        if (startFigureSize.X + (mouseDownPoint.X - point.X) >= 5 && startFigureSize.Y + (mouseDownPoint.Y - point.Y) >= 5)
                        {
                            SelectedFigure.Figure.Position = new Point2d(startFigurePosition.X + e.GetPosition(canvas).X - mouseDownPoint.X, startFigurePosition.Y + e.GetPosition(canvas).Y - mouseDownPoint.Y);
                            SelectedFigure.Figure.Size = new Vector2d(startFigureSize.X + (mouseDownPoint.X - point.X), startFigureSize.Y + (mouseDownPoint.Y - point.Y));
                            updateSelectedFigurePosition();
                            updateSelectedFigureSize();
                        }
                        break;
                    case ChangeFigureSize.DownLeftPoint:
                        if (startFigureSize.X + (mouseDownPoint.X - point.X) >= 5 && startFigureSize.Y - (mouseDownPoint.Y - point.Y) >= 5)
                        {
                            SelectedFigure.Figure.Position = new Point2d(startFigurePosition.X + e.GetPosition(canvas).X - mouseDownPoint.X, startFigurePosition.Y);
                            SelectedFigure.Figure.Size = new Vector2d(startFigureSize.X + (mouseDownPoint.X - point.X), startFigureSize.Y - (mouseDownPoint.Y - point.Y));
                            updateSelectedFigurePosition();
                            updateSelectedFigureSize();
                        }
                        break;
                    case ChangeFigureSize.UpRightPoint:
                        if (startFigureSize.X - (mouseDownPoint.X - point.X) >= 5 && startFigureSize.Y + (mouseDownPoint.Y - point.Y) >= 5)
                        {
                            SelectedFigure.Figure.Position = new Point2d(startFigurePosition.X, startFigurePosition.Y + e.GetPosition(canvas).Y - mouseDownPoint.Y);
                            SelectedFigure.Figure.Size = new Vector2d(startFigureSize.X - (mouseDownPoint.X - point.X), startFigureSize.Y + (mouseDownPoint.Y - point.Y));
                            updateSelectedFigurePosition();
                            updateSelectedFigureSize();
                        }
                        break;
                    case ChangeFigureSize.DownRightPoint:
                        if (startFigureSize.X - (mouseDownPoint.X - point.X) >= 5 && startFigureSize.Y - (mouseDownPoint.Y - point.Y) >= 5)
                        {
                            SelectedFigure.Figure.Size = new Vector2d(startFigureSize.X - (mouseDownPoint.X - point.X), startFigureSize.Y - (mouseDownPoint.Y - point.Y));
                            updateSelectedFigureSize();
                        }

                        break;
                    case ChangeFigureSize.LeftSide:
                        if (startFigureSize.X + (mouseDownPoint.X - point.X) >= 5)
                        {
                            SelectedFigure.Figure.Position = new Point2d(startFigurePosition.X + e.GetPosition(canvas).X - mouseDownPoint.X, startFigurePosition.Y);
                            SelectedFigure.Figure.Size = new Vector2d(startFigureSize.X + (mouseDownPoint.X - point.X), SelectedFigure.Figure.Size.Y);
                            updateSelectedFigurePosition();
                            updateSelectedFigureSize();
                        }
                        break;
                    case ChangeFigureSize.RightSide:
                        if (startFigureSize.X - (mouseDownPoint.X - point.X) >= 5)
                        {
                            SelectedFigure.Figure.Size = new Vector2d(startFigureSize.X - (mouseDownPoint.X - point.X), SelectedFigure.Figure.Size.Y);
                            updateSelectedFigureSize();
                        }
                        break;
                    case ChangeFigureSize.UpSide:
                        if (startFigureSize.Y + (mouseDownPoint.Y - point.Y) >= 5)
                        {
                            SelectedFigure.Figure.Position = new Point2d(startFigurePosition.X, startFigurePosition.Y + e.GetPosition(canvas).Y - mouseDownPoint.Y);
                            SelectedFigure.Figure.Size = new Vector2d(SelectedFigure.Figure.Size.X, startFigureSize.Y + (mouseDownPoint.Y - point.Y));
                            updateSelectedFigurePosition();
                            updateSelectedFigureSize();
                        }
                        break;
                    case ChangeFigureSize.DownSide:
                        if (startFigureSize.Y - (mouseDownPoint.Y - point.Y) >= 5)
                        {
                            SelectedFigure.Figure.Size = new Vector2d(SelectedFigure.Figure.Size.X, startFigureSize.Y - (mouseDownPoint.Y - point.Y));
                            updateSelectedFigureSize();
                        }
                        break;
                }
                OnPropertyChanged("SelectedFigure");
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
                        if (Math.Abs(mouseDownPoint.X - (SelectedFigure.Figure.Position.X - 5)) <= 5 && Math.Abs(mouseDownPoint.Y - (SelectedFigure.Figure.Position.Y - 5)) <= 2)
                        {
                            changeFigureSize = ChangeFigureSize.UpLeftPoint;
                            this.Cursor = Cursors.SizeNWSE;
                        }
                        else if (Math.Abs(mouseDownPoint.X - (SelectedFigure.Figure.Position.X - 5)) <= 5 && Math.Abs(mouseDownPoint.Y - (SelectedFigure.Figure.Position.Y + SelectedFigure.Figure.Size.Y + 5)) <= 5)
                        {
                            changeFigureSize = ChangeFigureSize.DownLeftPoint;
                            this.Cursor = Cursors.SizeNESW;
                        }
                        else if (Math.Abs(mouseDownPoint.X - (SelectedFigure.Figure.Position.X + SelectedFigure.Figure.Size.X + 5)) <= 5 && Math.Abs(mouseDownPoint.Y - (SelectedFigure.Figure.Position.Y - 5)) <= 5)
                        {
                            changeFigureSize = ChangeFigureSize.UpRightPoint;
                            this.Cursor = Cursors.SizeNESW;
                        }
                        else if (Math.Abs(mouseDownPoint.X - (SelectedFigure.Figure.Position.X + SelectedFigure.Figure.Size.X + 5)) <= 5 && Math.Abs(mouseDownPoint.Y - (SelectedFigure.Figure.Position.Y + SelectedFigure.Figure.Size.Y + 5)) <= 5)
                        {
                            changeFigureSize = ChangeFigureSize.DownRightPoint;
                            this.Cursor = Cursors.SizeNWSE;
                        }
                        else if (Math.Abs(mouseDownPoint.X - (SelectedFigure.Figure.Position.X - 5)) <= 5 && mouseDownPoint.Y > (SelectedFigure.Figure.Position.Y - 5) && mouseDownPoint.Y < SelectedFigure.Figure.Position.Y + SelectedFigure.Figure.Size.Y + 5)
                        {
                            changeFigureSize = ChangeFigureSize.LeftSide;
                            this.Cursor = Cursors.SizeWE;
                        }
                        else if (Math.Abs(mouseDownPoint.X - (SelectedFigure.Figure.Position.X + SelectedFigure.Figure.Size.X + 5)) <= 5 && mouseDownPoint.Y > (SelectedFigure.Figure.Position.Y - 5) && mouseDownPoint.Y < SelectedFigure.Figure.Position.Y + SelectedFigure.Figure.Size.Y + 5)
                        {
                            changeFigureSize = ChangeFigureSize.RightSide;
                            this.Cursor = Cursors.SizeWE;
                        }
                        else if (Math.Abs(mouseDownPoint.Y - (SelectedFigure.Figure.Position.Y - 5)) <= 5 && mouseDownPoint.X > SelectedFigure.Figure.Position.X - 5 && mouseDownPoint.X < SelectedFigure.Figure.Position.X + SelectedFigure.Figure.Size.X + 5)
                        {
                            changeFigureSize = ChangeFigureSize.UpSide;
                            this.Cursor = Cursors.SizeNS;
                        }
                        else if (Math.Abs(mouseDownPoint.Y - (SelectedFigure.Figure.Position.Y + SelectedFigure.Figure.Size.Y + 5)) <= 5 && mouseDownPoint.X > SelectedFigure.Figure.Position.X - 5 && mouseDownPoint.X < SelectedFigure.Figure.Position.X + SelectedFigure.Figure.Size.X + 5)
                        {
                            changeFigureSize = ChangeFigureSize.DownSide;
                            this.Cursor = Cursors.SizeNS;
                        }
                    }
                    if (changeFigureSize == ChangeFigureSize.None)
                    {
                        _vm.SelectFigure.Execute(new DataStructures.Geometry.Point2d(point.X, point.Y)).Subscribe();
                        _ = SelectedFigure;
                        if (SelectedFigure != null)
                        {
                            state = MoveState.FIGURE_MOVE;
                            updateSelectedFigurePosition();
                            SelectedFigureAngle = SelectedFigure.Figure.Angle * 180.0 / Math.PI;
                        }
                        OnPropertyChanged("SelectedFigure");
                    }

                    if (SelectedFigure != null)
                    {
                        //OnPropertyChanged("SelectedFigure");
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

        private void updateSelectedFigureSize()
        {
            SelectedFigureW = SelectedFigure.Figure.Size.X;
            SelectedFigureH = SelectedFigure.Figure.Size.Y;
            OnPropertyChanged();
        }

        private void updateSelectedFigurePosition()
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
            state = MoveState.SELECT;
            changeFigureSize = ChangeFigureSize.None;
            this.Cursor = Cursors.Arrow;
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

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            state = MoveState.SELECT;
            changeFigureSize = ChangeFigureSize.None;
            this.Cursor = Cursors.Arrow;
        }

        private void Create_Button(object sender, RoutedEventArgs e)
        {

        }
        private void Open_Button(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Разметка(*.json; *.svg)|*.json;*.svg";
            if (openFileDialog.ShowDialog() == true)
                _vm.Load.Execute(openFileDialog.OpenFile()).Subscribe();
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
