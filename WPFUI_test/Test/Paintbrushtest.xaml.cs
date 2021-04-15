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
    /// Paintbrushtest.xaml 的交互逻辑
    /// </summary>
    public partial class Paintbrushtest : Window
    {
        public Paintbrushtest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          //  this.btn.Foreground = new SolidColorBrush(Color.FromRgb(144, 177, 172));
            this.btn.Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
