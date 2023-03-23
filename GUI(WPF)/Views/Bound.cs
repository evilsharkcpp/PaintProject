using DataStructures.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI_WPF.Views
{
    class Bound
    {
        MainWindow mainWindow;
        Point2d boundPos;
        Vector2d boundSize;
        double eps = 5.0;
        double minSize = 2.0;
        enum ChangeFigureSize
        {
            None,
            UpLeftresizeClick,
            DownLeftresizeClick,
            UpRightresizeClick,
            DownRightresizeClick,
            LeftSide,
            RightSide,
            UpSide,
            DownSide,
            Rotate
        }
        ChangeFigureSize changeFigureSize = ChangeFigureSize.None;
        public Bound(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
            boundPos = new Point2d();
            boundSize = new Vector2d();
        }

        public void setProperties(Point2d pos, Vector2d size)
        {
            boundPos = pos;
            boundSize = size;
        }

        public bool isInBound(Point click)
        {

            if (Math.Abs(click.X - (boundPos.X - eps)) <= eps && Math.Abs(click.Y - (boundPos.Y - eps)) <= minSize)
            {
                changeFigureSize = ChangeFigureSize.UpLeftresizeClick;
                mainWindow.Cursor = Cursors.SizeNWSE;
            }
            else if (Math.Abs(click.X - (boundPos.X - eps)) <= eps && Math.Abs(click.Y - (boundPos.Y + boundSize.Y + eps)) <= eps)
            {
                changeFigureSize = ChangeFigureSize.DownLeftresizeClick;
                mainWindow.Cursor = Cursors.SizeNESW;
            }
            else if (Math.Abs(click.X - (boundPos.X + boundSize.X + eps)) <= eps && Math.Abs(click.Y - (boundPos.Y - eps)) <= eps)
            {
                changeFigureSize = ChangeFigureSize.UpRightresizeClick;
                mainWindow.Cursor = Cursors.SizeNESW;
            }
            else if (Math.Abs(click.X - (boundPos.X + boundSize.X + eps)) <= eps && Math.Abs(click.Y - (boundPos.Y + boundSize.Y + eps)) <= eps)
            {
                changeFigureSize = ChangeFigureSize.DownRightresizeClick;
                mainWindow.Cursor = Cursors.SizeNWSE;
            }
            else if (Math.Abs(click.X - (boundPos.X - eps)) <= eps && click.Y > (boundPos.Y - eps) && click.Y < boundPos.Y + boundSize.Y + eps)
            {
                changeFigureSize = ChangeFigureSize.LeftSide;
                mainWindow.Cursor = Cursors.SizeWE;
            }
            else if (Math.Abs(click.X - (boundPos.X + boundSize.X + eps)) <= eps && click.Y > (boundPos.Y - eps) && click.Y < boundPos.Y + boundSize.Y + eps)
            {
                changeFigureSize = ChangeFigureSize.RightSide;
                mainWindow.Cursor = Cursors.SizeWE;
            }
            else if (Math.Abs(click.Y - (boundPos.Y - eps)) <= eps && click.X > boundPos.X - eps && click.X < boundPos.X + boundSize.X + eps)
            {
                changeFigureSize = ChangeFigureSize.UpSide;
                mainWindow.Cursor = Cursors.SizeNS;
            }
            else if (Math.Abs(click.Y - (boundPos.Y + boundSize.Y + eps)) <= eps && click.X > boundPos.X - eps && click.X < boundPos.X + boundSize.X + eps)
            {
                changeFigureSize = ChangeFigureSize.DownSide;
                mainWindow.Cursor = Cursors.SizeNS;
            }

            if (changeFigureSize == ChangeFigureSize.None)
                return false;
            else 
                return true;
        }

        public (Point2d, Vector2d) resize(Point click, Point resizeClick, Vector2d startFigureSize, Point2d startFigurePosition)
        {
            Vector2d resSize = startFigureSize;
            Point2d resPos = startFigurePosition;
            switch (changeFigureSize)
            {
                case ChangeFigureSize.UpLeftresizeClick:
                    if (startFigureSize.X + (click.X - resizeClick.X) >= 5 && startFigureSize.Y + (click.Y - resizeClick.Y) >= 5)
                    {
                        resPos = new Point2d(startFigurePosition.X + resizeClick.X - click.X, startFigurePosition.Y + resizeClick.Y - click.Y);
                        resSize = new Vector2d(startFigureSize.X + (click.X - resizeClick.X), startFigureSize.Y + (click.Y - resizeClick.Y));
                        
                    }
                    break;
                case ChangeFigureSize.DownLeftresizeClick:
                    if (startFigureSize.X + (click.X - resizeClick.X) >= 5 && startFigureSize.Y - (click.Y - resizeClick.Y) >= 5)
                    {
                        resPos = new Point2d(startFigurePosition.X + resizeClick.X - click.X, startFigurePosition.Y);
                        resSize = new Vector2d(startFigureSize.X + (click.X - resizeClick.X), startFigureSize.Y - (click.Y - resizeClick.Y));
                        
                    }
                    break;
                case ChangeFigureSize.UpRightresizeClick:
                    if (startFigureSize.X - (click.X - resizeClick.X) >= 5 && startFigureSize.Y + (click.Y - resizeClick.Y) >= 5)
                    {
                        resPos = new Point2d(startFigurePosition.X, startFigurePosition.Y + resizeClick.Y - click.Y);
                        resSize = new Vector2d(startFigureSize.X - (click.X - resizeClick.X), startFigureSize.Y + (click.Y - resizeClick.Y));
                        
                    }
                    break;
                case ChangeFigureSize.DownRightresizeClick:
                    if (startFigureSize.X - (click.X - resizeClick.X) >= 5 && startFigureSize.Y - (click.Y - resizeClick.Y) >= 5)
                    {
                        resSize = new Vector2d(startFigureSize.X - (click.X - resizeClick.X), startFigureSize.Y - (click.Y - resizeClick.Y));
                    }

                    break;
                case ChangeFigureSize.LeftSide:
                    if (startFigureSize.X + (click.X - resizeClick.X) >= 5)
                    {
                        resPos = new Point2d(startFigurePosition.X + resizeClick.X - click.X, startFigurePosition.Y);
                        resSize = new Vector2d(startFigureSize.X + (click.X - resizeClick.X), startFigureSize.Y);
                        
                    }
                    break;
                case ChangeFigureSize.RightSide:
                    if (startFigureSize.X - (click.X - resizeClick.X) >= 5)
                    {
                        resSize = new Vector2d(startFigureSize.X - (click.X - resizeClick.X), startFigureSize.Y);
                    }
                    break;
                case ChangeFigureSize.UpSide:
                    if (startFigureSize.Y + (click.Y - resizeClick.Y) >= 5)
                    {
                        resPos = new Point2d(startFigurePosition.X, startFigurePosition.Y + resizeClick.Y - click.Y);
                        resSize = new Vector2d(startFigureSize.X, startFigureSize.Y + (click.Y - resizeClick.Y));
                        
                    }
                    break;
                case ChangeFigureSize.DownSide:
                    if (startFigureSize.Y - (click.Y - resizeClick.Y) >= 5)
                    {
                        resSize = new Vector2d(startFigureSize.X, startFigureSize.Y - (click.Y - resizeClick.Y));
                        
                    }
                    break;
            }
            return (resPos, resSize);
        }

        public void setNone()
        {
            changeFigureSize = ChangeFigureSize.None;
        }
    }
}
