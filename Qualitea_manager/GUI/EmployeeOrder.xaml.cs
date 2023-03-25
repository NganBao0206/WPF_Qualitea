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

namespace Qualitea
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class EmployeeOrder : Window
    {
        public ObservableCollection<OrderDetail> od = new ObservableCollection<OrderDetail>();
        private ProductOption poSelect;
        private Border selected;
        private decimal total;
        private decimal discountTotal = 0;
        public EmployeeOrder()
        {
            InitializeComponent();
            listProduct.ItemsSource = BUS_Product.getProducts(true);
            comboBoxCategory.ItemsSource = BUS_Category.getCategories();
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
                decimal temp;
                decimal? priceMin = null;
                decimal? priceMax = null;
                bool success = decimal.TryParse(minPrice.Text, out temp);
                priceMin = success ? temp / 1000 : priceMin;
                success = decimal.TryParse(maxPrice.Text, out temp);
                priceMax = success ? temp / 1000 : priceMax;
                listProduct.ItemsSource = BUS_Product.getProducts(searchBoxProduct.Text, (int?)comboBoxCategory.SelectedValue, true, priceMin, priceMax);
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
            MessageBox.Show("   OK");
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
                if (BUS_Order.addNewOrder(DateTime.Now, total/1000, discountTotal/1000, od.ToList(), CurrentLogin.Instance.Login.LoginID))
                {
                    od.Clear();
                    Bill.ItemsSource = od;
                    var view = CollectionViewSource.GetDefaultView(Bill.ItemsSource);
                    view.Refresh();
                    MessageBox.Show("Thanh toán thành công");
                }
            }
        }
    }
}
