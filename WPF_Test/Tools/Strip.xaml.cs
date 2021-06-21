using System.Windows;

namespace WPF_Test.Tools
{
    /// <summary>
    /// Strip.xaml 的交互逻辑
    /// </summary>
    public partial class Strip : Window
    {
        public Strip()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static System.Drawing.Bitmap Generate1(string text, int width, int height)
        {
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            ZXing.QrCode.QrCodeEncodingOptions options = new ZXing.QrCode.QrCodeEncodingOptions()
            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距,单位不是固定像素
            };

            writer.Options = options;
            System.Drawing.Bitmap map = writer.Write(text);
            return map;
        }
        /// <summary>
        /// 生成带Logo的二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static System.Drawing.Bitmap Generate3(string text, int width, int height)
        {
            //Logo 图片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\img\logo.png";
            System.Drawing.Bitmap logo = new System.Drawing.Bitmap(logoPath);
            //构造二维码写码器
            ZXing.MultiFormatWriter writer = new ZXing.MultiFormatWriter();
            System.Collections.Generic.Dictionary<ZXing.EncodeHintType, object> hint = new System.Collections.Generic.Dictionary<ZXing.EncodeHintType, object>();
            hint.Add(ZXing.EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(ZXing.EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
            //hint.Add(EncodeHintType.MARGIN, 2);//旧版本不起作用，需要手动去除白边

            //生成二维码 
            ZXing.Common.BitMatrix bm = writer.encode(text, ZXing.BarcodeFormat.QR_CODE, width + 30, height + 30, hint);
            bm = deleteWhite(bm);
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            System.Drawing.Bitmap map = barcodeWriter.Write(bm);

            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = System.Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = System.Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            System.Drawing.Bitmap bmpimg = new System.Drawing.Bitmap(map.Width, map.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, height);
                //白底将二维码插入图片
                g.FillRectangle(System.Drawing.Brushes.White, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            return bmpimg;
        }
        /// <summary>
        /// 删除默认对应的空白
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static ZXing.Common.BitMatrix deleteWhite(ZXing.Common.BitMatrix matrix)
        {
            int[] rec = matrix.getEnclosingRectangle();
            int resWidth = rec[2] + 1;
            int resHeight = rec[3] + 1;

            ZXing.Common.BitMatrix resMatrix = new ZXing.Common.BitMatrix(resWidth, resHeight);
            resMatrix.clear();
            for (int i = 0; i < resWidth; i++)
            {
                for (int j = 0; j < resHeight; j++)
                {
                    if (matrix[i + rec[0], j + rec[1]])
                        resMatrix[i, j] = true;
                }
            }
            return resMatrix;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var test = Generate1("test", 100, 100);

            

        }
    }
}
