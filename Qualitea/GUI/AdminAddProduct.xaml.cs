using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DTO;
using Microsoft.Win32;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminAddProduct : Window
    {
        string imagePath;
        private BUS_Category busCategory = new BUS_Category();
        private BUS_Product busProduct = new BUS_Product();

        public AdminAddProduct()
        {
            InitializeComponent();
            comboBoxCate.DisplayMemberPath = "Name";
            comboBoxCate.SelectedValuePath = "CategoryID";
            comboBoxCate.ItemsSource = busCategory.getCategories();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //checkBoxes = FindVisualChildren<CheckBox>(myList).ToList();
            //textBoxes = FindVisualChildren<TextBox>(myList).ToList();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void uploadImage(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image | *.png; *.jpeg; *.jpg";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                Panel.SetZIndex(imageBox, 1);
                imagePath = dlg.FileName;
                BitmapImage imgPrd = new BitmapImage();
                imgPrd.BeginInit();
                imgPrd.UriSource = new Uri(imagePath);
                imgPrd.EndInit();
                imageProduct.Source = imgPrd;
                uploadButton.BorderBrush = Brushes.White;
                uploadButton.Opacity = 0.5;
            }
        }

        private void imageBox_MouseEnter(object sender, MouseEventArgs e)
        {
            Panel.SetZIndex(imageBox, 0);
        }

        private void imageBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imageProduct.Source != null)
                Panel.SetZIndex(imageBox, 1);
        }

        private void addPrd_Click(object sender, RoutedEventArgs e)
        {
            string mess = "";
            if (imageProduct.Source == null)
            {
                uploadButton.BorderBrush = Brushes.Red;
                mess += "Chưa thêm ảnh sản phẩm";
                MessageBox.Show(mess);
                return;
            } //Kiểm tra ảnh
            if (namePrd.Text == "")
            {
                boxNamePrd.BorderBrush = Brushes.Red;
                mess += "Chưa nhập tên sản phẩm\n";
                MessageBox.Show(mess);
                return;
            } //Kiểm tra tên
            if (comboBoxCate.SelectedItem == null)
            {
                comboBoxCate.Style = (Style)FindResource("ComboBoxStyle2");
                mess += "Chưa chọn loại sản phẩm\n";
                MessageBox.Show(mess);
                return;
            } //Kiểm tra loại

            if (myList.Items.Count == 0)
            {
                mess += "Bạn chưa chọn size cho sản phẩm\n";
                MessageBox.Show(mess);
                return;
            } //Kiểm tra nếu không chọn size nào
            List<ProductOption> productOptions = new List<ProductOption>();
            for (int i = 0; i < myList.Items.Count; i++)
            {
                ProductOption po = myList.Items[i] as ProductOption;
                if (String.IsNullOrEmpty(po.Size) || po.Price <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin về các size");
                    return;
                }
                productOptions.Add(po);
            } //Kiểm tra các size và giá tiền
            Product p = new Product();
            p.Name = namePrd.Text;
            p.CategoryID = (int)comboBoxCate.SelectedValue;
            p.Image = imagePath;
            
            if (busProduct.addProduct(p, productOptions))
            {
                namePrd.Text = "";
                imagePath = "";
                imageProduct.Source = null;
                Panel.SetZIndex(imageBox, 0);
                myList.Items.Clear();
                MessageBox.Show("Thêm thành công");
            }
            else
                MessageBox.Show("Đã có lỗi xảy ra không thể thêm");
        }

        private void namePrd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (namePrd.Text != "")
                boxNamePrd.BorderBrush = new SolidColorBrush(Color.FromRgb(67, 73, 72));
        }

        private void comboBoxCate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCate.SelectedItem != null)
                comboBoxCate.Style = (Style)FindResource("ComboBoxStyle1");

        }

        private void price_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox thisPrice = (TextBox)sender;
            if (thisPrice.Text != "") 
                thisPrice.BorderBrush = new SolidColorBrush(Color.FromRgb(67, 73, 72));
        }

        private void addSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductOption po = new ProductOption();
            myList.Items.Add(po);
        }

        private void removeSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border b = (Border)sender;
            ProductOption po = b.DataContext as ProductOption;
            myList.Items.Remove(po);
        }
    }
}
