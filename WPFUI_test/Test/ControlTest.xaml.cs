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
    /// ControlTest.xaml 的交互逻辑
    /// </summary>
    public partial class ControlTest : Window
    {
        public ControlTest()
        {
            InitializeComponent();
        }

  
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.btn.Background = new SolidColorBrush(Colors.Red);
            this.btn.Foreground = new SolidColorBrush(Colors.Blue);
            this.btn.Foreground = System.Windows.SystemColors.WindowTextBrush;//获取系统默认颜色
          
        }
    

        string s = "";
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.pb_test.Value += 2; // 进度条每次累加的单位

            //获取选中的日期
            for (int i = 0; i < this.cd.SelectedDates.Count; i++)
            {
                s += this.cd.SelectedDates[i].ToShortDateString() + ";";
                this.btn.Content = s;
            }
        }

        private void cd_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Title = this.cd.SelectedDate.ToString();
        }

        private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {

        }
    }
}
