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
    /// WriteableBitmapTest.xaml 的交互逻辑
    /// </summary>
    public partial class WriteableBitmapTest : Window
    {
        public WriteableBitmapTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // PixelFormats定义图像的像素格式和基于像素的图面。
            // dpiX 位图的水平每英寸点数
            // dpiY 位图的垂直每英寸点数
            WriteableBitmap wb = new WriteableBitmap((int)image.Width,(int)image.Height,96,96,PixelFormats.Pbgra32,null);
           
            Random rad = new Random();
            int stride =0;
            Int32Rect rect;
            byte[] colorData =null ;
            for (int i = 0; i < wb.PixelWidth; i++)
            {
                for (int j = 0; j < wb.PixelHeight; j++)
                {
                    byte blue =(byte) rad.Next(0,255);
                    byte green = (byte)rad.Next(0, 255);
                    byte red = (byte)rad.Next(0, 255);
                    byte alpha = (byte)rad.Next(0, 255);
                     colorData =new byte[] { blue, green, red, alpha };
                     rect = new Int32Rect(i,j,1,1);
                     stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;
                }
                wb.WritePixels(rect, colorData, stride, 0);
            }
           
            image.Source = wb;
        }
    }
}
