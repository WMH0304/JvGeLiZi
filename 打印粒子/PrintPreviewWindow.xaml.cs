using System;
using System.Windows;
using System.Windows.Documents;
using System.IO;
using System.IO.Packaging;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;

namespace 打印粒子
{
    /// <summary>
    /// PrintPreviewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        
        private delegate void LoadXpsMethod();
        private readonly Object m_data;
        //FlowDocument 用高级文档功能（如分页和列）承载流内容和设置流内容格式。
        //存储流文档的容器
        private readonly FlowDocument m_doc;


        /// <summary>
        /// 容器
        /// </summary>
        /// <param name="strTmplName"></param>
        /// <param name="data"></param>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static FlowDocument LoadDocumentAndRender(string strTmplName, Object data, IDocumentRenderer renderer = null)
        {
            //strTmplName 流文件地址
            // UriKind.RelativeOrAbsolute 用指定的 URI 初始化 Uri 类的新实例。 此构造函数允许指定 URI 字符串是相对 URI、绝对 URI 还是不确定
            //打开流文件 并存储到流文档容器中
            FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri(strTmplName, UriKind.RelativeOrAbsolute));
            //设置外边距
            doc.PagePadding = new Thickness(50);
            //DataContext 获取或设置元素在参与数据绑定时的数据上下文。
            doc.DataContext = data;
            if (renderer != null)
            {
                renderer.Render(doc, data);
            }
            return doc;
        }

        /// <summary>
        /// 构造函数
        /// </summary>,
        /// <param name="strTmplName"></param>
        /// <param name="data"></param>
        /// <param name="renderer"></param>
        public PrintPreviewWindow(string strTmplName, Object data, IDocumentRenderer renderer = null)
        {
            InitializeComponent();
            m_data = data;
            m_doc = LoadDocumentAndRender(strTmplName, data, renderer);
            // Dispatcher 提供用于管理线程工作项队列的服务。
            // BeginInvoke 在与 Dispatcher 关联的线程上异步执行委托。
            // DispatcherPriority描述可通过 Dispatcher 调用操作的优先级。
            // DispatcherPriority.ApplicationIdle 。在应用程序空闲时处理操作。
            /*
             * 按指定的优先级在与 Dispatcher 关联的线程上异步执行指定的委托。
             */
            Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadXps()
        {
            //构造一个基于内存的xps document
            MemoryStream ms = new MemoryStream();// 创建一个流，其后备存储为内存。
            //Package 表示可以存储多个数据对象的容器。
            //创建一个package 容器存储  MemoryStream 流内存 并设置读写权限
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);//铲除 package 中 指定的内存块
            PackageStore.AddPackage(DocumentUri, package);//在package 中添加指定内存块
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

            //将flow document写入基于内存的xps document中去
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

            writer.Write(((IDocumentPaginatorSource)m_doc).DocumentPaginator);

            //获取这个基于内存的xps document的fixed document
            docViewer.Document = xpsDocument.GetFixedDocumentSequence();

            //关闭基于内存的xps document
            xpsDocument.Close();
        }
    }
}
