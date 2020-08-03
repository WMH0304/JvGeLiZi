using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

namespace 打印粒子
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        private void Btprint_Click(object sender, RoutedEventArgs e)
        {
            #region 基础打印
            /*
                         PrintDialog print = new PrintDialog();
            if (print.ShowDialog() ==true)
            {
                //隐藏grid
                grid.Visibility = Visibility.Hidden;

                //放大5倍打印
                ///canvas.LayoutTransform = new ScaleTransform(5,5);

                // Run初始化 应包含一连串格式化或未格式化文本的内联级别的 流 内容元素。
                // Run初始化 Run 类的一个新实例，将指定字符串作为文本运行的初始内容。
                Run run = new Run("一身污浊，自得其乐，谁的命运，谁又能把握" +"这是你要的，物质生活");
                TextBlock textBlock = new TextBlock();
                //将 字符流 流 写入容器中
                textBlock.Inlines.Add(run);
                //设置外边距
                textBlock.Margin = new Thickness(10);

                //行适应页面宽度 多出来的转行
                textBlock.TextWrapping = TextWrapping.Wrap;

                int pagemargin = 5;
                // Size 实现用于描述对象的 Size 的结构。
                //PrintableAreaWidth  一个 System.Double ，表示可打印的页面区域的宽度。
                //PrintableAreaHeight 一个 System.Double 表示可打印的页面区域的高度。
                Size size = new Size(print.PrintableAreaWidth - pagemargin, print.PrintableAreaHeight - pagemargin);

                //触发元素的大小调整
                // 在 Measure 调用期间，元素通过使用 size 输入来确定其大小要求
                canvas.Measure(size);
                // Arrange定位子元素并确定 UIElement 的大小。
                // Rect初始化 Rect 结构的新实例，此结构具有指定的 x 坐标、y 坐标、宽度和高度。
                canvas.Arrange(new Rect(pagemargin, pagemargin, size.Height, size.Width));

                // PrintDocument开始文档的打印进程。
               
                //打印元素
                print.PrintVisual(textBlock, "dsd");
                //还原画板
                canvas.LayoutTransform = null;
                grid.Visibility = Visibility.Visible;


            }





             */
            #endregion
            #region 文档打印
            //PrintDialog print = new PrintDialog();
            ////if (print.ShowDialog() ==true)
            ////{
            ////    FlowDocument doc = new FlowDocument();
            ////    //设置高度
            ////    doc.PageHeight = print.PrintableAreaHeight;
            ////    doc.PageWidth = print.PrintableAreaWidth;
            ////    // 要打印的作业的说明。 此文本出现在 用户界面 (UI) 的打印机。
            ////    print.PrintDocument(((IDocumentPaginatorSource)(doc)).DocumentPaginator, "sdfad");
            ////}


            //FlowDocument doc = new FlowDocument();
            //DocumentPaginator documentPaginator = ((IDocumentPaginatorSource)(doc)).DocumentPaginator;


            #endregion

            #region 打印预览
            /*
             * 
             */
           
            PrintPreviewWindow previewWnd = new PrintPreviewWindow("OrderDocument.xaml", GlobalData.m_orderExample, new OrderDocumentRenderer());//在这里我们将FlowDocument.xaml这个页面传进去，之后通过打印预览窗口的构造函数填充打印内容,如果有数据要插入应该在此传数据结构进去
            previewWnd.Owner = this;
            previewWnd.ShowInTaskbar = false;//设置预览窗体在最小化时不要出现在任务栏中 
            previewWnd.ShowDialog();//显示打印预览窗体

            #endregion
        }


    }
}
