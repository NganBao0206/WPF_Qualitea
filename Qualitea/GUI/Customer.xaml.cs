using DTO;
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
using System.Windows.Shapes;
using BUS;
using System.Text.RegularExpressions;
using System.IO;
using QRCoder;
using QRCoder.Xaml;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        public ObservableCollection<OrderDetail> od = new ObservableCollection<OrderDetail>();
        private ProductOption poSelect;
        private Border selected;
        private double total;
        private double discountTotal;
        private Grid selectedTab;
        private BUS_Product busProduct = new BUS_Product();
        private BUS_Category busCategory = new BUS_Category();
        private BUS_Order busOrder;
        private BUS_Customer busCustomer;
        private DTO.Customer customer;
        bool isApply = false;
        int ScoreApply;
        public Customer()
        {
            InitializeComponent();
            listProduct.ItemsSource = busProduct.getProducts(true);
            List<Category> categories = busCategory.getCategories();
            categories.Add(new Category() { CategoryID = -1, Name = "Tất cả" });
            comboBoxCategory.ItemsSource = categories;
            total = 0;
            discountTotal = 0;
            init();
            initScoreApply();

            
        }

        public void init()
        {
            busOrder = new BUS_Order();
            busCustomer = new BUS_Customer();
            userOrder.ItemsSource = busOrder.GetOrderHeadersByCustomerID(CurrentLogin.Instance.LoginID);
            customer = busCustomer.getCustomerByID(CurrentLogin.Instance.LoginID);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(customer.Score.ToString(), QRCodeGenerator.ECCLevel.H);
            XamlQRCode qrCode = new XamlQRCode(qrCodeData);

            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20, "#53825E", "transparent");
            QRCode.Source = qrCodeAsXaml;
            userSCore.DataContext = customer;
            card.DataContext = customer;
            isApply = false;
            firstProduct.DataContext = busProduct.getProducts()[3];
        }

        public void initScoreApply()
        {
            
            if (customer.Score == 0 || od.Count == 0)
            {
                ScoreApply_Bar.IsEnabled = false;
                ScoreApply_Button.IsEnabled = false;
                ScoreApply = 0;
                ScoreApply_Button.Style = this.FindResource("disable") as Style;
                ScoreApply_Bar.Style = this.FindResource("disable") as Style;
                isApply = false;
            }
            else
            {
                ScoreApply_Bar.IsEnabled = true;
                ScoreApply_Button.IsEnabled = true;
                ScoreApply = customer.Score < (int)(total * 10 / 100) ? customer.Score : (int)(total * 10 / 100);
            }
            txtApplyScore.Text = ScoreApply.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectedTab = tab1;
            //userPoint.DataContext = CurrentLogin.Instance.Login;
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void menuItem_Click(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < menu.Children.Count; i++)
            {
                Border item = menu.Children[i] as Border;
                TextBlock text = item.Child as TextBlock;
                text.Style = this.FindResource("darkFont") as Style;
            }
            Border selectedItem = sender as Border;
            TextBlock selectedText = selectedItem.Child as TextBlock;
            selectedText.Style = this.FindResource("greenFont") as Style;

            string tabName = "tab" + (menu.Children.IndexOf(selectedItem) + 1).ToString();
            Grid tab = this.FindName(tabName) as Grid;
            Panel.SetZIndex(selectedTab, 0);
            Panel.SetZIndex(tab, 1);
            selectedTab = tab;
        }

        public void loadPrd()
        {
            if (listProduct != null)
            {
                listProduct.ItemsSource = busProduct.getProducts(searchBoxProduct.Text, (int?)comboBoxCategory.SelectedValue, true, 0, 9999999999);
            }
        }

        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadPrd();
        }

        private void searchBoxProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadPrd();
        }

        private void minPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadPrd();
        }

        private void maxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadPrd();
        }

        SolidColorBrush color = new SolidColorBrush(Color.FromRgb(83, 130, 94));

        private void ProductOption_Selected(object sender, MouseButtonEventArgs e)
        {
            if (selected != null)
            {
                selected.Background = color;
            }
            Border newselected = sender as Border;
            selected = newselected;
            SolidColorBrush selectedcolor = new SolidColorBrush(Color.FromRgb(186, 211, 115));
            selected.Background = selectedcolor;
            ProductOption newPo = newselected.DataContext as ProductOption;
            poSelect = newPo;
            priceMainPrd.Text = newPo._price.ToString("N0") + "đ";
        }


        private void Product_Selected(object sender, MouseButtonEventArgs e)
        {
            Border brSelected = sender as Border;
            Product prd = brSelected.DataContext as Product;
            mainProduct.DataContext = prd;

            Panel.SetZIndex(beforeChoose, 0);
            selected = null;
            quantity.Text = 1.ToString();
            priceMainPrd.Text = prd.MinPrice.ToString("N0") + "đ";
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


        private void minus_Click(object sender, RoutedEventArgs e)
        {
            int qtt = int.Parse(quantity.Text);
            if (qtt <= 1)
                qtt = 1;
            else
                qtt--;
            quantity.Text = qtt.ToString();
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            int qtt = int.Parse(quantity.Text);
            qtt++;
            quantity.Text = qtt.ToString();
        }

        private void quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text == "";
        }

        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (quantity.Text == "")
                quantity.Text = 1.ToString();
        }

        private void addToCard_Click(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {

                bool flag = false;
                foreach (OrderDetail o in od)
                {
                    if (o.ProductOptionID == poSelect.ProductOptionID)
                    {
                        o.Quantity += int.Parse(quantity.Text);
                        o.TotalLine = o.Quantity * o.ProductOption.Price;
                        total += o.ProductOption._price * int.Parse(quantity.Text);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    OrderDetail newOD = new OrderDetail();
                    newOD.ProductOption = poSelect;
                    newOD.ProductOptionID = poSelect.ProductOptionID;
                    newOD.Quantity = int.Parse(quantity.Text);
                    newOD.TotalLine = newOD.Quantity * newOD.ProductOption.Price;
                    total += newOD.ProductOption._price * int.Parse(quantity.Text);
                    od.Add(newOD);
                }
                MessageBox.Show("Thêm vào giỏ thành công");
                initScoreApply();
                if (isApply) discountTotal = ScoreApply;
                quantity.Text = 1.ToString();
                cart.ItemsSource = od;
                txtTotal.Text = total.ToString("N0") + "đ";
                txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
                var view = CollectionViewSource.GetDefaultView(cart.ItemsSource);
                view.Refresh();
                

            }
            else
                MessageBox.Show("Vui lòng chọn size");
        }
        private void plusCart_Click(object sender, RoutedEventArgs e)
        {
            Button add = sender as Button;
            OrderDetail addOD = add.DataContext as OrderDetail;
            addOD.Quantity += 1;
            addOD.TotalLine = addOD.Quantity * addOD.ProductOption.Price;
            initScoreApply();
            if (isApply) discountTotal = ScoreApply;
            total += addOD.ProductOption._price;

            txtTotal.Text = total.ToString("N0") + "đ";
            txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            cart.ItemsSource = od;
            var view = CollectionViewSource.GetDefaultView(cart.ItemsSource);
            view.Refresh();
        }

        private void minusCart_Click(object sender, RoutedEventArgs e)
        {


            Button minus = sender as Button;
            OrderDetail minusOD = minus.DataContext as OrderDetail;
            if (minusOD.Quantity != 1)
            {
                minusOD.Quantity -= 1;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
                {
                    minusOD.Quantity -= 1;
                    od.Remove(minusOD);
                    selected.Background = color;
                }
                else
                    return;
            }
            minusOD.TotalLine = minusOD.Quantity * minusOD.ProductOption.Price;
            initScoreApply();
            if (isApply) discountTotal = ScoreApply;
            total -= minusOD.ProductOption._price;
            txtTotal.Text = total.ToString("N0") + "đ";
            txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            cart.ItemsSource = od;
            var view = CollectionViewSource.GetDefaultView(cart.ItemsSource);
            view.Refresh();
        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                Border del = sender as Border;
                OrderDetail delOD = del.DataContext as OrderDetail;
                od.Remove(delOD);
                initScoreApply();
                if (isApply) discountTotal = ScoreApply;
                selected.Background = color;
                total = total - delOD._totalLine;
                txtTotal.Text = (total).ToString("N0") + "đ";
                txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            }
        }

        private void showCart(object sender, MouseButtonEventArgs e)
        {
            tabCart.Visibility = Visibility.Visible;
        }

        private void hideCart(object sender, MouseButtonEventArgs e)
        {
            tabCart.Visibility = Visibility.Hidden;
        }




        private void backToCart(object sender, MouseButtonEventArgs e)
        {
            tabConfirm.Visibility = Visibility.Hidden;
        }

        private void continuePay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (od.Count > 0)
            {
                bill.ItemsSource = cart.ItemsSource;
                tabConfirm.Visibility = Visibility.Visible;
                billTotal.Text = txtTotal.Text;
                billDiscount.Text = txtDiscount.Text;
                billCash.Text = txtCash.Text;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để tiếp tục");
            }
        }
        
        private void btnCancel_Click(object sender, MouseButtonEventArgs e)
        {
            if (od.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc là hủy đơn hàng này không?", "Lưu ý", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    od.Clear();
                    cart.ItemsSource = od;
                    var view = CollectionViewSource.GetDefaultView(cart.ItemsSource);
                    view.Refresh();
                    txtTotal.Text = "0đ";
                    txtDiscount.Text = "0đ";
                    txtCash.Text = "0đ";
                    total = 0;
                    discountTotal = 0;
                    isApply = false;

                    initScoreApply();
                    MessageBox.Show("Hủy đơn thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
                MessageBox.Show("Bạn không thể hủy đơn khi không có sẵn phẩm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void confirmBill(object sender, MouseButtonEventArgs e)
        {
            if (!String.IsNullOrEmpty(receiverName.Text) && !String.IsNullOrEmpty(receiverAddress.Text) && !String.IsNullOrEmpty(receiverPhone.Text))
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đơn hàng này không", "Xác nhận đơn hàng", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
                {
                    
                    
                    int score = busOrder.addNewCusOrder(total, discountTotal, od.ToList(), CurrentLogin.Instance.LoginID, isApply? ScoreApply : 0, receiverName.Text, receiverAddress.Text, receiverPhone.Text);
                    if (score > 0)
                    {
                        receiverName.Text = "";
                        receiverAddress.Text = "";
                        receiverPhone.Text = "";
                        bill.ItemsSource = null;
                        od = new ObservableCollection<OrderDetail>();
                        cart.ItemsSource = od;
                        var view = CollectionViewSource.GetDefaultView(cart.ItemsSource);
                        view.Refresh();
                        tabConfirm.Visibility = Visibility.Hidden;
                        tabCart.Visibility = Visibility.Hidden;
                        Panel.SetZIndex(beforeChoose, 1);
                        mainProduct.DataContext = null;
                        MessageBox.Show("Xác nhận đơn hàng thành công bạn, bạn được nhận thêm " + score.ToString() + " điểm tích lũy");
                        txtTotal.Text = "0đ";
                        txtDiscount.Text = "0đ";
                        txtCash.Text = "0đ";
                        total = 0;
                        discountTotal = 0;
                        userOrder.ItemsSource = busOrder.GetOrderHeadersByCustomerID(CurrentLogin.Instance.LoginID);
                        isApply = false;

                        init();
                        initScoreApply();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra");
                    }
                }

            }
            else
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
        }

        private void showOrder(object sender, MouseButtonEventArgs e)
        {
            OrderHeader oh = (sender as Border).DataContext as OrderHeader;
            busOrder = new BUS_Order();
            mainOrder.DataContext = busOrder.GetOrderHeadersByID(oh.OrderHeaderID);
            Panel.SetZIndex(beforeOrderView, 0);
        }


        private void Logout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                Login l = new Login();
                this.Close();
                l.Show();
            }
        }

        private void ApplyScore_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isApply = true;
            discountTotal = ScoreApply;
            txtDiscount.Text = discountTotal.ToString("N0") + "đ";
            txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            ScoreApply_Button.Style = this.FindResource("enable") as Style;
            ScoreApply_Bar.Style = this.FindResource("enable") as Style;
        }
        private void DisapplyScore_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            isApply = false;
            discountTotal = 0;
            txtDiscount.Text = discountTotal.ToString("N0") + "đ";
            txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            ScoreApply_Button.Style = this.FindResource("disable") as Style;
            ScoreApply_Bar.Style = this.FindResource("disable") as Style;
        }

        private void bookProduct_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < menu.Children.Count; i++)
            {
                Border item = menu.Children[i] as Border;
                TextBlock text = item.Child as TextBlock;
                text.Style = this.FindResource("darkFont") as Style;
            }
            Border selectedItem = (Border)this.FindName("btn2");
            TextBlock selectedText = selectedItem.Child as TextBlock;
            selectedText.Style = this.FindResource("greenFont") as Style;

            Grid tab = this.FindName("tab2") as Grid;
            Panel.SetZIndex(selectedTab, 0);
            Panel.SetZIndex(tab, 1);
            selectedTab = tab;

            Button brSelected = sender as Button;
            Product prd = brSelected.DataContext as Product;
            mainProduct.DataContext = prd;

            Panel.SetZIndex(beforeChoose, 0);
            selected = null;
            quantity.Text = 1.ToString();
            priceMainPrd.Text = prd.MinPrice.ToString("N0") + "đ";
        }

        private void cancelOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderHeader o = ((Button)sender).DataContext as OrderHeader;
            if (o.Status == 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn hủy không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
                {
                    
                    if (busOrder.cancelOrder(o.OrderHeaderID))
                    {
                        busOrder = new BUS_Order();
                        MessageBox.Show("Hủy đơn thành công");
                        mainOrder.DataContext = null;
                        Panel.SetZIndex(beforeOrderView, 1);

                        userOrder.ItemsSource = busOrder.GetOrderHeadersByCustomerID(CurrentLogin.Instance.LoginID);
                        init();
                    }
                    else
                    {
                        busOrder = new BUS_Order();
                        MessageBox.Show("Đã có lỗi xảy ra, không thể hủy đơn");

                        mainOrder.DataContext = busOrder.GetOrderHeadersByID(o.OrderHeaderID);
                        Panel.SetZIndex(beforeOrderView, 0);
                        userOrder.ItemsSource = busOrder.GetOrderHeadersByCustomerID(CurrentLogin.Instance.LoginID);
                    }
                }
            } 
            else
            {
                MessageBox.Show("Bạn chỉ được hủy đơn chưa được xác nhận");
            }
        }
    }
}
