using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(67, 73, 72));
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

            if (String.IsNullOrWhiteSpace(txtName.Text) || !Regex.IsMatch(txtName.Text, @"[\p{L}\s]+$"))
            {
                boxName.BorderBrush = Brushes.Red;
                MessageBox.Show("Tên không đúng định dạng");
                return;
            }
            else
            {
                boxName.BorderBrush = dark;
            }
            txtName.Text = validString(txtName.Text);

            if (String.IsNullOrWhiteSpace(txtEmail.Text) || !isValidEmail(txtEmail.Text))
            {
                boxEmail.BorderBrush = Brushes.Red;
                MessageBox.Show("Email không đúng định dạng");
                return;
            }
            else
            {
                boxEmail.BorderBrush = dark;
            }
            txtEmail.Text = txtEmail.Text.ToLower();

            if (String.IsNullOrWhiteSpace(txtUsername.Text) || !Regex.IsMatch(txtUsername.Text, @"[a-zA-Z0-9.]+$"))
            {
                boxUsername.BorderBrush = Brushes.Red;
                MessageBox.Show("Username chỉ được chứa chữ, số, dấu chấm");
                return;
            }
            else
            {
                boxUsername.BorderBrush = dark;
            }
            txtUsername.Text = txtUsername.Text.ToLower();

            if (dpkrDOB.SelectedDate == null)
            {
                boxDOB.BorderBrush = Brushes.Red;
                MessageBox.Show("Vui lòng nhập ngày sinh");
                return;
            }
            else
            {
                DateTime dob = dpkrDOB.SelectedDate.Value;
                if (DateTime.Now.Year - dob.Year < 18)
                {
                    boxDOB.BorderBrush = Brushes.Red;
                    MessageBox.Show("Nhân viên nhỏ hơn 18 tuổi không thể thêm");
                    return;
                }
                else
                {
                    boxDOB.BorderBrush = dark;
                }
            }


            if (dpkrStart.SelectedDate == null)
            {
                boxStart.BorderBrush = Brushes.Red;
                MessageBox.Show("Vui lòng nhập ngày bắt đầu làm");
                return;
            }
            else
            {
                DateTime dob = dpkrDOB.SelectedDate.Value;
                DateTime startWork = dpkrStart.SelectedDate.Value;
                if (startWork.Year - dob.Year < 18)
                {
                    boxStart.BorderBrush = Brushes.Red;
                    MessageBox.Show("Không thể thêm khách hàng đi làm lúc nhỏ hơn 18 tuổi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    boxStart.BorderBrush = dark;
                }
            }


            if (isEnd.IsChecked == true)
            {
                MessageBox.Show("Không thể thêm nhân viên đã nghỉ");
                return;
            }

            if (comboBoxRole.SelectedValue == null)
            {
                comboBoxRole.Style = (Style)FindResource("ComboBoxStyle2");
                MessageBox.Show("Vui lòng chọn chức vụ");
                return;
            }
            else
            {
                comboBoxRole.Style = (Style)FindResource("ComboBoxStyle1");
            }

            if (String.IsNullOrWhiteSpace(txtPass.Password) || (txtConfirm.Password != txtPass.Password))
            {
                boxPass.BorderBrush = Brushes.Red;
                boxConfirm.BorderBrush = Brushes.Red;
                MessageBox.Show("Mật khẩu và mật khẩu xác nhận chưa hợp lệ");
                return;
            }
            else
            {
                boxPass.BorderBrush = dark;
                boxConfirm.BorderBrush = dark;
            }

            Employee emp = new Employee();
            emp.Name = txtName.Text;
            emp.DOB = dpkrDOB.SelectedDate.Value;
            emp.Email = txtEmail.Text;
            emp.StartDate = dpkrStart.SelectedDate.Value;
            emp.RoleID = (int)comboBoxRole.SelectedValue;
            emp.Username = txtUsername.Text;
            if (be.getEmployeeByUsername(txtUsername.Text) != null)
            {
                MessageBox.Show("Username đã tồn tại không thể thêm");
                return;
            }
            emp.Password = txtPass.Password;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
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
                else
                {
                    MessageBox.Show("Thêm không thành công");
                }
            }
        }

        private String validString(String txt)
        {
            TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
            return textInfo.ToTitleCase(txt.Trim().Replace(@"\s+", " ").ToLower());
        }

        public bool isValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text) || !Regex.IsMatch(txtName.Text, @"[\p{L}\s]+$"))
            {
                boxName.BorderBrush = Brushes.Red;
                MessageBox.Show("Tên không đúng định dạng");
                return;
            }
            else
            {
                boxName.BorderBrush = dark;
            }
            txtName.Text = validString(txtName.Text);

            if (String.IsNullOrWhiteSpace(txtEmail.Text) || !isValidEmail(txtEmail.Text))
            {
                boxEmail.BorderBrush = Brushes.Red;
                MessageBox.Show("Email không đúng định dạng");
                return;
            }
            else
            {
                boxEmail.BorderBrush = dark;
            }
            txtEmail.Text = txtEmail.Text.ToLower();

            if (String.IsNullOrWhiteSpace(txtUsername.Text) || !Regex.IsMatch(txtUsername.Text, @"[a-zA-Z0-9.]+$"))
            {
                boxUsername.BorderBrush = Brushes.Red;
                MessageBox.Show("Username chỉ chứa chữ, số, dấu chấm");
                return;
            }
            else
            {
                boxUsername.BorderBrush = dark;
            }
            txtUsername.Text = txtUsername.Text.ToLower();

            if (dpkrDOB.SelectedDate == null)
            {
                boxDOB.BorderBrush = Brushes.Red;
                MessageBox.Show("Vui lòng nhập ngày sinh");
                return;
            }
            else
            {
                DateTime dob = dpkrDOB.SelectedDate.Value;
                if (DateTime.Now.Year - dob.Year < 18)
                {
                    boxDOB.BorderBrush = Brushes.Red;
                    MessageBox.Show("Nhân viên nhỏ hơn 18 tuổi không thể thêm");
                    return;
                }
                else
                {
                    boxDOB.BorderBrush = dark;
                }
            }

            if (dpkrStart.SelectedDate == null)
            {
                boxStart.BorderBrush = Brushes.Red;
                MessageBox.Show("Vui lòng nhập ngày bắt đầu làm");
                return;
            }

            else
            {
                DateTime dob = dpkrDOB.SelectedDate.Value;
                DateTime startWork = dpkrStart.SelectedDate.Value;
                if (startWork.Year - dob.Year < 18)
                {
                    boxStart.BorderBrush = Brushes.Red;
                    MessageBox.Show("Không thể thêm khách hàng đi làm lúc nhỏ hơn 18 tuổi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    boxStart.BorderBrush = dark;
                }
            }

            if (comboBoxRole.SelectedValue == null)
            {
                comboBoxRole.Style = (Style)FindResource("ComboBoxStyle2");
                MessageBox.Show("Vui lòng chọn chức vụ");
                return;
            }
            else
            {
                comboBoxRole.Style = (Style)FindResource("ComboBoxStyle1");
            }

            if (String.IsNullOrWhiteSpace(txtPass.Password) || (txtConfirm.Password != txtPass.Password))
            {
                boxPass.BorderBrush = Brushes.Red;
                boxConfirm.BorderBrush = Brushes.Red;
                MessageBox.Show("Mật khẩu và mật khẩu xác nhận chưa hợp lệ");
                return;
            }
            else
            {
                boxPass.BorderBrush = dark;
                boxConfirm.BorderBrush = dark;
            }


            if (
                txtName.Text == employee.Name &&
                dpkrDOB.SelectedDate.Value == employee.DOB &&
                txtEmail.Text == employee.Email &&
                (int)comboBoxRole.SelectedValue == employee.Role.RoleID &&
                dpkrStart.SelectedDate.Value == employee.StartDate &&
                ((isEnd.IsChecked == false && employee.IsEmployed) || (isEnd.IsChecked == true && !employee.IsEmployed)) &&
                txtUsername.Text == employee.Username &&
                txtPass.Password == employee.Password &&
                txtConfirm.Password == employee.Password
             )
            {
                MessageBox.Show("Không có gì thay đổi");
                return;
            }

            Employee emp = new Employee();
            emp.EmployeeID = int.Parse(txtID.Text);
            emp.Name = txtName.Text;
            emp.DOB = dpkrDOB.SelectedDate.Value;
            emp.Email = txtEmail.Text;
            emp.RoleID = (int)comboBoxRole.SelectedValue;
            emp.Username = txtUsername.Text;
            Employee temp = be.getEmployeeByUsername(txtUsername.Text);
            if (temp != null && temp.EmployeeID != emp.EmployeeID)
            {
                MessageBox.Show("Username đã tồn tại không thể sửa");
                return;
            }
            emp.Password = txtPass.Password;
            emp.StartDate = dpkrStart.SelectedDate.Value;
            emp.IsEmployed = isEnd.IsChecked == true ? false : true;
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa nhân viên này không?", "Stop", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
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
                else
                {
                    MessageBox.Show("Sửa không thành công");
                }
            }

        }
    }

}
