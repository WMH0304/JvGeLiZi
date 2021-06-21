using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace QQMessageSend
{
    public partial class Form1 : Form
    {
        public static string formName = "";
        private Boolean isFirst = true;
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);//用来获取窗口句柄
        public static string Form2text = "";
        IntPtr checkH;//用来存放选定窗口的句柄
        [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                /*
             哈哈哈哈哈哈我写的什么东西
             靠文件传值可还行哈哈哈哈哈哈哈哈哈哈哈（神经病
             （算了算了以后再改
             */


                //从form2那里拿到对话框的句柄
                Form form = new Form2();
                form.ShowDialog();
                /*
                FileStream fileStream = new FileStream("log.txt", FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                Form2text = streamReader.ReadLine();
                streamReader.Close();
                */
                form.Dispose();
                //判断form2



                IntPtr ParenthWnd;
                //获取目标窗口句柄
                ParenthWnd = FindWindow(null, Form2text);
                //判断这个窗体是否有效 
                if (ParenthWnd != IntPtr.Zero)
                {
                    MessageBox.Show("窗口:" + Form2text +
                                  "\n句柄:" + ParenthWnd.ToInt64().ToString() + 
                                  "\n找到窗口，点击确定开始下一步", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    checkH = ParenthWnd;
                    
                }

                else
                {
                    MessageBox.Show("没有找到窗口，是不是输错字了？");
                    Environment.Exit(0);
                }
                if (isFirst)
                {
                    MessageBox.Show("禁止使用此软件用作任何形式的违法行为，您必须严格遵守当地法律！" +
                        "\n开始前请将输入法切换成英文，开启回车发送消息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isFirst = false;
                }

            }
            catch (Exception es)
            {
                new StreamWriter(new FileStream("Wrong.log", FileMode.OpenOrCreate, FileAccess.Write)).Write(es.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //null
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("你发个寂寞");
                }
                else
                {
                    if (textBox2.Text == string.Empty)
                    {
                        MessageBox.Show("你这是要发几次");
                    }
                    else
                    {
                        ShowWindow(checkH, 0);//控制窗口显示和隐藏，0隐藏，1显示
                        ShowWindow(checkH, 1);
                        for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
                        {
                            SendKeys.Send(textBox1.Text + i);//发送
                            SendKeys.SendWait("{ENTER}");//回车
                            System.Threading.Thread.Sleep(Convert.ToInt32(textBox3.Text));
                        }
                    }
                }
            }
            catch (Exception es)
            {
                new StreamWriter(new FileStream("Wrong.log", FileMode.OpenOrCreate, FileAccess.Write)).Write(es.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者的联系邮箱:2519041062@qq.com\n禁止使用此软件进行违法活动，您必须严格遵守当地法律！" +
                "\n开始前请将输入法切换成英文，开启回车发送消息","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            Form2 form = new Form2();
            form.ShowDialog();
            FileStream fileStream = new FileStream("log.txt", FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            Form2text = streamReader.ReadLine();
            checkH = FindWindow(null, streamReader.ReadLine());
            streamReader.Close();
            */
            //MessageBox.Show("正在重写...给我点时间");
            Form1_Load(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
