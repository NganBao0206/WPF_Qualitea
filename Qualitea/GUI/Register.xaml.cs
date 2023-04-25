using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Net.Mail;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BUS;
using DTO;
using System.Globalization;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private BUS_Customer bc = new BUS_Customer();
        public Register()
        {
            InitializeComponent();
        }

        DateTime date = DateTime.Now;

        private void textName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtName.Focus();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                textName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textName.Visibility = Visibility.Visible;
            }
        }
        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }


        private void textUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUser.Text) && txtUser.Text.Length > 0)
            {
                textUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUser.Visibility = Visibility.Visible;
            }
        }

        private void textPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPass.Focus();
        }

        private void txtPass_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPass.Password) && txtPass.Password.Length > 0)
            {
                textPass.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPass.Visibility = Visibility.Visible;
            }
        }

        private void textConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtConfirm.Focus();
        }

        private void txtConfirm_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtConfirm.Password) && txtConfirm.Password.Length > 0)
            {
                textConfirm.Visibility = Visibility.Collapsed;
            }
            else
            {
                textConfirm.Visibility = Visibility.Visible;
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Login a = new Login();
            this.Close();
            a.Show();
        }

        TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(txtName.Text)) && (DOB.SelectedDate != null) && (!String.IsNullOrEmpty(txtEmail.Text)))
            {
                txtName.Text = textInfo.ToTitleCase(txtName.Text.Trim().Replace(@"\s+", " ").ToLower());
                txtEmail.Text = txtEmail.Text.ToLower();
                bool result;
                try
                {
                    MailAddress m = new MailAddress(txtEmail.Text);
                    result = true;
                }
                catch (FormatException)
                {
                    result = false;
                }

                int age = DateTime.Now.Year - DOB.SelectedDate.Value.Year;
                if (!Regex.IsMatch(txtName.Text, @"[\p{L}\s]+$") || !result)
                {
                    
                    txtAlert1.Visibility = Visibility.Collapsed;
                    txtAlert4.Visibility = Visibility.Visible;
                    return;
                }
                if (age < 18)
                {
                    MessageBox.Show("Độ tuổi khách hàng đăng ký phải từ 18 tuổi trở lên", "Thông báo", MessageBoxButton.OK);
                    return;
                }
                txtAlert4.Visibility = Visibility.Collapsed;
                date = (DateTime)DOB.SelectedDate;
                txtAlert1.Visibility = Visibility.Collapsed;
                Panel.SetZIndex(step2, 1);
            }
            else
            {
                txtAlert1.Visibility = Visibility.Visible;
                txtAlert4.Visibility = Visibility.Collapsed;
            }
        }


        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có xác nhận những thông tin đăng ký trên không ?", "Xác nhận đăng ký", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if ((!String.IsNullOrEmpty(txtUser.Text)) && (!String.IsNullOrEmpty(txtPass.Password)) && (!String.IsNullOrEmpty(txtConfirm.Password)))
                {
                    txtUser.Text = txtUser.Text.Trim().ToLower();

                    if (!Regex.IsMatch(txtUser.Text, @"[a-zA-Z0-9.]+$"))
                    {
                        MessageBox.Show("Username chỉ chứa chữ, số, dấu chấm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    txtAlert2.Visibility = Visibility.Collapsed;
                    DTO.Customer checkUser = bc.getCustomerByUsername(txtUser.Text);
                    if (checkUser != null)
                    {
                        txtAlertUser.Visibility = Visibility.Visible;
                        txtAlert3.Visibility = Visibility.Collapsed;
                        txtAlert2.Visibility = Visibility.Collapsed;
                        return;
                    }
                    txtAlertUser.Visibility = Visibility.Collapsed;
                    if (txtPass.Password == txtConfirm.Password)
                    {
                        txtAlert3.Visibility = Visibility.Collapsed;
                        DTO.Customer newCustomer = new DTO.Customer();
                        newCustomer.Name = txtName.Text;
                        newCustomer.DOB = DOB.SelectedDate.Value;
                        newCustomer.Email = txtEmail.Text;
                        newCustomer.Username = txtUser.Text;
                        newCustomer.Password = txtPass.Password;
                        if (bc.addCustomer(newCustomer))
                        {
                            MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            Login l = new Login();
                            this.Close();
                            l.Show();
                        }
                    }
                    else
                    {
                        txtAlert3.Visibility = Visibility.Visible;
                        txtAlert2.Visibility = Visibility.Collapsed;
                        txtAlertUser.Visibility = Visibility.Collapsed;

                    }
                }
                else
                {
                    txtAlert2.Visibility = Visibility.Visible;
                    txtAlert3.Visibility = Visibility.Collapsed;
                    txtAlertUser.Visibility = Visibility.Collapsed;

                }
            }
        }
    }
}
