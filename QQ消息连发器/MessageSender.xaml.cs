using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace QQ消息连发器
{
    /// <summary>
    /// MessageSender.xaml 的交互逻辑
    /// </summary>
    public partial class MessageSender : Window
    {
        public static string formName = "";
       
        public string Win_Name;
        public MessageSender(string WinName)
        {
            Win_Name = WinName;
            InitializeComponent();
        }
        //DllImport 使用包含要导入的方法的 DLL 的名称初始化 DllImportAttribute 类的新实例。
        //EntryPoint 指示要调用的 DLL 入口点的名称或序号。
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);//用来获取窗口句柄
        public static string Form2text = "";
        IntPtr checkH;//用来存放选定窗口的句柄
        [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]

        //隐藏或显示目标窗口句柄
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Tbcon.Text = "Hello World!";
            TbSenderNum.Text = "1000";
            TbInterval.Text = "10";

            try
            {
                // 获取目标窗口句柄
                IntPtr ParenthWnd;
                ParenthWnd = FindWindow(null, Win_Name);
                // Zero 表示已初始化为零的指针或句柄的只读字段。
                if (ParenthWnd != IntPtr.Zero)
                {
                    MessageBox.Show("窗口:" + Win_Name +
                                  "\n句柄:" + ParenthWnd.ToInt64().ToString() +
                                  "\n找到窗口，点击确定开始下一步", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    checkH = ParenthWnd;
                }
                else
                {
                    MessageBox.Show("没有找到窗口，是不是输错字了？");
                    Environment.Exit(0);
                }
               

            }
            catch (Exception es)
            {

                new StreamWriter(new FileStream("Wrong.log", FileMode.OpenOrCreate, FileAccess.Write)).Write(es.ToString());
            }
        }
        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bthelp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 重寻窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtNew_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtSender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tbcon.Text =="")
                {
                    MessageBox.Show("你发个寂寞");
                }
                else
                {
                    if (TbSenderNum.Text =="")
                    {
                        MessageBox.Show("你这是要发几次");
                    }
                    else if (TbInterval.Text =="")
                    {
                        MessageBox.Show("发送时间间隔");
                    }
                    else
                    {
                        ShowWindow(checkH, 0);//控制窗口显示和隐藏，0隐藏，1显示
                        ShowWindow(checkH, 1);
                        for (int i = 0; i < Convert.ToInt32(TbSenderNum.Text); i++)
                        {
                            //+ " 发送次数： " +i
                            SendKeys.SendWait(Tbcon.Text);//发送
                            SendKeys.SendWait("{ENTER}");//回车
                            System.Threading.Thread.Sleep(Convert.ToInt32(TbInterval.Text));
                        }
                    }
                    
                }
            }
            catch (Exception ee)
            {

                Debug.Write(ee.ToString());
            }
        }
    }
}
