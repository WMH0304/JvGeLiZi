using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace QQ消息连发器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Boolean isUsr = true;
        //导入dll 并指向入口
        [DllImport("User32.dll", EntryPoint = "FindWindow")]


        /* 
         * extern 修饰符用于声明在外部实现的方法。 
         * FindWindow  dll 内的方法
         */
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bthelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("打开聊天的窗口，一般是聊天对象的备注名（窗口上应该有显示）");
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btquit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageSender messageSender = new MessageSender(TbWindows.Text.ToString());
                messageSender.Show();
                if (TbWindows.Text != "")
                {
                    if (isUsr)
                    {
                        isUsr = false;
                        this.Close();
                    }
                   
                }
                else
                {
                    MessageBox.Show("你输了个寂寞");
                }

            }
            catch (Exception es)
            {

                new StreamWriter(new FileStream("Wrong.log", FileMode.OpenOrCreate, FileAccess.Write)).Write(es.ToString());
            }
        }
        /// <summary>
        /// 窗口关闭时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (isUsr)
            {
                Environment.Exit(0);
            }
        }
    }
}
