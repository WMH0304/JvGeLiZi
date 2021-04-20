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
    /// AnimationTest.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationTest : Window
    {
        public AnimationTest()
        {
            InitializeComponent();
        }
        #region 动画
        /*
        /// <summary>
        /// 动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();//动画类对象
            animation.From = this.button.ActualWidth;//指定初始值
            animation.To =   30;//指定结束值
            animation.AutoReverse = true;//获取或设置一个值，该值指示时间线在完成向前迭代后是否按相反的顺序播放
            animation.FillBehavior = FillBehavior.Stop;//恢复到初始状态
            animation.Duration = TimeSpan.FromSeconds(2);//动画执行时间
            animation.Completed += Animation_Completed;//动画执行时触发事件
            button.BeginAnimation(Button.WidthProperty, animation);//将动画附加到对象中

            DoubleAnimation animation1 = new DoubleAnimation();

            animation1.From = 50;
            animation1.To = this.Height;
            animation1.Duration = TimeSpan.FromSeconds(2);
            animation1.FillBehavior = FillBehavior.Stop;
            button.BeginAnimation(Button.HeightProperty, animation1);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            button.BeginAnimation(Button.WidthProperty, null);
            button.Width = 200;
        }
        */
        #endregion


        #region 动画时间线

       
        /// <summary>
        /// 动画时间线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = this.es.Width;
            animation.To = this.Width;
            animation.Duration = TimeSpan.FromSeconds(2);
            animation.AutoReverse = true;
            animation.AccelerationRatio = .3;//动画加速 整个动画过程中的前 30%加速，然后后面的 70% 会比不加速缓慢，要补偿前面的加速
            animation.DecelerationRatio = .6;//减速
            es.BeginAnimation(Ellipse.HeightProperty, animation);
            es.BeginAnimation(Ellipse.WidthProperty, animation);

        }

        #endregion
    }
}
