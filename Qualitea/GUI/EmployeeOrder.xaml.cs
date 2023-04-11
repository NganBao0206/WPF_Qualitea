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
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    /// 
    public partial class EmployeeOrder : Window
    {
        public ObservableCollection<OrderDetail> od = new ObservableCollection<OrderDetail>();
        private ProductOption poSelect;
        private Border selected;
        private double total;
        private double discountTotal = 0;
        private BUS_Product busProduct = new BUS_Product();
        private BUS_Category busCategory = new BUS_Category();
        private BUS_Order busOrder = new BUS_Order();
        private BUS_Customer busCustomer = new BUS_Customer();
        bool isApply = false;
        int ScoreApply = 0;
        private DTO.Customer customer;
        public EmployeeOrder()
        {
            InitializeComponent();
            listProduct.ItemsSource = busProduct.getProducts(true);
            List<Category> categories = busCategory.getCategories();
            categories.Add(new Category() { CategoryID = -1, Name = "Tất cả" });
            comboBoxCategory.ItemsSource = categories;
            total = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void restartTabPrd()
        {
            searchBoxProduct.Text = "";
            comboBoxCategory.SelectedValue = null;
        }
        public void loadPrd()
        {
            if (listProduct != null)
            {
                double temp;
                double? priceMin = null;
                double? priceMax = null;
                bool success = double.TryParse(minPrice.Text, out temp);
                priceMin = success ? temp / 1000 : priceMin;
                success = double.TryParse(maxPrice.Text, out temp);
                priceMax = success ? temp / 1000 : priceMax;
                listProduct.ItemsSource = busProduct.getProducts(searchBoxProduct.Text, (int?)comboBoxCategory.SelectedValue, true, priceMin, priceMax);
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            loadPrd();
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

        private void RadioSize_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OK");
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

            TextBlock priceTextBlocks = FindVisualChildren<TextBlock>(listProduct).Where(x => x.Name == "PriceText" && (x.DataContext as Product).ProductID == newPo.ProductID).FirstOrDefault();

            priceTextBlocks.Text = newPo._price.ToString("N0") + "đ";
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

        private void cartButtonClick(object sender, RoutedEventArgs e)
        {

            if (selected != null)
            {
                
                bool flag = false;
                foreach (OrderDetail o in od)
                {
                    if (o.ProductOptionID == poSelect.ProductOptionID)
                    {
                        o.Quantity += 1;
                        o.TotalLine = o.Quantity * o.ProductOption.Price;
                        total += o.ProductOption._price;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    OrderDetail newOD = new OrderDetail();
                    newOD.ProductOption = poSelect;
                    newOD.ProductOptionID = poSelect.ProductOptionID;
                    newOD.Quantity = 1;
                    newOD.TotalLine = newOD.Quantity * newOD.ProductOption.Price;
                    total += newOD.ProductOption._price;
                    od.Add(newOD);
                }
                
                Bill.ItemsSource = od;
                txtTotal.Text = total.ToString("N0") + "đ";
                txtCash.Text = txtTotal.Text;
                var view = CollectionViewSource.GetDefaultView(Bill.ItemsSource);
                view.Refresh();
            }
            
        }


        private void Delete(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                Border del = sender as Border;
                OrderDetail delOD = del.DataContext as OrderDetail;
                od.Remove(delOD);
                selected.Background = color;
                txtTotal.Text = (total - delOD._totalLine).ToString("N0") + "đ";
                txtCash.Text = txtTotal.Text;

            }
        }


        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            Button minus = sender as Button;
            OrderDetail minusOD = minus.DataContext as OrderDetail;
            minusOD.Quantity -= 1;
            if (minusOD.Quantity == 0)
            {
                od.Remove(minusOD);
                selected.Background = color;
            }
            minusOD.TotalLine = minusOD.Quantity * minusOD.ProductOption.Price;
            total -= minusOD.ProductOption._price;
            txtTotal.Text = total.ToString("N0") + "đ";
            txtCash.Text = txtTotal.Text;
            Bill.ItemsSource = od;
            var view = CollectionViewSource.GetDefaultView(Bill.ItemsSource);
            view.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button add = sender as Button;
            OrderDetail addOD = add.DataContext as OrderDetail;
            addOD.Quantity += 1;
            addOD.TotalLine = addOD.Quantity * addOD.ProductOption.Price;
            total += addOD.ProductOption._price;
            txtTotal.Text = total.ToString("N0") + "đ";
            txtCash.Text = txtTotal.Text;
            Bill.ItemsSource = od;
            var view = CollectionViewSource.GetDefaultView(Bill.ItemsSource);
            view.Refresh();
        }

        
        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn đã thanh toán không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                if (busOrder.addNewEmpOrder(total/1000, discountTotal/1000, od.ToList(), customer.CustomerID, ScoreApply ,CurrentLogin.Instance.LoginID))
                {
                    od.Clear();
                    Bill.ItemsSource = od;
                    var view = CollectionViewSource.GetDefaultView(Bill.ItemsSource);
                    view.Refresh();
                    MessageBox.Show("Thanh toán thành công");
                    isApply = false;
                    infoCustomer.DataContext = null;
                    customer = null;
                    infoCustomer.Visibility = Visibility.Hidden;
                    ScoreApply_Button.Style = this.FindResource("disable") as Style;
                    ScoreApply_Bar.Style = this.FindResource("disable") as Style;
                    ScoreApply = 0;
                    total = 0;
                    discountTotal = 0;
                    txtTotal.Text = total.ToString("N0") + "đ";
                    txtDiscount.Text = discountTotal.ToString("N0") + "đ";
                    txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
                    idCustomer.Text = "";
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (od.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc là hủy đơn hàng này không?", "Lưu ý", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    od.Clear();
                    Bill.ItemsSource = od;
                    var view = CollectionViewSource.GetDefaultView(Bill.ItemsSource);
                    view.Refresh();
                    MessageBox.Show("Hủy đơn thành công","Thành công",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
            else
                MessageBox.Show("Bạn không thể hủy đơn khi không có sẵn phẩm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }

        private void ApplyScore_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (customer.Score <= 0)
            {
                MessageBox.Show("Khách hàng không có điểm tích lũy");
                return;
            }    
            if (od.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng trước");
                return;
            }
            isApply = true;
            ScoreApply = customer.Score;
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

        private void btnCheckCustomer_Click(object sender, RoutedEventArgs e)
        {
            
            if (String.IsNullOrEmpty(idCustomer.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng");
                return;
            }
            int id = -1;
            int.TryParse(idCustomer.Text, out id);
            customer = busCustomer.getCustomerByID(id);
            if (customer == null)
            {
                MessageBox.Show("Mã khách hàng không đúng");
                return;
            }

            infoCustomer.DataContext = customer;
            infoCustomer.Visibility = Visibility.Visible;
            
        }

       

        private void cancelInfoCustomer(object sender, RoutedEventArgs e)
        {
            infoCustomer.DataContext = null;
            customer = null;
            infoCustomer.Visibility = Visibility.Hidden;
            isApply = false;
            ScoreApply_Button.Style = this.FindResource("disable") as Style;
            ScoreApply_Bar.Style = this.FindResource("disable") as Style;
            ScoreApply = 0;
            discountTotal = 0;
            txtDiscount.Text = discountTotal.ToString("N0") + "đ";
            txtCash.Text = (total - discountTotal).ToString("N0") + "đ";
        }
    }
}
