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
            if (!String.IsNullOrEmpty(txtName.Text) && txtName.Text.Length > 0)
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
            this.Close();
            Login a = new Login();
            a.Show();
        }


        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(txtName.Text))&& (DOB.SelectedDate != null) && (!String.IsNullOrEmpty(txtEmail.Text)))
            {
                date = (DateTime)DOB.SelectedDate;
                txtAlert1.Visibility = Visibility.Collapsed;
                Panel.SetZIndex(step2, 1);
            }
            else
            {
                txtAlert1.Visibility = Visibility.Visible;
            }
        }


        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(txtUser.Text)) && (!String.IsNullOrEmpty(txtPass.Password)) && (!String.IsNullOrEmpty(txtConfirm.Password)))
            {
                txtAlert2.Visibility = Visibility.Collapsed;
                DTO.Customer checkUser =  bc.getCustomerByUsername(txtUser.Text);
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
                        MessageBox.Show("Đăng ký thành công", "Thông báo",MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();

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
