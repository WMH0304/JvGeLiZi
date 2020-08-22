using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace QQMessageSend
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private Boolean isUsr = true;
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        private void Form2_Load(object sender, EventArgs e)
        {
            //null
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("打开聊天的窗口，一般是聊天对象的备注名（窗口上应该有显示）");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {

                    Form1.Form2text = textBox1.Text;
                    /*
                    FileStream fileStream = new FileStream("log.txt", FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(textBox1.Text);
                    streamWriter.Close();
                    this.Close();
                    */
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //TODO
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUsr)
            {
                Environment.Exit(0);
            }
        }
    }
}
