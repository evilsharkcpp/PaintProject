using Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
   public partial class MainWindow : Window
   {
      MainVM vm = new MainVM();
      const double scaleRate = 1.1;
      bool canvasTranslateState = false;
      Point canvasStartP, startTranslate;
      public MainWindow()
      {
         InitializeComponent();
         Trace.WriteLine("init");
         DataContext = vm;
         vm.CreateFigure.Subscribe();

         startTranslate = new Point();
      }

      private void scaleUp()
      {
         testText.Text = "scale up";
         canvasST.ScaleX *= scaleRate;
         canvasST.ScaleY *= scaleRate;
      }

      private void scaleDown()
      {
         testText.Text = "scale down";
         canvasST.ScaleX /= scaleRate;
         canvasST.ScaleY /= scaleRate;         
      }

      private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         if (e.Delta > 0) 
            scaleUp();
         else 
            scaleDown();
      }

      private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         Trace.WriteLine("in mouse down");
         canvasStartP = e.GetPosition(this);
         startTranslate.X = canvasTranslate.X;
         startTranslate.Y = canvasTranslate.Y;
         canvasTranslateState = true;
      }

      private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         canvasTranslateState = false;
      }

      private void Canvas_MouseMove(object sender, MouseEventArgs e)
      {
         if (canvasTranslateState)
         {
            Point curPos;
            curPos.X = e.GetPosition(this).X;
            curPos.Y = e.GetPosition(this).Y;
            var sub = Point.Subtract(curPos, canvasStartP);
            Trace.WriteLine("x " + sub.X + ", y " + sub.Y);
            Trace.WriteLine("x tr " + canvasTranslate.X + ", y tr " + canvasTranslate.Y);
            canvasTranslate.X = startTranslate.X + sub.X;
            canvasTranslate.Y = startTranslate.Y + sub.Y;
         }
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
