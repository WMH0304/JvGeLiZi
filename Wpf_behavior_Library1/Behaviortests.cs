using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Wpf_behavior_Library
{
    class Behaviortest : Behavior<UIElement>
    {
        private Canvas canvas;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;

        }
        //鼠标是否拖动
        private bool isDragging = false;

        //获取被点击元素点的位置
        private Point mouseOffset;

        /// <summary>
        /// 鼠标左键按下时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (canvas == null)
            {
                canvas = VisualTreeHelper.GetParent(this.AssociatedObject) as Canvas;

            }
            //拖动标示
            isDragging = true;

            //获取位置
            mouseOffset = e.GetPosition(AssociatedObject);

            //试图强制将鼠标捕获到此元素。
            AssociatedObject.CaptureMouse();



        }
        /// <summary>
        /// 鼠标在元素上移动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                //获取鼠标在画布中的相对位置
                Point point = e.GetPosition(canvas);

                //重新定义元素相对于画板的位置
                AssociatedObject.SetValue(Canvas.TopProperty, point.Y = mouseOffset.Y);
                AssociatedObject.SetValue(Canvas.TopProperty, point.X = mouseOffset.X);

            }
        }
        /// <summary>
        /// 鼠标左键释放时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                //释放鼠标
                AssociatedObject.ReleaseMouseCapture();
                //恢复状态
                isDragging = false;
            }
        }
   
    }
}
