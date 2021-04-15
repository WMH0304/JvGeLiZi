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
    /// StyleTest.xaml 的交互逻辑
    /// </summary>
    public partial class StyleTest : Window
    {
        public StyleTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 鼠标移入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameworkElement_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Foreground = new SolidColorBrush(Colors.Red);

        }

        private void FrameworkElement_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Foreground = new SolidColorBrush(Colors.Green);
        }
    }
}
