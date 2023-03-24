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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Qualitea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            selectedTab = home;
        }
        Border selectedTab;
        private void menuitem_click(object sender, MouseButtonEventArgs e)
        {
            Border bd = (Border)sender;
            selectedTab.Style = this.FindResource("unselectedBtn") as Style;
            bd.Style = this.FindResource("selectedBtn") as Style;
            selectedTab = bd;
        }

        private void menuitem_mouseEnter(object sender, MouseEventArgs e)
        {
            Border bd = (Border)sender;
            bd.Style = this.FindResource("hoverBtn") as Style;
        }

        private void menuitem_mouseLeave(object sender, MouseEventArgs e)
        {
            Border bd = (Border)sender;
            bd.Style = this.FindResource("unselectedBtn") as Style;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
        }
    }
}
