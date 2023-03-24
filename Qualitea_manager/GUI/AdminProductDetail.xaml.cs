using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AdminProductDetail : Window
    {
        public Product product { get; set; }
        string imagePath = "";
        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<TextBox> textBoxes = new List<TextBox>();
        public AdminProductDetail()
        {
            InitializeComponent();
            comboBoxCate.DisplayMemberPath = "Name";
            comboBoxCate.SelectedValuePath = "CategoryID";
            comboBoxCate.ItemsSource = BUS_Category.getCategories();
           
            myList.ItemsSource = BUS_Size.getSizes();  
        }

        private void getColorFromImg(BitmapImage imgPrd)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(300, 300, 96d, 96d, PixelFormats.Default);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawImage(imgPrd, new Rect(0, 0, 300, 300));
            }
            rtb.Render(dv);

            // Lấy bộ đệm pixel
            int stride = rtb.PixelWidth * ((rtb.Format.BitsPerPixel + 7) / 8);
            byte[] pixels = new byte[stride * rtb.PixelHeight];
            rtb.CopyPixels(pixels, stride, 0);

            // Lấy giá trị RGB tại điểm (x,y)
            int x = (int)(rtb.PixelWidth / 2 + 23);
            int y = (int)(rtb.PixelHeight / 2 - 20);
            int index = y * stride + x * ((rtb.Format.BitsPerPixel + 7) / 8);
            byte b = pixels[index];
            byte g = pixels[index + 1];
            byte r = pixels[index + 2];
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb((byte)(r + 10), (byte)(g + 10), (byte)(b + 5));
            baseBack.Background = brush;
        }
        private void init()
        {
            comboBoxCate.SelectedValue = product.CategoryID;
            prdId.Content = "Mã sản phẩm: " + product.ProductID.ToString();
            if (product.IsActive)
                isActive.IsChecked = true;
            else
                isActive.IsChecked = false;
            BitmapImage imgPrd = new BitmapImage();
            imgPrd.BeginInit();
            imgPrd.UriSource = new Uri(product.Image);
            imgPrd.EndInit();
            imgPrd.DownloadCompleted += (s, e) => getColorFromImg(imgPrd);

            getColorFromImg(imgPrd);
            imageProduct.Source = imgPrd;
            namePrd.Text = product.Name;
            List<ProductOption> productOptions = product.ProductOptions.ToList();
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                DTO.Size s = myList.Items[i] as DTO.Size;
                foreach (ProductOption o in productOptions)
                {
                    if (o.SizeID == s.SizeID)
                    {
                        textBoxes[i].Text = (o.Price * 1000).ToString("N0");
                        if (o.IsActive)
                            checkBoxes[i].IsChecked = true;
                        else
                            checkBoxes[i].IsChecked = false;
                        productOptions.Remove(o);
                        break;
                    }
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkBoxes = FindVisualChildren<CheckBox>(myList).ToList();
            textBoxes = FindVisualChildren<TextBox>(myList).ToList();
            init();
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
            }
        }

        private void imageBox_MouseEnter(object sender, MouseEventArgs e)
        {
            Panel.SetZIndex(imageBox, 0);
        }

        private void imageBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Panel.SetZIndex(imageBox, 1);
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

        private void editPrd_Click(object sender, RoutedEventArgs e)
        {
            string mess = "";
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
                if (checkBoxes[i].IsChecked == true)
                {
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
                            money.Add(m / 1000);
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
            if (BUS_Product.editProduct(product, namePrd.Text, (int)comboBoxCate.SelectedValue, imagePath, listSize, money))
            {
                init();
                MessageBox.Show("Sửa thành công");
                Admin mainWindow = (Admin)Application.Current.MainWindow;
                mainWindow.restartTabPrd();
            }
            else
                MessageBox.Show("Đã có lỗi xảy ra không thể sửa");
        }

        private void options_Checked(object sender, RoutedEventArgs e)
        {
            isActive.IsChecked = true;
        }

        private void options_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cbox in checkBoxes)
                if (cbox.IsChecked == true)
                    return;
            isActive.IsChecked = false;
        }

        private void isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            foreach (CheckBox cbox in checkBoxes)
                cbox.IsChecked = false;
        }

        private void isActive_Checked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cbox in checkBoxes)
                if (cbox.IsChecked == true)
                    return;
            List<ProductOption> productOptions = product.ProductOptions.ToList();
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                DTO.Size s = myList.Items[i] as DTO.Size;
                foreach (ProductOption o in productOptions)
                {
                    if (o.SizeID == s.SizeID)
                    {
                        if (o.IsActive)
                            checkBoxes[i].IsChecked = true;
                        else
                            checkBoxes[i].IsChecked = false;
                        productOptions.Remove(o);
                        break;
                    }
                }
            }
        }
    }
}
