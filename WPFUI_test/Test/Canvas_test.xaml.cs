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
    /// Canvas_test.xaml 的交互逻辑
    /// </summary>
    public partial class Canvas_test : Window
    {
        public Canvas_test()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ic_cont.EditingMode = (InkCanvasEditingMode)this.cb_cont.SelectedItem;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (InkCanvasEditingMode item in Enum.GetValues(typeof(InkCanvasEditingMode)))
            {
                cb_cont.Items.Add(item);
            }
            cb_cont.SelectedIndex = 0;

        }
        /*
private void bt_button1_Click(object sender, RoutedEventArgs e)
{
Canvas.SetZIndex(this.bt_button2, 55);
}*/
    }
}
