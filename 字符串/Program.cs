using System.Drawing;
using System.Drawing.Imaging;

namespace 字符串
{
    class Program
    {
        static void Main(string[] args)
        {

            getint(100);
            //Parallel.For(0, 1000000, (i,c) => {
            //    Console.WriteLine(i);
            //});

            // Console.WriteLine(0xc29);

            //string str = "sdfoskdf";
            //// Console.WriteLine(str.Substring(str.Length-3,3));//.Split(str.Length - 4)
            //string s = "1,2,3,4,5";
            //string[] p = s.Split(',');

            ////dsfads 
            //str.Reverse();

            //if (s.Contains("2") && !s.Contains("3"))
            //{

            //}
            //foreach (var item in p)
            //{
            //    Console.WriteLine(item);
            //}
            // InserImage(str);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static void getint(int a)
        {
            /*
            问题： 求一个数的所有因数， 例如 4 有 1，4，2，2 
            然后找出最接近的因子对   2,2
             */
            string num = string.Empty;
            var num1 = int.MaxValue;
            var st = string.Empty;
            var sts = string.Empty;
            System.Collections.Generic.Dictionary<int, int> vs = new System.Collections.Generic.Dictionary<int, int>();
            System.Collections.Generic.List<int> vs1 = new System.Collections.Generic.List<int>();
            for (int i = 1; i <= a; i++)
            {
                if (a % i == 0)
                {
                    if (!vs.ContainsKey(a / i))
                    {
                        vs.Add(i, a / i);
                        var text = i + "和" + a / i + "  ";
                        num += text;
                        var t = i;
                        var t1 = a / i;
                        
                        if (t>t1)
                        {
                            if (num1 >= t - t1)
                            {
                                num1 = t - t1;
                            }
                          
                        }
                        if (t1>t)
                        {
                            if (num1 >= t1 - t)
                            {
                                num1 = t1 - t;
                            }
                        }
                        sts = "最接近的因数是" + t1 + "和" + t;
                    }
                }
            }
            System.Console.WriteLine(num);

            System.Console.WriteLine(sts);

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
