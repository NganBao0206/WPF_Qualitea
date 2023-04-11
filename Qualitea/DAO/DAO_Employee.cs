using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_Employee
    {
        private MyContext context;

        public DAO_Employee()
        {
            context = new MyContext();
        }
        public bool addEmployee(Employee e)
        {
            context.Employees.Add(e);
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool editEmployee(Employee emp)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    Employee e = context.Employees.Find(emp.EmployeeID);
                    e.Name = emp.Name;
                    e.DOB = emp.DOB;
                    e.Email = emp.Email;
                    e.RoleID = emp.RoleID;
                    e.Username = emp.Username;
                    e.Password = emp.Password;
                    context.ChangeTracker.DetectChanges(); //Phát hiện những thay đổi
                    int result = context.SaveChanges();
                    context.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public Employee getEmployeeByUsername(String username)
        {
            return context.Employees.Where(e => e.Username == username).FirstOrDefault();
        }

        public Employee getEmployeeByID(int ID)
        {
            return context.Employees.Find(ID);
        }

        public List<Employee> getAdmins()
        {
            return context.Employees.Where(e => e.Role.Name == "Manager").ToList();
        }

        public List<Employee> getEmployees(string keyword, DateTime? dob, DateTime? StartDate, bool? isEmployeed)
        {
            
            keyword = keyword.ToLower();
            List<Employee> employees = context.Employees.ToList();
            if (keyword != "")
                employees = employees.Where(e => e.Name.ToLower().Contains(keyword) || e.Email.Contains(keyword) || e.Username.Contains(keyword)).ToList();
            if (dob != null)
                employees = employees.Where(e => e.DOB == dob).ToList();

            if (StartDate != null)
                employees = employees.Where(e => e.StartDate == StartDate).ToList();
            if (isEmployeed != null)
                employees = employees.Where(e => e.IsEmployed == isEmployeed).ToList();
            return employees;
        }

    }
}
