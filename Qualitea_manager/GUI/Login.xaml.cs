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
using BUS;
using DTO;

namespace Qualitea
{

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            if (BUS_Login.getLoginAdmin().Count == 0)
                BUS_Login.addLoginAdmin("admin", "123", "Nguyễn Kim Bảo Ngân", DateTime.Now, "ngan@ou.edu.vn");
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register rf = new Register();
            rf.ShowInTaskbar = false;
            rf.Owner = this;
            rf.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text != "" && txtPass.Password != "")
            {
                int result = BUS_Login.Authentication(txtUser.Text, txtPass.Password);
                if (result >= 0)
                {
                    if (EmpCheck.IsChecked == true)
                    {
                        if (result == 2)
                        {
                            Employee emp = new Employee();
                            emp.Show();
                            this.Close();
                        }
                        else if (result == 1)
                        {
                            Admin ad = new Admin();
                            ad.Show();
                            this.Close();
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thành công");
                    }
                }
            }
        }

    }

}
