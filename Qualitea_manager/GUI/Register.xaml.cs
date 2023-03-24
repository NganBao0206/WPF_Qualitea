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


namespace Qualitea
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        DateTime dateValue;

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


        private void textDOB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDOB.Focus();
        }

        private void txtDOB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDOB.Text) && txtDOB.Text.Length > 0)
            {
                textDOB.Visibility = Visibility.Collapsed;
            }
            else
            {
                textDOB.Visibility = Visibility.Visible;
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
        }


        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParseExact(txtDOB.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateValue))
            {
                if ((!String.IsNullOrEmpty(txtName.Text) && txtName.Text.Length > 0)
                && (!String.IsNullOrEmpty(txtDOB.Text) && txtDOB.Text.Length > 0)
                && (!String.IsNullOrEmpty(txtDOB.Text) && txtDOB.Text.Length > 0))
                {
                    txtAlert1.Visibility = Visibility.Collapsed;
                    Panel.SetZIndex(step2, 1);

                }
                else
                {
                    txtAlert1.Visibility = Visibility.Visible;
                }
            }
            else
                MessageBox.Show("sai r");
        }



        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(txtUser.Text) && txtUser.Text.Length > 0) && (!String.IsNullOrEmpty(txtPass.Password) && txtPass.Password.Length > 0) && (!String.IsNullOrEmpty(txtConfirm.Password) && txtConfirm.Password.Length > 0))
            {
                txtAlert2.Visibility = Visibility.Collapsed;
                if (txtPass.Password == txtConfirm.Password)
                {
                    txtAlert3.Visibility = Visibility.Collapsed;
                    if (BUS_Login.addLogin(txtUser.Text, txtPass.Password, txtName.Text, dateValue, txtEmail.Text))
                    {
                        MessageBox.Show("Đăng ký thành công");
                        this.Close();
                    }   
                }
                else
                    txtAlert3.Visibility = Visibility.Visible;
            }
            else 
            {
                txtAlert2.Visibility = Visibility.Visible;
            }
        }

        
    }
}
