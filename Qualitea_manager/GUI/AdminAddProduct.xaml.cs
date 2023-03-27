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

namespace Qualitea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminAddProduct : Window
    {
        string imagePath;
        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<TextBox> textBoxes = new List<TextBox>();
        
        public AdminAddProduct()
        {
            InitializeComponent();
            comboBoxCate.DisplayMemberPath = "Name";
            comboBoxCate.SelectedValuePath = "CategoryID";
            comboBoxCate.ItemsSource = BUS_Category.getCategories();
            myList.ItemsSource = BUS_Size.getSizes();  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkBoxes = FindVisualChildren<CheckBox>(myList).ToList();
            textBoxes = FindVisualChildren<TextBox>(myList).ToList();
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
            int count = 0;
            List<DTO.Size> listSize = new List<DTO.Size>();
            List<Decimal> money = new List<Decimal>();
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
            for (int i = 0; i < checkBoxes.Count; i++) 
            {
                if (checkBoxes[i].IsChecked  == true)
                {
                    count++;
                    if (textBoxes[i].Text == "")
                    {
                        textBoxes[i].BorderBrush = Brushes.Red;
                        mess += "Chưa nhận đầy đủ giá\n";
                        MessageBox.Show(mess);
                        return;
                    }
                    else
                    {
                        DTO.Size temp = myList.Items[i] as DTO.Size;
                        decimal m;
                        if (Decimal.TryParse(textBoxes[i].Text, out m))
                        {
                            listSize.Add(temp);
                            money.Add(m/1000);
                        }
                        else
                        {
                            mess += "Giá tiền không hợp lệ\n";
                            MessageBox.Show(mess);
                            return;
                        }
                    }
                }
            } //Kiểm tra các size và giá tiền
            if (count == 0)
            {
                mess += "Bạn chưa chọn size cho sản phẩm\n";
                MessageBox.Show(mess);
                return;
            } //Kiểm tra nếu không chọn size nào
            if(BUS_Product.addProduct(namePrd.Text, (int)comboBoxCate.SelectedValue, imagePath, listSize, money))
            {
                namePrd.Text = "";
                imagePath = "";
                imageProduct.Source = null;
                Panel.SetZIndex(imageBox, 0);
                for (int i = 0; i < checkBoxes.Count; i++)
                {
                    checkBoxes[i].IsChecked = true;
                    textBoxes[i].Text = "";
                }
                MessageBox.Show("Thêm thành công");
            }
            else
                MessageBox.Show("Đã có lỗi xảy ra không thể thêm");
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    else
                    {
                        T childOfChild = FindVisualChild<T>(child);
                        if (childOfChild != null)
                            return childOfChild;
                    }
                }
                return null;
            }
            return null;
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

    }
}
