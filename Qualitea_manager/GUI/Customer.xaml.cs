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

namespace Qualitea
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        public ObservableCollection<OrderDetail> od = new ObservableCollection<OrderDetail>();
        private ProductOption poSelect;
        private Border selected;
        private decimal total;
        private decimal discountTotal;
        private Grid selectedTab;
        public Customer()
        {
            InitializeComponent();
            listProduct.ItemsSource = BUS_Product.getProducts(true);
            comboBoxCategory.ItemsSource = BUS_Category.getCategories();
            listVoucher.ItemsSource = BUS_Coupon.GetCurrentCoupons("", null, null, null, null);
            userCoupon.ItemsSource = BUS_CouponRedemption.GetCouponRedemptions(CurrentLogin.Instance.Login);
            userCoupon.DisplayMemberPath = "Coupon.info";
            userCoupon.SelectedValuePath = "CouponRedemptionID";
            userOrder.ItemsSource = BUS_Order.GetOrders(CurrentLogin.Instance.Login);
            total = 0;
            discountTotal = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectedTab = tab1;
            userPoint.DataContext = CurrentLogin.Instance.Login;
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
                listProduct.ItemsSource = BUS_Product.getProducts(searchBoxProduct.Text, (int?)comboBoxCategory.SelectedValue, true, 0, 9999999999);
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
            imgMainPrd.DataContext = prd;
            nameMainPrd.DataContext = prd;
            priceMainPrd.DataContext = prd;
            optionMainPrd.ItemsSource = prd.ProductOptions;
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
                        total += o.ProductOption._price* int.Parse(quantity.Text);
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
                    total += newOD.ProductOption._price* int.Parse(quantity.Text);
                    od.Add(newOD);
                }
                MessageBox.Show("Thêm vào giỏ thành công");
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

        private void ShowDetailVoucher(object sender, MouseButtonEventArgs e)
        {
            Border temp = sender as Border;
            Coupon voucher = temp.DataContext as Coupon;
            Panel.SetZIndex(beforeChooseVoucher, 0);
            mainVoucher.DataContext = voucher;
        }

        public void loadVoucher()
        {
            if (listProduct != null)
            {
                decimal? _minPer = null;
                decimal? _maxPer = null;
                int? _minPoint = null;
                int? _maxPoint = null;

                decimal temp;

                bool success = decimal.TryParse(minPer.Text, out temp);
                _minPer = success ? temp / 100 : _minPer;

                success = decimal.TryParse(maxPer.Text, out temp);
                _maxPer = success ? temp / 100 : _maxPer;

                int temp2;

                success = int.TryParse(minPoint.Text, out temp2);
                _minPoint = success ? temp2 : _minPoint;

                success = int.TryParse(maxPoint.Text, out temp2);
                _maxPoint = success ? temp2 : _maxPoint;
                listVoucher.ItemsSource = BUS_Coupon.GetCurrentCoupons(searchNameVoucher.Text, _minPer, _maxPer, _minPoint, _maxPoint);
            }
        }

        private void txtVoucher_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadVoucher();
        }

        private void redeem_Click(object sender, RoutedEventArgs e)
        {
            Button redeen = (Button)sender;
            Coupon coupon = redeem.DataContext as Coupon;
            if (CurrentLogin.Instance.Login.Score > coupon.RedemptionPoints)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn đổi voucher này không?", "Đổi voucher", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    if (BUS_CouponRedemption.addCouponRedemption(coupon, CurrentLogin.Instance.Login, DateTime.Now))
                    {
                        userPoint.Text = (int.Parse(userPoint.Text) - coupon.RedemptionPoints).ToString();
                        listVoucher.ItemsSource = BUS_Coupon.GetCurrentCoupons("", null, null, null, null);
                        Panel.SetZIndex(beforeChooseVoucher, 1);
                        MessageBox.Show("Đổi voucher thành công");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra");
                    }    
            } 
            else
                MessageBox.Show("Bạn không đủ điểm");
        }

        private void userCoupon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userCoupon.SelectedItem == null)
            {
                discountTotal = 0;
                txtDiscount.Text = "0đ";
                txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            }
            else
            {
                CouponRedemption cp = userCoupon.SelectedItem as CouponRedemption;
                decimal per = cp.Coupon.PercentageDiscount;
                decimal maxDiscount = cp.Coupon._MaxDiscount != null ? (decimal)cp.Coupon._MaxDiscount : 0;
                discountTotal = total * per < maxDiscount ? total * per : maxDiscount;
                txtDiscount.Text = discountTotal.ToString("N0") + "đ";
                txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
            }
        }

        private void backToCart(object sender, MouseButtonEventArgs e)
        {
            tabConfirm.Visibility = Visibility.Hidden;
        }

        private void continuePay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bill.ItemsSource = cart.ItemsSource;
            tabConfirm.Visibility = Visibility.Visible;
            billTotal.Text = txtTotal.Text;
            billDiscount.Text = txtDiscount.Text;
            billCash.Text = txtCash.Text;
        }

        private void confirmBill(object sender, MouseButtonEventArgs e)
        {
            if (!String.IsNullOrEmpty(receiverName.Text) && !String.IsNullOrEmpty(receiverAddress.Text) && !String.IsNullOrEmpty(receiverPhone.Text))
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đơn hàng này không", "Xác nhận đơn hàng", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
                {
                    int score = BUS_Order.addNewCusOrder(DateTime.Now, total, discountTotal, od.ToList(), CurrentLogin.Instance.Login, (CouponRedemption)userCoupon.SelectedItem, receiverName.Text, receiverAddress.Text, receiverPhone.Text);
                    if (score > 0)
                    {
                        userPoint.Text = (int.Parse(userPoint.Text) + score).ToString();
                        userCoupon.ItemsSource = BUS_CouponRedemption.GetCouponRedemptions(CurrentLogin.Instance.Login);
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
                        MessageBox.Show("Xác nhận đơn hàng thành công bạn, bạn được nhận thêm " + score.ToString() + " điểm tích lũy");
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
            mainOrder.DataContext = (sender as Border).DataContext as OrderHeader;
        }
    }
}
