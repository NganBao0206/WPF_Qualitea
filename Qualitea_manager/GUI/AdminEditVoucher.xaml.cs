using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    public partial class AdminEditVoucher : Window
    {
        public int Num { get; set; }
        public string date { get; set; }

        public Coupon voucher { get; set; }
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(67,73,72));
        public AdminEditVoucher()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Text = voucher.CouponID.ToString();
            txtName.Text = voucher.Name;
            txtPercentage.Text = (voucher.PercentageDiscount * 100).ToString();
            if (voucher.MaxDiscount != null)
            {
                txtMax.Text = voucher._MaxDiscount.ToString();
            }
            else
            {   
                isMax.IsChecked = false;
            }

            if (voucher.Amount != null)
            { 
                txtAmount.Text = voucher.Amount.ToString();
            }
            else
            {   
                isAmount.IsChecked = false;
            }

            txtPoint.Text = voucher.RedemptionPoints.ToString();
            dpkrStart.SelectedDate = voucher.StartDate;
            if (voucher.EndDate != null)
            {
                dpkrEnd.SelectedDate = voucher.EndDate;
            }    
            else
            {
                isEnd.IsChecked = false;
            }    
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPercentage_TextChanged(object sender, TextChangedEventArgs e)
        {
           int per = 0;
            if (Int32.TryParse(txtPercentage.Text, out per))
            {
                if (per > 100)
                {
                    per = 100;
                    txtPercentage.Text = per.ToString();
                }
            }
            else
            {
                if (txtPercentage.Text.Length > 0)
                    txtPercentage.Text = txtPercentage.Text.Substring(0, txtPercentage.Text.Length - 1);
                else
                    txtPercentage.Text = "";
            }
        }


        private void txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra xem phím vừa nhấn có phải là phím số hay không
            bool isNumberKey = (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);

            // Kiểm tra xem phím vừa nhấn có phải là một phím điều hướng hay không
            bool isNavigationKey = e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Escape || e.Key == Key.Back;

            // Nếu phím vừa nhấn không phải là phím số hoặc phím điều hướng
            if (!isNumberKey && !isNavigationKey)
            {
                // Đánh dấu sự kiện đã được xử lý để ngăn chặn việc nhập ký tự
                e.Handled = true;
            }
        }

        private void isMax_Unchecked(object sender, RoutedEventArgs e)
        {
            txtMax.Text = "";
            txtMax.IsReadOnly = true;
        }

        private void isMax_Checked(object sender, RoutedEventArgs e)
        {
            if (txtMax != null)
                txtMax.IsReadOnly = false;
        }

        private void isAmount_Checked(object sender, RoutedEventArgs e)
        {
            if (txtAmount != null)
                txtAmount.IsReadOnly = false;
        }

        private void isAmount_Unchecked(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = "";
            txtAmount.IsReadOnly = true;
        }

        private void dpkrStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;
            DateTime? startDate = picker.SelectedDate;
            if ((dpkrEnd.SelectedDate != null && startDate > dpkrEnd.SelectedDate) || dpkrStart.SelectedDate == null)
            {
                boxStart.BorderBrush = Brushes.Red;
            }
            else
            {
                boxStart.BorderBrush = dark;
                boxEnd.BorderBrush = dark;
            }    
                
        }

        private void dpkrEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;
            DateTime? endDate = picker.SelectedDate;
            if ((dpkrStart.SelectedDate != null && endDate < dpkrStart.SelectedDate) || dpkrEnd.SelectedDate == null)
            {
                boxEnd.BorderBrush = Brushes.Red;
            }
            else
            {
                boxStart.BorderBrush = dark;
                boxEnd.BorderBrush = dark;
            }    
        }

        private void editVoucher_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            
            int? max = null;
            int? amount = null;
            
            int maxTemp;
            int amountTemp;
            int.TryParse(txtMax.Text, System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out maxTemp);
            int.TryParse(txtAmount.Text, out amountTemp);
            if (maxTemp > 0)
                max = maxTemp;
            if (amountTemp > 0)
                amount = amountTemp;
            decimal per = 0;
            decimal.TryParse(txtPercentage.Text, out per);

            int point = 0;
            int.TryParse(txtPoint.Text, out point);
            
            if (txtName.Text == "")
            {
                boxNameVoucher.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (txtPercentage.Text == "" || per <= 0)
            {
                boxPercentage.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (isMax.IsChecked == true && (txtMax.Text == "" || maxTemp <= 0))
            {
                boxMax.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (isAmount.IsChecked == true && (txtAmount.Text == "" || amountTemp <= 0))
            {
                boxAmount.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (txtPoint.Text == "" || point < 0)
            {
                boxPoint.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (dpkrStart.SelectedDate == null)
            {
                boxStart.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (isEnd.IsChecked == true && dpkrEnd.SelectedDate == null)
            {
                boxEnd.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (flag)
            {
                int id = int.Parse(txtID.Text);
                if (BUS_Coupon.editCoupon(id,txtName.Text, per / 100, max/1000, amount, point, (DateTime)dpkrStart.SelectedDate, dpkrEnd.SelectedDate))
                {
                    MessageBox.Show("Thành công");
                    Admin mainWindow = (Admin)this.Owner;
                    mainWindow.voucherList.ItemsSource = BUS_Coupon.GetCoupons();
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
            else
                MessageBox.Show("ở đây");
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text != null)
                boxNameVoucher.BorderBrush = dark;
            else
                boxNameVoucher.BorderBrush = Brushes.Red;
        }
       
        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                long per = 0;
                TextBox txt = sender as TextBox;
                if (!long.TryParse(txt.Text, System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out per))
                {
                    if (txt.Text.Length > 0)
                    {
                        while (!long.TryParse(txtMax.Text, out per) && txt.Text != "")
                        {
                            txt.TextChanged -= txt_TextChanged;

                            txt.Text = txt.Text.Substring(0, txt.Text.Length - 1);
                            txt.Select(txt.Text.Length, 0);
                            txt.TextChanged += txt_TextChanged;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        txt.Text = "";
                    }
                }
            }
            catch (Exception) { }

        }

        private void isEnd_Checked(object sender, RoutedEventArgs e)
        {
            if (dpkrEnd != null)
                dpkrEnd.IsEnabled = true;
        }

        private void isEnd_Unchecked(object sender, RoutedEventArgs e)
        {
            dpkrEnd.IsEnabled = false;
        }
    }
}
