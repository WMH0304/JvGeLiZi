using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFUI_test.Tools;

namespace WPFUI_test.Test
{
    /// <summary>
    /// VisualObject.xaml 的交互逻辑
    /// </summary>
    public partial class VisualObject : Window
    {
        public VisualObject()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();
            Brush brush = Brushes.Red;
            Pen pen = new Pen(Brushes.Green,3);

            dc.DrawRectangle(brush, pen, new Rect(new Point(100, 100), new Size(50, 100)));
            dc.DrawEllipse(brush, pen, new Point(200, 200),20,100);

            dc.DrawLine(pen, new Point(0, 0), new Point(50, 50));
            dc.DrawLine(pen, new Point(50, 50), new Point(20, 50));
            dc.DrawLine(pen, new Point(20, 50), new Point(0, 0));
            dc.Close();
          
            Mytest.AddVisual(dv);


        }

        /// <summary>
        /// 在窗口 上按下左键时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(Mytest);
            DrawingVisual visual = new DrawingVisual();
            DrawSquare(visual, point);
            Mytest.AddVisual(visual);

        }

        /// <summary>
        /// 生成图像
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="topLeft"></param>
        private void DrawSquare(DrawingVisual visual,Point topLeft)
        {
          
            DrawingContext dc = visual.RenderOpen();
            Brush brush = Brushes.Green;
            Pen pen = new Pen(brush,3);
            dc.DrawRectangle(brush, pen, new Rect(topLeft,new Size(40,40)));
            dc.Close();//关闭对象后才能完成
        }
        /// <summary>
        /// 删除图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(Mytest);
            DrawingVisual dv = Mytest.GetVisual(point);
            if (dv!=null)
            {
                Mytest.DeleteVisual(dv);
            }
        }
    }
}
