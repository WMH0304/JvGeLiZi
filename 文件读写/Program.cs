using System;
using System.IO;
using System.Text;
namespace 文件读写
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool Slave_IO_Running = false;
            //bool Slave_SQL_Running = false;
            //string file = @"F:\举个栗子\举个栗子\文件读写\目标文档.txt";

            //using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            //using (StreamReader sr = new StreamReader(fs))
            //{
                //try
                //{
                //    string currentLine = sr.ReadLine();//从当前流中读取一行字符并将数据作为字符串返回。


                //    while (currentLine != null)
                //    {
                //        //判断Slave_IO_Running是否在运行
                //        if (currentLine.Contains("Slave_IO_Running"))//值指示指定的 String 对象是否出现在此字符串中。
                //        {
                //            Slave_IO_Running = currentLine.Split(':')[1].Trim() == "Yes" ? true : false;
                //        }
                //        //判断Slave_SQL_Running是否在运行
                //        if (currentLine.Contains("Slave_SQL_Running"))
                //        {
                //            Slave_SQL_Running = currentLine.Split(':')[1].Trim() == "Yes" ? true : false;
                //        }
                //        currentLine = sr.ReadLine();
                //        Console.WriteLine(currentLine);
                //    }
                //    fs.Close();
                //    sr.Close();
                //}
                //catch
                //{
                //}
                /*&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&*/
                string[] strs = new string[]
    {
        "10.100.182.89","10.100.182.90"
    };
                byte[] byData = new byte[100];
                char[] charData = new char[1000];
                using (StreamWriter writer = new StreamWriter(@"F:\\写入目标文件.txt"))
                    {
                        foreach (string s in strs)
                        {
                            writer.WriteLine(s);
                        }
                        writer.Close();
                    }
                /*实例读写文件类*/
                //FileStream file1 = new FileStream(@"F:\举个栗子\举个栗子\文件读写\写入目标文件.txt",FileMode.Open);
                //file1.Seek(0,SeekOrigin.Begin);//指定开头
                //file1.Read(byData, 0, 100);
                //Decoder decoder = Encoding.Default.GetDecoder();
                //decoder.GetChars(byData, 0, byData.Length, charData, 0);
                //Console.WriteLine(charData);
                //file1.Close();
                /*&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&7*/

                StreamReader stream = new StreamReader(@"F:\\写入目标文件.txt", Encoding.Default);
                String line;
                while((line = stream.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString()); 
                }
            /*&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&*/

            FileStream file = new FileStream(@"F:\\at.txt", FileMode.Create);//在f 盘建一个 名为 at 的文本

            byte[] data = Encoding.Default.GetBytes("Hello World!");//内容
            //开始写入
            file.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            file.Flush();
            file.Close();
        }
        }
    }

