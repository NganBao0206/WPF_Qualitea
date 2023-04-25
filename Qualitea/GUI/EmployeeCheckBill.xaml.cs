using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using BUS;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for EmployeeCheckBill.xaml
    /// </summary>
    public partial class EmployeeCheckBill : Window
    {
        private BUS_Order busOrder = new BUS_Order();
        private bool isOnline = false;
        private int status = 1;
        public EmployeeCheckBill()
        {
            InitializeComponent();
            
            listOrder.ItemsSource = busOrder.getEmployeeOrders(CurrentLogin.Instance.LoginID, null, isOnline, status);
            List<OrderHeader> unConfirmedOrders = busOrder.getUnconfimredOrders();
            listOrderByCustomer.ItemsSource = unConfirmedOrders;
            if (unConfirmedOrders.Count > 0)
                ShowAnnounce.Visibility = Visibility.Visible;
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                busOrder = new BUS_Order();
                unConfirmedOrders = busOrder.getUnconfimredOrders();
                listOrderByCustomer.ItemsSource = unConfirmedOrders;
                if (unConfirmedOrders.Count > 0)
                    ShowAnnounce.Visibility = Visibility.Visible;
                else
                {
                    ShowAnnounce.Visibility = Visibility.Collapsed;
                }
            };

        }

        private void searchOrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadOrder();
        }

        public void loadOrder()
        { 
            if (listOrder != null)
            {
                busOrder = new BUS_Order();
                listOrder.ItemsSource = busOrder.getEmployeeOrders(CurrentLogin.Instance.LoginID, searchOrderDate.SelectedDate, isOnline, status);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNotice_Click(object sender, RoutedEventArgs e)
        {
            //ShowAnnounce.Visibility = Visibility.Collapsed;
            ShowCusOrder.Visibility = Visibility.Collapsed;
        }

        private void btnBell_Click(object sender, RoutedEventArgs e)
        {
            //ShowAnnounce.Visibility = Visibility.Visible;
            ShowCusOrder.Visibility = Visibility.Visible;
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            Button complete = (Button)sender;
            OrderHeader oh = complete.DataContext as OrderHeader;
            if (oh.Status == 2)
            {
                MessageBox.Show("Đơn hàng này đã được xác nhận hoàn thành");
                return;
            }
            if (busOrder.ChangeStatus(oh, 2))
            {
                loadOrder();
                listOrderByCustomer.ItemsSource = busOrder.getUnconfimredOrders();
            }
            else
                MessageBox.Show("Đã có lỗi xảy ra");
        }

        private void tabOffline_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isOnline = false;
            tabOffline.Background = new SolidColorBrush(Color.FromRgb(83, 130, 94));
            tabOffline.Width = 45;
            tabOffline.Height = 160;

            tabOnline.Background = new SolidColorBrush(Color.FromRgb(172, 172, 172));
            tabOnline.Width = 40;
            tabOnline.Height = 150;

            loadOrder();
        }

        private void tabOnline_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isOnline = true;

            tabOnline.Background = new SolidColorBrush(Color.FromRgb(83, 130, 94));
            tabOnline.Width = 45;
            tabOnline.Height = 160;

            tabOffline.Background = new SolidColorBrush(Color.FromRgb(172, 172, 172));
            tabOffline.Width = 40;
            tabOffline.Height = 150;

            loadOrder();
        }

        private void rdoNotFinish_Checked(object sender, RoutedEventArgs e)
        {
            status = 1;
            loadOrder();
        }

        private void rdoFinish_Checked(object sender, RoutedEventArgs e)
        {
            status = 2;
            loadOrder();
        }

        private void confirmOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đơn hàng này không", "Xác nhận đơn hàng", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                Button btnConfirm = (Button)sender;
                OrderHeader oh = btnConfirm.DataContext as OrderHeader;
                if (busOrder.confirmOrder(oh, CurrentLogin.Instance.LoginID))
                {
                    loadOrder();
                    listOrderByCustomer.ItemsSource = busOrder.getUnconfimredOrders();
                }
                else
                    MessageBox.Show("Đã có lỗi xảy ra");
            }
        }
    }
}
