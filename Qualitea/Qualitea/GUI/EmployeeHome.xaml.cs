using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using BUS;

namespace GUI
{
    /// <summary>
    /// Interaction logic for EmployeeHome.xaml
    /// </summary>
    public partial class EmployeeHome : Window, INotifyPropertyChanged
    {
        private string _currenttime;

        private BUS_Employee busEmployee = new BUS_Employee();
        private BUS_Order busOrder = new BUS_Order();
        public EmployeeHome()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                UpdateTime();
            };
            user.DataContext = busEmployee.getEmployeeByID(CurrentLogin.Instance.LoginID);
            amountOrders.Text = busOrder.countOrderEmployeeToday(CurrentLogin.Instance.LoginID) + " ĐƠN";
            totalOrders.Text = (busOrder.TotalOrderEmployeeToday(CurrentLogin.Instance.LoginID)*1000).ToString("N0") + "đ";
            this.DataContext = this;
        }

        public string CurrentTime
        {
            get { return _currenttime; }
            set { _currenttime = value; OnPropertyChanged("CurrentTime"); }
        }

        private void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToLongTimeString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            EmployeeOrder eo = new EmployeeOrder();
            eo.ShowInTaskbar = false;
            eo.Owner = this;
            eo.ShowDialog();
            amountOrders.Text = busOrder.countOrderEmployeeToday(CurrentLogin.Instance.LoginID) + " ĐƠN";
            totalOrders.Text = (busOrder.TotalOrderEmployeeToday(CurrentLogin.Instance.LoginID) * 1000).ToString("N0") + "đ";

        }

        private void BtnShowBill_Click(object sender, RoutedEventArgs e)
        {
            EmployeeCheckBill ec = new EmployeeCheckBill();
            ec.ShowInTaskbar = false;
            ec.Owner = this;
            ec.ShowDialog();
            amountOrders.Text = busOrder.countOrderEmployeeToday(CurrentLogin.Instance.LoginID) + " ĐƠN";
            totalOrders.Text = (busOrder.TotalOrderEmployeeToday(CurrentLogin.Instance.LoginID) * 1000).ToString("N0") + "đ";
        }

       


        private void LogOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                Login l= new Login();
                this.Close();
                l.Show();
            }
        }

    }
}
