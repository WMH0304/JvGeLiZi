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
using System.Windows.Media.Animation;
namespace WPFUI_test.Test
{
    /// <summary>
    /// Easing.xaml 的交互逻辑
    /// </summary>
    public partial class Easing : Window
    {
        public Easing()
        {
            InitializeComponent();
        }
    }


    /// <summary>
    /// 自定义缓动函数
    /// </summary>
    public class RandomJitterEase : EasingFunctionBase
    {
        /// <summary>
        /// 返回一个缓动函数类
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new RandomJitterEase();
        }

        /// <summary>
        /// 调整动画的进度
        /// </summary>
        /// <param name="normalizedTime"></param>
        /// <returns></returns>
        protected override double EaseInCore(double normalizedTime)
        {
            return Math.Pow(normalizedTime, 3);
        }
    }
}
