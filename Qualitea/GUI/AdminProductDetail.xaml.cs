using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AdminProductDetail : Window
    {
        public Product product { get; set; }
        string imagePath = "";
        private BUS_Category busCategory = new BUS_Category();
        private BUS_Product busProduct = new BUS_Product();
        private BUS_Order busOrder = new BUS_Order();
        private Product _product { get; set; }
        private ObservableCollection<ProductOption> productOptions;

        public AdminProductDetail()
        {
            InitializeComponent();
            comboBoxCate.DisplayMemberPath = "Name";
            comboBoxCate.SelectedValuePath = "CategoryID";
            comboBoxCate.ItemsSource = busCategory.getCategories();
            _product = new Product();
            //myList.ItemsSource = product.ProductOptions;
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
            _product = new Product();
            _product.ProductID = product.ProductID;
            _product.Name = product.Name;
            _product.CategoryID = product.CategoryID;
            _product.IsActive = product.IsActive;
            _product.Image = product.Image;
            _product.ProductOptions.Clear();
            List<ProductOption> pos = new List<ProductOption>();
            foreach (ProductOption po in product.ProductOptions)
            {
                ProductOption poClone = new ProductOption();
                poClone.ProductOptionID = po.ProductOptionID;
                poClone.ProductID = po.ProductID;
                poClone.Price = po.Price;
                poClone.Size = po.Size;
                poClone.IsActive = po.IsActive;
                pos.Add(poClone);
            }
            _product.ProductOptions = pos;
            ProductInfo.DataContext = _product;
            imageBox.DataContext = _product;

            productOptions = new ObservableCollection<ProductOption>();
            myListOption.ItemsSource = productOptions;
            BitmapImage imgPrd = new BitmapImage();
            imgPrd.BeginInit();
            imgPrd.UriSource = new Uri(product.Image);
            imgPrd.EndInit();
            imgPrd.DownloadCompleted += (s, e) =>
            {
                getColorFromImg(imgPrd);
            };
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
            thisPrice.Text = thisPrice.Text.Replace(".", "");
            thisPrice.CaretIndex = thisPrice.Text.Length;
            if (!String.IsNullOrWhiteSpace(thisPrice.Text))
                thisPrice.BorderBrush = new SolidColorBrush(Color.FromRgb(67, 73, 72));
        }

        private void addSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductOption po = new ProductOption();
            po.ProductID = _product.ProductID;
            productOptions.Add(po);
            isActive.IsChecked = true;

        }

        private void removeSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border b = (Border)sender;
            ProductOption po = b.DataContext as ProductOption;
            productOptions.Remove(po);
            myListOption.UpdateLayout();
            bool flag = false;
            foreach (ProductOption p in _product.ProductOptions)
            {
                if (p.IsActive)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag && myListOption.Items.Count == 0)
                isActive.IsChecked = false;
        }

        private void editPrd_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa sản phẩm này không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                string mess = "";
                if (myList.Items.Count + myListOption.Items.Count > 0)
                {
                    if (imageProduct.Source == null)
                    {
                        uploadButton.BorderBrush = Brushes.Red;
                        mess += "Chưa thêm ảnh sản phẩm";
                        MessageBox.Show(mess);
                        return;
                    } //Kiểm tra ảnh

                    if (String.IsNullOrWhiteSpace(namePrd.Text) || !Regex.IsMatch(namePrd.Text, @"[\p{L}\s]+$"))
                    {
                        boxNamePrd.BorderBrush = Brushes.Red;
                        mess += "Chưa nhập tên sản phẩm\n";
                        MessageBox.Show(mess);
                        return;
                    } //Kiểm tra tên
                    namePrd.Text = namePrd.Text.Trim().Replace(@"\s+", " ");

                    if (comboBoxCate.SelectedItem == null)
                    {
                        comboBoxCate.Style = (Style)FindResource("ComboBoxStyle2");
                        mess += "Chưa chọn loại sản phẩm\n";
                        MessageBox.Show(mess);
                        return;
                    } //Kiểm tra loại

                    for (int i = 0; i < productOptions.Count; i++)
                    {
                        ProductOption po = productOptions[i];
                        if (String.IsNullOrEmpty(po.Size))
                        {
                            MessageBox.Show("Vui lòng điền đủ thông tin về kích thước");
                            return;
                        }
                        if (!Regex.IsMatch(po.Size, @"[a-zA-Z\s]+$"))
                        {
                            MessageBox.Show("Tồn tại size không đúng định dạng");
                            return;
                        }
                        if (po.Price <= 0)
                        {
                            MessageBox.Show("Giá tiền phải lớn hơn 0");
                            return;
                        }
                    } //Kiểm tra các size và giá tiền mới

                    foreach (ProductOption po in _product.ProductOptions)
                    {
                        if (String.IsNullOrEmpty(po.Size))
                        {
                            MessageBox.Show("Vui lòng điền đủ thông tin về kích thước");
                            return;
                        }
                        if (!Regex.IsMatch(po.Size, @"[a-zA-Z\s]+$"))
                        {
                            MessageBox.Show("Tồn tại size không đúng định dạng");
                            return;
                        }
                        if (po.Price <= 0)
                        {
                            MessageBox.Show("Giá tiền phải lớn hơn 0");
                            return;
                        }
                    }

                    if (imagePath != "")
                        _product.Image = imagePath;

                    if (busProduct.editProduct(_product, productOptions.ToList()))
                    {
                        MessageBox.Show("Sửa thành công");
                        busProduct = new BUS_Product();
                        product = busProduct.getProduct(product);
                        productOptions.Clear();

                        init();
                        CollectionViewSource.GetDefaultView(myList.ItemsSource).Refresh();
                        myList.UpdateLayout();
                    }
                    else
                        MessageBox.Show("Đã có lỗi xảy ra không thể sửa");
                }
                else
                {
                    if (MessageBox.Show("Sản phẩm không thể không có thông tin về size, bạn có muốn xóa sản phẩm này không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
                    {
                        if (busOrder.GetOrderDetailsByProductID(_product.ProductID).Count > 0)
                        {
                            MessageBox.Show("Sản phẩm này đã được bán không thể xóa, bạn chỉ có thể xóa sản phẩm chưa bán được");
                            return;
                        }
                        if (busProduct.delProduct(_product))
                        {
                            MessageBox.Show("Xóa thành công");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đã có lỗi xảy ra, xóa không thành công");
                        }
                    }
                }
            }
        }

        private void options_Checked(object sender, RoutedEventArgs e)
        {
            isActive.IsChecked = true;
        }

        private void options_Unchecked(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            foreach (ProductOption p in _product.ProductOptions)
            {
                if (p.IsActive)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag && myListOption.Items.Count == 0)
                isActive.IsChecked = false;
        }

        private void isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (ProductOption po in _product.ProductOptions)
                po.IsActive = false;
            CollectionViewSource.GetDefaultView(myList.ItemsSource).Refresh();
            productOptions.Clear();
        }

        private void isActive_Checked(object sender, RoutedEventArgs e)
        {

            if (myList != null && myList.Items.Count > 0)
            {
                bool flag = false;
                foreach (ProductOption p in _product.ProductOptions)
                {
                    if (p.IsActive)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag && myListOption.Items.Count == 0)
                    foreach (ProductOption po in _product.ProductOptions)
                        po.IsActive = true;
                CollectionViewSource.GetDefaultView(myList.ItemsSource).Refresh();
            }


        }

        private void removeOldSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Border b = (Border)sender;
            ProductOption po = b.DataContext as ProductOption;
            BUS_Order bo = new BUS_Order();
            if (bo.GetOrderDetailsByProductOption(po.ProductOptionID).Count > 0)
            {
                MessageBox.Show("Không thể xóa vì kích cỡ này đã được bán, thay vào đó xin vui lòng bỏ tick để vô hiệu hóa kích cỡ này");
                return;
            }

            _product.ProductOptions.Remove(po);
            bool flag = false;
            foreach (ProductOption p in _product.ProductOptions)
            {
                if (p.IsActive)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag && myListOption.Items.Count == 0)
                isActive.IsChecked = false;
            CollectionViewSource.GetDefaultView(myList.ItemsSource).Refresh();
        }

        private void delPrd_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                if (busOrder.GetOrderDetailsByProductID(_product.ProductID).Count > 0)
                {
                    MessageBox.Show("Sản phẩm này đã được bán không thể xóa, bạn chỉ có thể xóa sản phẩm chưa bán được");
                    return;
                }
                if (busProduct.delProduct(_product))
                {
                    MessageBox.Show("Xóa thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, xóa không thành công");
                }
            }
                
        }
    }
}
