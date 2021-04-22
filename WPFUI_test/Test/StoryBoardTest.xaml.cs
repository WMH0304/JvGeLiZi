using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace WPFUI_test.Test
{
    /// <summary>
    /// StoryBoardTest.xaml 的交互逻辑
    /// </summary>
    public partial class StoryBoardTest : Window
    {
        public StoryBoardTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检索时间更改进度条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            Clock clock = (Clock)sender;
            if (clock == null)
                this.pg.Value = 0;
            else
                this.pg.Value = (double)clock.CurrentProgress;
        }
    }
}
