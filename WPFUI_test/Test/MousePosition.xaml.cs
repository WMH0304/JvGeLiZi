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

namespace WPFUI_test.Test
{
    /// <summary>
    /// MousePosition.xaml 的交互逻辑
    /// </summary>
    public partial class MousePosition : Window
    {
        public MousePosition()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(this);
            this.tb_lable.Text =$"当前坐标为{pt.X-10},{pt.Y-10}"  ;
        }
        /// <summary>
        /// 鼠标的隧道路由事件  点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rect_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// 鼠标的冒泡路由事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rect_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_cmd_Click(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(this.rect);
            this.bt_cmd.Content = "已捕获";//捕获后失去焦点 
        }

    }
}
