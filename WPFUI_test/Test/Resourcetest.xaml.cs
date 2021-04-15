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
    /// Resourcetest.xaml 的交互逻辑
    /// </summary>
    public partial class Resourcetest : Window
    {
        public Resourcetest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
           // this.stackPanel.Resources["TileBrush"] = new SolidColorBrush(Colors.Red);
        }
    }
}
