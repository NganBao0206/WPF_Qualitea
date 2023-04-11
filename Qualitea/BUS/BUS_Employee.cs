using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_Employee
    {
        private static string formatString(string s)
        {
            s = s.Trim();
            while (s.IndexOf("  ") >= 0)
            {
                s = s.Replace("  ", " ");
            }
            return s;
        }

        private DAO_Employee de = new DAO_Employee();
        public bool addEmployee(Employee e)
        {
            e.Username = formatString(e.Username).ToLower();
            e.Name = formatString(e.Name);
            e.Email = formatString(e.Email).ToLower();
            e.Password = PasswordManager.HashPassword(e.Password);
            return de.addEmployee(e);
        }

        public bool editEmployee(Employee emp)
        {
            emp.Username = formatString(emp.Username).ToLower();
            emp.Name = formatString(emp.Name);
            emp.Email = formatString(emp.Email).ToLower();
            if (de.getEmployeeByID(emp.EmployeeID).Password != emp.Password)
            {
                emp.Password = PasswordManager.HashPassword(emp.Password);
            }

            return de.editEmployee(emp);
        }

        public Employee getEmployeeByUsername(String username)
        {
            return de.getEmployeeByUsername(username);
        }

        public Employee getEmployeeByID(int ID)
        {
            return de.getEmployeeByID(ID);
        }

        public int authentication(String username, String password)
        {
            Employee e = getEmployeeByUsername(username);
            if (e != null && PasswordManager.VerifyPassword(password, e.Password))
            {
                CurrentLogin.Instance.LoginID = e.EmployeeID;

                if (e.Role.Name == "Manager")
                {
                    CurrentLogin.Instance.LoginType = "Admin";
                    return 1;
                }
                else
                {
                    CurrentLogin.Instance.LoginType = "Staff";
                    return 2;
                }    
                    
            }
            else
                return -1;
        }

        public List<Employee> getAdmins()
        {
            return de.getAdmins();
        }

        public List<Employee> getEmployees(string keyword, DateTime? dob, DateTime? StartDate, bool isEmployeed, bool isUnemployeed)
        {
            keyword = keyword.ToLower();
            bool? _isEmp;
            if (isEmployeed == true && isUnemployeed == true)
                _isEmp = null;
            else if (isEmployeed == false && isUnemployeed == false)
                return null;
            else
                _isEmp = isEmployeed == true ? true : false;
            return de.getEmployees(keyword, dob, StartDate, _isEmp);
        }
    }
}
