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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using System.Drawing;

namespace Zxing_二维码条形码_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        [System.Runtime.InteropServices.DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 创建二维码图像
        /// </summary>
        /// <param name="content">要写入的内容</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        private ImageSource createQRCode(String content, int width, int height)
        {
            // options;
            //包含一些编码、大小等的设置
            //BarcodeWriter :一个智能类来编码一些内容的条形码图像
            // write = null;
            EncodingOptions options = new QrCodeEncodingOptions
            {
                //DisableECI = true,
                //CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 0
            };
            BarcodeWriter write = new BarcodeWriter();
            //设置条形码格式
            write.Format = BarcodeFormat.QR_CODE;
            //获取或设置选项容器的编码和渲染过程。
            write.Options = options;
            //对指定的内容进行编码，并返回该条码的呈现实例。渲染属性渲染实例使用，必须设置方法调用之前。
            Bitmap bitmap = write.Write(content);
            IntPtr ip = bitmap.GetHbitmap();//从GDI+ Bitmap创建GDI位图对象
                                            //Imaging.CreateBitmapSourceFromHBitmap方法，基于所提供的非托管位图和调色板信息的指针，返回一个托管的BitmapSource
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,
            System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }

        /// <summary>
        /// 条形码
        /// </summary>
        /// <param name="cont"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private ImageSource createItem(string cont, int width,int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 5
            };
            writer.Options = options;
            Bitmap bitmap = writer.Write(cont);

            IntPtr intPtr = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtr,IntPtr.Zero,Int32Rect.Empty,System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            return bitmapSource;


        }


        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //ewm.Source = createQRCode("牛逼不", 50, 50);
            txm.Source = createItem("9321048088888888888888888888888", 50, 50);
        }
    }
}
