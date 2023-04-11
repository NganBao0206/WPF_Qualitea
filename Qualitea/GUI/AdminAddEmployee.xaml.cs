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
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminAddEmployee : Window
    {
        public Employee employee;
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(67,73,72));
        private BUS_Role br = new BUS_Role();
        private BUS_Employee be = new BUS_Employee();
        public AdminAddEmployee()
        {
            InitializeComponent();
            comboBoxRole.ItemsSource = br.getRoles();
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
                txtName.Text = employee.Name;
                dpkrDOB.SelectedDate = employee.DOB;
                txtEmail.Text = employee.Email;
                comboBoxRole.SelectedValue = employee.RoleID;
                dpkrStart.SelectedDate = employee.StartDate;
                if (!employee.IsEmployed)
                {
                    isEnd.IsChecked = true;
                }
                txtUsername.Text = employee.Username;
                txtPass.Password = employee.Password;
                txtConfirm.Password = employee.Password;
                txtID.Text = employee.EmployeeID.ToString();
            }
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dpkrStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpkrStart.SelectedDate == null)
            {
                boxStart.BorderBrush = Brushes.Red;
            }
            else
            {
                boxStart.BorderBrush = dark;
            }    
                
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
            if (isEnd.IsChecked == true)
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
                Employee emp = new Employee();
                emp.Name = txtName.Text;
                emp.DOB = dpkrDOB.SelectedDate.Value;
                emp.Email = txtEmail.Text;
                emp.StartDate = dpkrStart.SelectedDate.Value;
                emp.RoleID = (int) comboBoxRole.SelectedValue;
                emp.Username = txtUsername.Text;
                emp.Password = txtPass.Password;
                if (be.addEmployee(emp))
                {
                    MessageBox.Show("Thêm thành công");
                    boxName.BorderBrush = dark;
                    boxDOB.BorderBrush = dark;
                    boxEmail.BorderBrush = dark;
                    comboBoxRole.Style = (Style)FindResource("ComboBoxStyle1");
                    boxStart.BorderBrush = dark;
                    boxUsername.BorderBrush = dark;
                    boxPass.BorderBrush = dark;
                    boxConfirm.BorderBrush = dark;
                    txtName.Text = "";
                    dpkrDOB.SelectedDate = null;
                    txtEmail.Text = "";
                    isEnd.IsChecked = false;

                    txtUsername.Text = "";
                    txtPass.Password = "";
                    txtConfirm.Password = "";
                }    
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ");
        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (
                txtName.Text == employee.Name &&
                dpkrDOB.SelectedDate == employee.DOB &&
                txtEmail.Text == employee.Email &&
                (int)comboBoxRole.SelectedValue == employee.Role.RoleID &&
                dpkrStart.SelectedDate == employee.StartDate &&
                isEnd.IsChecked == false && !employee.IsEmployed &&
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
            if (isEnd.IsChecked == true)
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
                Employee emp = new Employee();
                emp.EmployeeID = int.Parse(txtID.Text);
                emp.Name = txtName.Text;
                emp.DOB = dpkrDOB.SelectedDate.Value;
                emp.Email = txtEmail.Text;
                emp.RoleID = (int)comboBoxRole.SelectedValue;
                emp.Username = txtUsername.Text;
                emp.Password = txtPass.Password;
                if (be.editEmployee(emp))
                {
                    MessageBox.Show("Chỉnh sửa thành công");
                    be = new BUS_Employee();
                    boxName.BorderBrush = dark;
                    boxDOB.BorderBrush = dark;
                    boxEmail.BorderBrush = dark;
                    comboBoxRole.Style = (Style)FindResource("ComboBoxStyle1");
                    boxStart.BorderBrush = dark;
                    boxUsername.BorderBrush = dark;
                    boxPass.BorderBrush = dark;
                    boxConfirm.BorderBrush = dark;
                    employee = be.getEmployeeByID(employee.EmployeeID);
                    txtName.Text = employee.Name;
                    dpkrDOB.SelectedDate = employee.DOB;
                    txtEmail.Text = employee.Email;
                    comboBoxRole.SelectedValue = employee.RoleID;
                    dpkrStart.SelectedDate = employee.StartDate;
                    if (!employee.IsEmployed)
                    {
                        isEnd.IsChecked = true;
                    }
                    txtUsername.Text = employee.Username;
                    txtPass.Password = employee.Password;
                    txtConfirm.Password = employee.Password;
                    txtID.Text = employee.EmployeeID.ToString();
                }
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ");
        }

        
    }
}
