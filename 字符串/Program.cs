using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace 字符串
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "sdfoskdf";
            // Console.WriteLine(str.Substring(str.Length-3,3));//.Split(str.Length - 4)
            string s = "1,2,3,4,5";
            string[] p = s.Split(',');
            str.Reverse();
            foreach (var item in p)
            {
                Console.WriteLine(item);
            }
            // InserImage(str);
        }
        /// <summary>
        /// 图片生成
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static byte[] InserImage(string name)
        {  //新增图片，宽高为50
            Bitmap bt = new Bitmap(50, 50);
            //从指定的 Image 创建新的 Graphics。 绘画对象
            Graphics gh = Graphics.FromImage(bt);
            //填充背景色
            gh.Clear(Color.White);
            //设置字体颜色
           // DateTime dateTime = DateTime.Now();
            SolidBrush brush = new SolidBrush(Color.Black);
            /*
             * 
             * 在指定矩形并且用指定的 Brush 和 Font 对象绘制指定的文本字符串。
             * 
             *  gh.DrawString(绘制的字符串, 文本格式, 字体颜色, 左上角x坐标,左上角y坐标); 
             *  
             *  通过两个坐标确定文本再图片位置
             * 
             */
            gh.DrawString(name, new Font("宋体", 15), brush, 10, 10);
            //创建内存流
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //图片写入流，绑定图片格式
            bt.Save(ms, ImageFormat.Jpeg);

            return ms.ToArray();

        }
    }
}
