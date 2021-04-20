using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUI_test.Tools
{
    class VisualTest :Panel
    {
        private List<Visual> visuals = new List<Visual>();

        /// <summary>
        /// 重写获取可视化方法
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        /// <summary>
        /// 返回条数
        /// </summary>

        protected override int VisualChildrenCount =>visuals.Count ;

        /// <summary>
        /// 添加可视化对象
        /// </summary>
        /// <param name="visual"></param>
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);
            base.AddLogicalChild(visual);
            base.AddVisualChild(visual);
        }
        /// <summary>
        /// 移除可视化对象
        /// </summary>
        /// <param name="visual"></param>
        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            base.RemoveVisualChild(visual);//移除两个可视对象之间的父子关系
            //从此元素的逻辑树中移除所提供的对象。 FrameworkElement 将更新受影响的逻辑树父级指针，以便与此删除操作保持同步
            base.RemoveLogicalChild(visual);

        }

        /// <summary>
        /// 命中测试
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public DrawingVisual GetVisual(Point point)
        {
            //HitTestResult 为表示命中测试返回值的若干个派生类提供基类 VisualTreeHelper  用于执行涉及可视化树中的节点的常规任务。

            HitTestResult hitTestResult = VisualTreeHelper.HitTest(this, point);
            if (hitTestResult !=null)
                return hitTestResult.VisualHit as DrawingVisual; //返回 DrawingVisual 对象
           else
                return null;


        }
    }
}
