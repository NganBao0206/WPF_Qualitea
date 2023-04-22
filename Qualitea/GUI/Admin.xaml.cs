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
using LiveCharts;
using LiveCharts.Wpf;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private BUS_Category busCategory = new BUS_Category();
        private BUS_Product busProduct = new BUS_Product();
        private BUS_Employee busEmployee = new BUS_Employee();
        Border selectedTab;
        public SeriesCollection series { get; set; }
        public Admin()
        {
            InitializeComponent();
            selectedTab = stat;
            initCate();
            loadEmp();
            List<OrderHeader> oh = busOrder.getAllOrders();
            listOrderAll.ItemsSource = oh;
            txtTotal.Text = (busOrder.getTotal(oh) * 1000).ToString("N0") + "đ";
            txtBill.Text = busOrder.getCount(oh).ToString();
            List<Category> categories = busCategory.getCategories();
            categories.Add(new Category() { CategoryID = -1, Name = "Tất cả" });
            comboBoxCategory.ItemsSource = categories;


            for (int year = 2020; year <= DateTime.Now.Year; year++)
            {
                cbYear.Items.Add(year);
            }
            cbYear.SelectedItem = DateTime.Now.Year;
        }


        private void loadChartMonth()
        {
            var onlineTotal = new ChartValues<double>();
            var offlineTotal = new ChartValues<double>();
            var labels = new List<String>();
            List<DateTimeSales> listTotal = busOrder.getSalesInMonth();
            for (int i = 0; i < listTotal.Count; i++) {
                onlineTotal.Add(listTotal[i].OnlineSales);
                offlineTotal.Add(listTotal[i].OfflineSales);
                labels.Add(new DateTime(DateTime.Today.Year, DateTime.Today.Month, listTotal[i].Value).ToString("dd/MM"));
            }

            series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values =  offlineTotal,
                    Title = "Doanh thu offline",
                    Fill = new SolidColorBrush(Color.FromRgb(186,211,115)),
                    LabelPoint = point => point.Y + "K",
                },

                new LineSeries
                {
                    Values = onlineTotal,
                    Title = "Doanh thu online",
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(Color.FromRgb(231,216,123)),
                    LabelPoint = point => point.Y + "K",
                },
            };

            chart1.Series = series;
            
            axisX1.Labels = labels.ToArray();
            axisY1.LabelFormatter = var => var + "K";


            var cateSales = busOrder.getCategorySalesInMonth();

            SeriesCollection series2 = new SeriesCollection
            {
                
            };

            foreach (TypeSales c in cateSales)
            {
                var pieSeries = new PieSeries
                {
                    Title = c.Value,
                    Values = new ChartValues<int>() { c.TotalSales },
                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation),
                };

                series2.Add(pieSeries);

            }

            chart2.Series = series2;

            var prdSales = busOrder.getProductSalesInMonth();

            SeriesCollection series3 = new SeriesCollection
            {
            };

            foreach (TypeSales c in prdSales)
            {
                var pieSeries = new PieSeries
                {
                    Title = c.Value,
                    Values = new ChartValues<int>() { c.TotalSales },
                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation),
                };

                series3.Add(pieSeries);

            }

            chart3.Series = series3;

        }

        private void loadChartDate()
        {
            var onlineTotal = new ChartValues<double>();
            var offlineTotal = new ChartValues<double>();
            var labels = new List<String>();
            List<DateTimeSales> listTotal = busOrder.getSalesInDate();
            for (int i = 0; i < listTotal.Count; i++)
            {
                onlineTotal.Add(listTotal[i].OnlineSales);
                offlineTotal.Add(listTotal[i].OfflineSales);
                labels.Add("Giờ " + listTotal[i].Value);
            }

            series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = offlineTotal,
                    Title = "Doanh thu offline",
                    Fill = new SolidColorBrush(Color.FromRgb(186,211,115)),
                    LabelPoint = point => point.Y + "K",
                },
                new LineSeries
                {
                    Values = onlineTotal,
                    Title = "Doanh thu online",
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(Color.FromRgb(231,216,123)),
                    LabelPoint = point => point.Y + "K",
                },
            };

            chart1.Series = series;

            axisX1.Labels = labels.ToArray();
            axisY1.LabelFormatter = var => var + "K";


            var cateSales = busOrder.getCategorySalesInDate();

            SeriesCollection series2 = new SeriesCollection
            {
            };

            foreach (TypeSales c in cateSales)
            {
                var pieSeries = new PieSeries
                {
                    Title = c.Value,
                    Values = new ChartValues<int>() { c.TotalSales },
                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation),
                };
                if (c.TotalSales != 0)
                    pieSeries.DataLabels = true;

                series2.Add(pieSeries);

            }

            chart2.Series = series2;

            var prdSales = busOrder.getProductSalesInDate();

            SeriesCollection series3 = new SeriesCollection
            {
            };

            foreach (TypeSales c in prdSales)
            {
                var pieSeries = new PieSeries
                {
                    Title = c.Value,
                    Values = new ChartValues<int>() { c.TotalSales },
                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation),
                };
                if (c.TotalSales != 0)
                    pieSeries.DataLabels = true;

                series3.Add(pieSeries);

            }

            chart3.Series = series3;

        }

        private void loadChartYear()
        {

            int year = (int?)cbYear.SelectedItem ?? 0;
            var onlineTotal = new ChartValues<double>();
            var offlineTotal = new ChartValues<double>();
            var labels = new List<String>();
            List<DateTimeSales> listTotal = busOrder.getSalesInYear(year);
            for (int i = 0; i < listTotal.Count; i++)
            {
                onlineTotal.Add(listTotal[i].OnlineSales);
                offlineTotal.Add(listTotal[i].OfflineSales);
                labels.Add("Tháng " + listTotal[i].Value);
            }

            series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = offlineTotal,
                    Title = "Doanh thu offline",
                    Fill = new SolidColorBrush(Color.FromRgb(186,211,115)),
                    LabelPoint = point => point.Y + "K",
                },
                new LineSeries
                {
                    Values = onlineTotal,
                    Title = "Doanh thu online",
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(Color.FromRgb(231,216,123)),
                    LabelPoint = point => point.Y + "K",
                },
            };

            chart1.Series = series;

            axisX1.Labels = labels.ToArray();
            axisY1.LabelFormatter = var => var + "K";


            var cateSales = busOrder.getCategorySalesInYear(year);

            SeriesCollection series2 = new SeriesCollection
            {
            };

            foreach (TypeSales c in cateSales)
            {
                var pieSeries = new PieSeries
                {
                    Title = c.Value,
                    Values = new ChartValues<int>() { c.TotalSales },
                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation),
                };
                if (c.TotalSales != 0)
                    pieSeries.DataLabels = true;

                series2.Add(pieSeries);

            }

            chart2.Series = series2;

            var prdSales = busOrder.getProductSalesInYear(year);

            SeriesCollection series3 = new SeriesCollection
            {
            };

            foreach (TypeSales c in prdSales)
            {
                var pieSeries = new PieSeries
                {
                    Title = c.Value,
                    Values = new ChartValues<int>() { c.TotalSales },
                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation),
                };
                if (c.TotalSales != 0)
                    pieSeries.DataLabels = true;

                series3.Add(pieSeries);

            }

            chart3.Series = series3;

        }

        private void initCate()
        {
            categoryList.ItemsSource = busCategory.getCategories();
            loadPrd();
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
        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void addNewCategory(object sender, RoutedEventArgs e)
        {
            AdminAddCategory cw = new AdminAddCategory();
            cw.ShowInTaskbar = false;
            cw.Owner = this;
            cw.ShowDialog();
            //comboBoxCategory.ItemsSource = BUS_Category.getCategories();
        }

        private void delCates(object sender, RoutedEventArgs e)
        {
            var temp = categoryList.SelectedItems;
            List<Category> cates = new List<Category>();
            foreach (var t in temp)
            {
                cates.Add((Category)t);
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                MessageBox.Show(busCategory.delCategories(cates));
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
            ew.Owner = this;
            ew.ShowDialog();
        }

        private void search(object sender, RoutedEventArgs e)
        {
            categoryList.ItemsSource = busCategory.getCategories(filterName.Text);
        }

        private void filterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                categoryList.ItemsSource = busCategory.getCategories(filterName.Text);
        }

        private void LogOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                Login l = new Login();
                this.Close();
                l.Show();
            }
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
                double temp;
                double? priceMin = null;
                double? priceMax = null;
                bool success = double.TryParse(minPrice.Text, out temp);
                priceMin = success ? temp / 1000 : priceMin;
                success = double.TryParse(maxPrice.Text, out temp);
                priceMax = success ? temp / 1000 : priceMax;
                listProduct.ItemsSource = busProduct.getProducts(searchBoxProduct.Text, (int?)comboBoxCategory.SelectedValue, isAct, priceMin, priceMax);
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
                        cw.product = busProduct.getProduct(pd);
                        cw.ShowInTaskbar = false;
                        cw.Owner = this;
                        cw.ShowDialog();
                        busProduct = new BUS_Product();
                        loadPrd();
                        return;
                    }
                }
            }
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {

            AdminAddProduct cw = new AdminAddProduct();
            cw.ShowInTaskbar = false;
            cw.Owner = this;
            cw.ShowDialog();
            busProduct = new BUS_Product();
            loadPrd();
        }

        public void loadEmp()
        {
            if (employeeList != null)
            {
                employeeList.ItemsSource = busEmployee.getEmployees(searchEmployee.Text, searchDob.SelectedDate, searchStartDate.SelectedDate, (bool)ckboxIsEmployeed.IsChecked, (bool)ckboxIsUnemployeed.IsChecked);
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

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            AdminAddEmployee ew = new AdminAddEmployee();
            ew.ShowInTaskbar = false;
            ew.Owner = this;
            ew.ShowDialog();
        }

        private void EmployeeDetail_Click(object sender, MouseButtonEventArgs e)
        {
            Employee emp = (sender as Border).DataContext as Employee;
            AdminAddEmployee ew = new AdminAddEmployee();
            ew.employee = emp;
            ew.ShowInTaskbar = false;
            ew.Owner = this;
            ew.ShowDialog();
            updateEmployee();
        }

        public void updateEmployee()
        {
            busEmployee = new BUS_Employee();
            loadEmp();
        }


        private BUS_Order busOrder = new BUS_Order();
        private int select = 1;
        private void btnAll_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnMonth.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            btnToday.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            btnAll.Background = new SolidColorBrush(Color.FromRgb(186, 211, 115));
            btnAll.Width = 48;
            btnMonth.Width = 40;
            btnToday.Width = 40;

            btnMonth.Height = 150;
            btnToday.Height = 150;
            btnAll.Height = 170;

            txtAll.FontSize = 16;
            txtMonth.FontSize = 14;
            txtToday.FontSize = 14;

            txtAll.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            txtMonth.Foreground = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            txtToday.Foreground = new SolidColorBrush(Color.FromRgb(209, 209, 209));


            radAll.IsChecked = true;

            dateSelector.Visibility = Visibility.Visible;
            minDate.SelectedDate = null;
            maxDate.SelectedDate = null;
            YearSelector.Visibility = Visibility.Visible;
            select = 1;
            if (isChartShowing)
                loadChartYear();
        }

        private void btnMonth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnAll.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            btnToday.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            btnMonth.Background = new SolidColorBrush(Color.FromRgb(186, 211, 115));
            btnAll.Width = 40;
            btnMonth.Width = 48;
            btnToday.Width = 40;

            btnMonth.Height = 170;
            btnToday.Height = 150;
            btnAll.Height = 150;

            txtAll.FontSize = 14;
            txtMonth.FontSize = 16;
            txtToday.FontSize = 14;

            txtAll.Foreground = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            txtMonth.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            txtToday.Foreground = new SolidColorBrush(Color.FromRgb(209, 209, 209));

            radAll.IsChecked = true;
            dateSelector.Visibility = Visibility.Collapsed;
            minDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            maxDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            YearSelector.Visibility = Visibility.Collapsed;
            select = 2;
            if (isChartShowing)
                loadChartMonth();
        }

        private void btnToday_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnMonth.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            btnAll.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            btnToday.Background = new SolidColorBrush(Color.FromRgb(186, 211, 115));
            btnAll.Width = 40;
            btnMonth.Width = 40;
            btnToday.Width = 48;

            btnMonth.Height = 150;
            btnToday.Height = 170;
            btnAll.Height = 150;

            txtAll.FontSize = 14;
            txtMonth.FontSize = 14;
            txtToday.FontSize = 16;

            txtAll.Foreground = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            txtMonth.Foreground = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            txtToday.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));

            radAll.IsChecked = true;
            dateSelector.Visibility = Visibility.Collapsed;
            minDate.SelectedDate = DateTime.Now;
            maxDate.SelectedDate = DateTime.Now;
            YearSelector.Visibility = Visibility.Collapsed;
            select = 3;
            if (isChartShowing)
                loadChartDate();
        }

        private void loadOrderByFiller()
        {
            if (listOrderAll != null)
            {
                string kw = searchOrderBox.Text;
                DateTime? minD = minDate.SelectedDate;
                DateTime? maxD = maxDate.SelectedDate;
                int? status;
                if (radComplete.IsChecked == true)
                    status = 2;
                else if (radInProcess.IsChecked == true)
                    status = 1;
                else if (radNotAccept.IsChecked == true)
                    status = 0;
                else
                    status = null;
                List<OrderHeader> oh = busOrder.getOrdersFilter(kw, minD, maxD, status);
                listOrderAll.ItemsSource = oh;
                txtTotal.Text = (busOrder.getTotal(oh) * 1000).ToString("N0") + "đ";
                txtBill.Text = busOrder.getCount(oh).ToString();
            }

        }

        private void date_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            loadOrderByFiller();
        }

        private void searchOrderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadOrderByFiller();
        }

        private void radAll_Checked(object sender, RoutedEventArgs e)
        {
            loadOrderByFiller();
        }

        private void radComplete_Checked(object sender, RoutedEventArgs e)
        {
            loadOrderByFiller();
        }

        private void radInProcess_Checked(object sender, RoutedEventArgs e)
        {
            loadOrderByFiller();
        }

        private void radNotAccept_Checked(object sender, RoutedEventArgs e)
        {
            loadOrderByFiller();
        }

        private void cbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadChartYear();
        }

        private bool isChartShowing = false;
        private void btnShowChart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isChartShowing)
            {
                tabChart.Visibility = Visibility.Visible;
                isChartShowing = true;
                switch(select)
                {
                    case 1:
                        loadChartYear();
                        break;
                    case 2:
                        loadChartMonth();
                        break;
                    case 3:
                        loadChartDate();
                        break;
                    default:
                        break;
                }
                txtShowChart.Text = "Xem thống kê";
            }
            else
            {
                tabChart.Visibility = Visibility.Collapsed;
                isChartShowing = false;
                txtShowChart.Text = "Xem hóa đơn";
            } 
                
        }
    }
}
