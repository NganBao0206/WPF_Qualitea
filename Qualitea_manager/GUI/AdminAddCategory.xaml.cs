using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using BUS;

namespace Qualitea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminAddCategory : Window
    {

        public AdminAddCategory()
        {
            InitializeComponent();
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addNew(object sender, RoutedEventArgs e)
        {
            if (BUS_Category.addNewCategory(txtName.Text))
            {
                MessageBox.Show("Thêm thành công");
                Admin mainWindow = (Admin)Application.Current.MainWindow;
                mainWindow.categoryList.ItemsSource = BUS_Category.getCategories();
                txtName.Text = "";
            }
            else
                MessageBox.Show("Thêm thất bại");

        }
    }
}
