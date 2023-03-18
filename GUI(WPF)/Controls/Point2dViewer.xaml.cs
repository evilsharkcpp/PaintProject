using DataStructures.Geometry;
using System.ComponentModel;
using System.Windows.Controls;

namespace GUI_WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для Point2dViewer.xaml
    /// </summary>
    public partial class Point2dViewer : UserControl
    {
        private Point2d _value;
        private string _firstFieldName;
        private string _secondFieldName;

        [Category("Data")]
        public string FirstFieldName
        {
            get => _firstFieldName;
            set
            {
                _firstFieldName = value;
                FirstFieldLabel.Content = _firstFieldName;
            }
        }

        [Category("Data")]
        public string SecondFieldName
        {
            get => _secondFieldName;
            set
            {
                _secondFieldName = value;
                SecondFieldLabel.Content = _secondFieldName;
            }
        }

        [Category("Data")]
        public Point2d Value
        {
            get => _value;
            set
            {
                _value = value;
                FirstFieldTextBox.Text = _value.X.ToString();
                SecondFieldTextBox.Text = _value.Y.ToString();
            }
        }

        public Point2dViewer()
        {
            InitializeComponent();
            FirstFieldName = "X:";
            SecondFieldName = "Y:";
            Value = new Point2d(0, 0);
        }
    }
}
