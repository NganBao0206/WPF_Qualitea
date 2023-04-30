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

namespace GUI
{

    public partial class Login : Window
    {
        private BUS_Employee be = new BUS_Employee();
        public Login()
        {
            InitializeComponent();
            if (be.getAdmins().Count == 0)
            {
                Employee e = new Employee();
                e.Name = "admin";
                e.DOB = new DateTime(2002, 6, 2);
                e.Email = "abc@gmail.com";
                e.Username = "admin";
                e.Password = "123";
                e.RoleID = 1;
                be.addEmployee(e);
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register rf = new Register();
            this.Close();
            rf.Show();
        }
        private BUS_Customer bc = new BUS_Customer();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text != "" && txtPass.Password != "")
            {
                if (EmpCheck.IsChecked == true)
                {
                    int result = be.authentication(txtUser.Text, txtPass.Password);
                    if (result == 1)
                    {
                        txtAlert.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Đăng nhập thành công");
                        Admin ad = new Admin();
                        this.Close();
                        ad.ShowDialog();
                        
                    }
                    else if (result == 2)
                    {
                        txtAlert.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Đăng nhập thành công nhân viên");
                        EmployeeHome emp = new EmployeeHome();
                        this.Close();
                        emp.ShowDialog();
                    }    
                    else
                    {
                        txtAlert.Visibility = Visibility.Visible;
                        MessageBox.Show("Tài khoản không hợp lệ");
                    }

                }
                else
                {
                    if (bc.authentication(txtUser.Text, txtPass.Password))
                    {
                        txtAlert.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Đăng nhập thành công");
                        GUI.Customer cus = new Customer();
                        this.Close();
                        cus.ShowDialog();
                    }
                    else
                    {
                        txtAlert.Visibility = Visibility.Visible;
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
                    }

                }
            }
        }


        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }

}
