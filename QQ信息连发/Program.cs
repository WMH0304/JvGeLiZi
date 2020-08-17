

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TimSendMessage
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(String ClassName, String WindwosName);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte vk, byte vsacn, int flag, int wram);

        [DllImport("user32.dll")]
        static extern void PostMessage(IntPtr hwnd, uint msg, int w, string l);
        [DllImport("user32.dll")]
        static extern void PostMessage(IntPtr hwnd, uint msg, int w, int l);

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("发送QQ的名字");
            var name = Console.ReadLine();
            Console.WriteLine("要发送的字符");
            var t = Console.ReadLine();
            Console.WriteLine("要发送的次数");
            var Count = int.Parse(Console.ReadLine());

            while (Count > -1)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
                Clipboard.SetText(t);
                SendKey(name, t);
                Count--;
                Console.WriteLine("测试次数" + Count);
            }
        }
        static void SendKey(string name, string l)
        {
            var win = FindWindow(null, name);

            keybd_event(0x01, 0, 0, 0);//激活TIM
            PostMessage(win, 0x0302, 0, 0);
            //    PostMessage(win, 0x0101, new Random().Next(65,128),0);//发送字符                                              //下面是发送回车
            PostMessage(win, 0x0100, 13, 0);
            PostMessage(win, 0x0101, 13, 0);
            keybd_event(0x11, 0, 0x0002, 0);

        }
    }
}

