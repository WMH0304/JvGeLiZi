using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace 征文要的傻逼表白程序
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }
        /// <summary>
        /// 同意
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 鼠标移入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            var yll = btn2.Margin.Left;
            var ylt = btn2.Margin.Top;

            Random random = new Random();
            var rt = random.Next(0, 350);
            var rl = random.Next(0, 500);
            btn2.Margin =new Thickness(rt,rl,rt,rl);
        }
    }
}
