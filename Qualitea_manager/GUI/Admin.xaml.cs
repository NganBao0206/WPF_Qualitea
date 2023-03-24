using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUS;
using DTO;

namespace Qualitea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            this.DataContext = this;
            selectedTab = stat;
            initCate();
            var view = (ListCollectionView)CollectionViewSource.GetDefaultView(categoryList.ItemsSource);

            // Adding a SortDescription to our view
            view.SortDescriptions.Add(new SortDescription("CategoryID", ListSortDirection.Ascending));
            listProduct.ItemsSource = BUS_Product.getProducts();
            comboBoxCategory.ItemsSource = BUS_Category.getCategories();
            voucherList.ItemsSource = BUS_Coupon.GetCoupons();
            employeeList.ItemsSource = BUS_Login.getLoginEmployees();
        }
        Border selectedTab;
        private void initCate()
        {
            categoryList.ItemsSource = BUS_Category.getCategories();
        }
        
        private void menuitem_click(object sender, MouseButtonEventArgs e)
        {
            Border bd = (Border)sender;
            selectedTab.Style = this.FindResource("unselectedBtn") as Style;
            
            string untabName = "Tab" + menu.Children.IndexOf(selectedTab).ToString();
            Border untab = (Border)this.FindName(untabName);
            Panel.SetZIndex(untab, 0);


            bd.Style = (Style)this.FindResource("selectedBtn") as Style;
            selectedTab = bd;
            string tabName = "Tab" + menu.Children.IndexOf(selectedTab).ToString();
            Border tab = (Border)this.FindName(tabName);
            Panel.SetZIndex(tab, 1);
            Console.WriteLine(menu.Children.IndexOf(selectedTab));
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

        private void addNewCategory(object sender, RoutedEventArgs e)
        {
            AdminAddCategory cw = new AdminAddCategory();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.ShowDialog();
        }

        private void delCates(object sender, RoutedEventArgs e)
        {
            var temp = categoryList.SelectedItems ;
            List<Category> cates = new List<Category>(); 
            foreach(var t in temp)
            {
                cates.Add((Category)t);
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                MessageBox.Show(BUS_Category.delCategories(cates));
                initCate();
            }
        }

        private void editCate(object sender, MouseButtonEventArgs e)
        {
            var c = categoryList.SelectedItem;
            var cate = (Category)c;
            AdminEditCategory.cate = cate;
            AdminEditCategory ew = new AdminEditCategory();
            ew.ShowInTaskbar = false;
            ew.Owner = Application.Current.MainWindow;
            ew.ShowDialog();
        }

        private void search(object sender, RoutedEventArgs e)
        {
            categoryList.ItemsSource = BUS_Category.getCategories(filterName.Text);
        }

        private void filterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                categoryList.ItemsSource = BUS_Category.getCategories(filterName.Text);
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            
            AdminAddProduct cw = new AdminAddProduct();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.ShowDialog();
            loadPrd();
        }

        private void editPrd(object sender, MouseButtonEventArgs e)
        {
            Border obj = (Border)sender;
            var par = obj.Parent;
            while (par != null)
            {
                par = VisualTreeHelper.GetParent(par);
                if (par is ContentPresenter p)
                {
                    if (p.DataContext is Product pd)
                    {
                        AdminProductDetail cw = new AdminProductDetail();
                        cw.product = pd;
                        cw.ShowInTaskbar = false;
                        cw.Owner = Application.Current.MainWindow;
                        cw.ShowDialog();
                        loadPrd();
                        return;
                    }
                }
            }    
        }
        public void restartTabPrd()
        {
            searchBoxProduct.Text = "";
            comboBoxCategory.SelectedValue = null;
            rdoAllPrd.IsChecked = true;
        }
        public void loadPrd()
        {
            bool? isAct;
            if (listProduct != null)
            {
                if (rdoActivePrd.IsChecked == true)
                    isAct = true;
                else if (rdoAllPrd.IsChecked == true)
                    isAct = null;
                else
                    isAct = false;
                decimal temp;
                decimal? priceMin = null;
                decimal? priceMax = null;
                bool success = decimal.TryParse(minPrice.Text, out temp);
                priceMin = success ? temp / 1000 : priceMin;
                success = decimal.TryParse(maxPrice.Text, out temp);
                priceMax = success ? temp / 1000 : priceMax;
                listProduct.ItemsSource = BUS_Product.getProducts(searchBoxProduct.Text, (int?)comboBoxCategory.SelectedValue, isAct, priceMin, priceMax);
            }
        }

        public void loadVoucher()
        {
            if (listProduct != null)
            {
                decimal? _minPer = null;
                decimal? _maxPer = null;
                int? _minAmount = null;
                int? _maxAmount = null;
                int? _minPoint = null;
                int? _maxPoint = null;

                decimal temp;

                bool success = decimal.TryParse(minPer.Text, out temp);
                _minPer = success ? temp / 100 : _minPer;

                success = decimal.TryParse(maxPer.Text, out temp);
                _maxPer = success ? temp / 100 : _maxPer;

                int temp2;
                success = int.TryParse(minAmount.Text, out temp2);
                _minAmount = success ? temp2 : _minAmount;

                success = int.TryParse(maxAmount.Text, out temp2);
                _maxAmount = success ? temp2 : _maxAmount;

                success = int.TryParse(minPoint.Text, out temp2);
                _minPoint = success ? temp2 : _minPoint;

                success = int.TryParse(maxPoint.Text, out temp2);
                _maxPoint = success ? temp2 : _maxPoint;
                voucherList.ItemsSource = BUS_Coupon.GetCoupons(searchNameVoucher.Text, _minPer, _maxPer, _minAmount, _maxAmount, _maxPoint, _maxPoint, minDate.SelectedDate, maxDate.SelectedDate);
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

        private void editVoucher(object sender, MouseButtonEventArgs e)
        {
            Coupon item = (sender as Border).DataContext as Coupon;
            //AdminEditVoucher.voucher = item;

            AdminEditVoucher vw = new AdminEditVoucher();
            vw.voucher = item;
            vw.ShowInTaskbar = false;
            vw.Owner = Application.Current.MainWindow;
            vw.ShowDialog();
        }

        private void addVoucher_Click(object sender, RoutedEventArgs e)
        {
            AdminAddVoucher vw = new AdminAddVoucher();
            vw.ShowInTaskbar = false;
            vw.Owner = Application.Current.MainWindow;
            vw.ShowDialog();
        }

        private void txtVoucher_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadVoucher();
        }

        private void dateVoucher_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            loadVoucher();
        }

        private void delVoucher(object sender, MouseButtonEventArgs e)
        {
            Coupon item = (sender as Border).DataContext as Coupon;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xóa sản phẩm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (BUS_Coupon.DelCoupon(item))
                {
                    loadVoucher();
                    MessageBox.Show("Xóa thành công");
                }
                else
                    MessageBox.Show("Không thể xóa, bạn chỉ có thể xóa voucher chưa được người dùng quy đổi");
            }
            
        }

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            AdminAddEmployee ew = new AdminAddEmployee();
            ew.ShowInTaskbar = false;
            ew.Owner = Application.Current.MainWindow;
            ew.ShowDialog();
        }

        private void EmployeeDetail_Click(object sender, MouseButtonEventArgs e)
        {
            DTO.Login emp = (sender as Border).DataContext as DTO.Login;
            AdminAddEmployee ew = new AdminAddEmployee();
            ew.employee = emp;
            ew.ShowInTaskbar = false;
            ew.Owner = Application.Current.MainWindow;
            ew.ShowDialog();
        }

        public void updateEmployee()
        {
            employeeList.ItemsSource = BUS_Login.getLoginEmployees();
        }

        public void loadEmp()
        {
            if (employeeList != null)
            {
                employeeList.ItemsSource = BUS_Login.getLoginEmployees(searchEmployee.Text, searchDob.SelectedDate, searchStartDate.SelectedDate, (bool)ckboxIsEmployeed.IsChecked, (bool)ckboxIsUnemployeed.IsChecked);
            }
        }

        private void searchEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadEmp();
        }

        private void ckboxEmployeed_CheckChanged(object sender, RoutedEventArgs e)
        {
            loadEmp();
        }

        private void searchEmployee_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadEmp();
        }
    }
}
