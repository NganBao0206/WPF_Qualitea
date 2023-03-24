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
    public partial class AdminAddEmployee : Window
    {
        private bool changePassword = false;
        public DTO.Login employee;
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(67,73,72));
        public AdminAddEmployee()
        {
            InitializeComponent();
            comboBoxRole.ItemsSource = BUS_Role.getRoles();
            comboBoxRole.DisplayMemberPath = "Name";
            comboBoxRole.SelectedValuePath = "RoleID";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (employee == null)
                dpkrStart.SelectedDate = DateTime.Now;
            else
            {
                groupID.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(groupName, 1);
                Grid.SetColumn(groupName, 1);
                editEmployee.Visibility = Visibility.Visible;
                txtName.Text = employee.Person.Name;
                dpkrDOB.SelectedDate = employee.Person.DOB;
                txtEmail.Text = employee.Person.Email;
                comboBoxRole.SelectedValue = employee.Person.Employee.RoleID;
                dpkrStart.SelectedDate = employee.Person.Employee.StartDate;
                if (!employee.Person.Employee._isEmployed)
                {
                    isEnd.IsChecked = true;
                    dpkrEnd.SelectedDate = employee.Person.Employee.EndDate;
                }
                txtUsername.Text = employee.Username;
                txtPass.Password = employee.Password;
                txtConfirm.Password = employee.Password;
                txtID.Text = employee.PersonID.ToString();
            }
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dpkrStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;
            DateTime? startDate = picker.SelectedDate;
            if ((dpkrEnd.SelectedDate != null && startDate >= dpkrEnd.SelectedDate) || dpkrStart.SelectedDate == null)
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
            if ((dpkrStart.SelectedDate != null && endDate <= dpkrStart.SelectedDate) || dpkrEnd.SelectedDate == null)
            {
                boxEnd.BorderBrush = Brushes.Red;
            }
            else
            {
                boxStart.BorderBrush = dark;
                boxEnd.BorderBrush = dark;
            }    
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

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (txtName.Text == "")
            {
                flag = false;
                boxName.BorderBrush = Brushes.Red;
            }
            if (dpkrDOB.SelectedDate == null)
            {
                flag = false;
                boxDOB.BorderBrush = Brushes.Red;
            }
            if (txtEmail.Text == "")
            {
                flag = false;
                boxEmail.BorderBrush = Brushes.Red;
            }    
            if (dpkrStart.SelectedDate == null)
            {
                flag = false;
                boxStart.BorderBrush = Brushes.Red;
            }    
            if (isEnd.IsChecked == true && dpkrEnd.SelectedDate == null)
            {
                flag = false;
                boxStart.BorderBrush = Brushes.Red;
            }    
            if (comboBoxRole.SelectedValue == null)
            {
                flag = false;
                comboBoxRole.Style = (Style)FindResource("ComboBoxStyle2");
            }    
            if (txtUsername.Text == "")
            {
                flag = false;
                boxUsername.BorderBrush = Brushes.Red;
            }    
            if (txtPass.Password == "" || (txtConfirm.Password != txtPass.Password))
            {
                flag = false; 
                boxPass.BorderBrush = Brushes.Red;
                boxConfirm.BorderBrush = Brushes.Red;
            }
            if (flag)
            {
                if (BUS_Login.addLoginEmployee(txtName.Text, (DateTime)dpkrDOB.SelectedDate, txtEmail.Text, (DateTime)dpkrStart.SelectedDate, dpkrEnd.SelectedDate, (int)comboBoxRole.SelectedValue, txtUsername.Text, txtPass.Password.ToString()))
                {
                    MessageBox.Show("Thêm thành công");
                    boxName.BorderBrush = dark;
                    boxDOB.BorderBrush = dark;
                    boxEmail.BorderBrush = dark;
                    comboBoxRole.Style = (Style)FindResource("ComboBoxStyle1");
                    boxStart.BorderBrush = dark;
                    boxEnd.BorderBrush = dark;
                    boxUsername.BorderBrush = dark;
                    boxPass.BorderBrush = dark;
                    boxConfirm.BorderBrush = dark;
                    txtName.Text = "";
                    dpkrDOB.SelectedDate = null;
                    txtEmail.Text = "";
                    isEnd.IsChecked = false;
                    dpkrEnd.SelectedDate = null;
                    txtUsername.Text = "";
                    txtPass.Password = "";
                    txtConfirm.Password = "";
                    Admin a = this.Owner as Admin;
                    a.updateEmployee();
                }    
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ");
        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (
                txtName.Text == employee.Person.Name &&
                dpkrDOB.SelectedDate == employee.Person.DOB &&
                txtEmail.Text == employee.Person.Email &&
                (int)comboBoxRole.SelectedValue == employee.Person.Employee.RoleID &&
                dpkrStart.SelectedDate == employee.Person.Employee.StartDate &&
                ((isEnd.IsChecked == false && !employee.Person.Employee._isEmployed) || dpkrEnd.SelectedDate == employee.Person.Employee.EndDate) &&
                txtUsername.Text == employee.Username &&
                txtPass.Password == employee.Password &&
                txtConfirm.Password == employee.Password
             )
            {
                MessageBox.Show("Không có gì thay đổi");
                return;
            }
            if (txtName.Text == "")
            {
                flag = false;
                boxName.BorderBrush = Brushes.Red;
            }
            if (dpkrDOB.SelectedDate == null)
            {
                flag = false;
                boxDOB.BorderBrush = Brushes.Red;
            }
            if (txtEmail.Text == "")
            {
                flag = false;
                boxEmail.BorderBrush = Brushes.Red;
            }
            if (dpkrStart.SelectedDate == null)
            {
                flag = false;
                boxStart.BorderBrush = Brushes.Red;
            }
            if (isEnd.IsChecked == true && dpkrEnd.SelectedDate == null)
            {
                flag = false;
                boxStart.BorderBrush = Brushes.Red;
            }
            if (comboBoxRole.SelectedValue == null)
            {
                flag = false;
                comboBoxRole.Style = (Style)FindResource("ComboBoxStyle2");
            }
            if (txtUsername.Text == "")
            {
                flag = false;
                boxUsername.BorderBrush = Brushes.Red;
            }
            if (txtPass.Password == "" || (txtConfirm.Password != txtPass.Password))
            {
                flag = false;
                boxPass.BorderBrush = Brushes.Red;
                boxConfirm.BorderBrush = Brushes.Red;
            }
            if (flag)
            {
                if (BUS_Login.editLoginEmployee(int.Parse(txtID.Text),txtName.Text, (DateTime)dpkrDOB.SelectedDate, txtEmail.Text, (DateTime)dpkrStart.SelectedDate, dpkrEnd.SelectedDate, (int)comboBoxRole.SelectedValue, txtUsername.Text, txtPass.Password.ToString(), changePassword))
                {
                    MessageBox.Show("Chỉnh sửa thành công");
                    boxName.BorderBrush = dark;
                    boxDOB.BorderBrush = dark;
                    boxEmail.BorderBrush = dark;
                    comboBoxRole.Style = (Style)FindResource("ComboBoxStyle1");
                    boxStart.BorderBrush = dark;
                    boxEnd.BorderBrush = dark;
                    boxUsername.BorderBrush = dark;
                    boxPass.BorderBrush = dark;
                    boxConfirm.BorderBrush = dark;
                    employee = BUS_Login.getLoginEmployee(employee.LoginID);
                    txtName.Text = employee.Person.Name;
                    dpkrDOB.SelectedDate = employee.Person.DOB;
                    txtEmail.Text = employee.Person.Email;
                    comboBoxRole.SelectedValue = employee.Person.Employee.RoleID;
                    dpkrStart.SelectedDate = employee.Person.Employee.StartDate;
                    if (!employee.Person.Employee._isEmployed)
                    {
                        isEnd.IsChecked = true;
                        dpkrEnd.SelectedDate = employee.Person.Employee.EndDate;
                    }
                    txtUsername.Text = employee.Username;
                    txtPass.Password = employee.Password;
                    txtConfirm.Password = employee.Password;
                    txtID.Text = employee.PersonID.ToString();
                    Admin a = this.Owner as Admin;
                    a.updateEmployee();
                }
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ");
        }

        private void txtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (employee != null && txtPass.Password != employee.Password)
                changePassword = true;
        }
    }
}
