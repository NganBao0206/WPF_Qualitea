using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_Login
    {
        public static bool addLogin(string username, string password, string name, DateTime dob, string email)
        {
            MyContext context = new MyContext();
            Login newLogin = new Login();
            Person newPerson = new Person();
            Customer newCustomer = new Customer();
            newLogin.Username = username.ToLower();
            newLogin.Password = password;
            newLogin.Role = "Customer";
            newPerson.Name = name;
            newPerson.DOB = dob;
            newPerson.Email = email.ToLower();
            newCustomer.StartDate = DateTime.Now;
            newCustomer.Person = newPerson;
            newLogin.Person = newPerson;
            
            context.People.Add(newPerson);
            context.Logins.Add(newLogin);
            context.Customers.Add(newCustomer);
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }

        public static bool addLoginAdmin(string username, string password, string name, DateTime dob, string email)
        {
            MyContext context = new MyContext();
            Login newLogin = new Login();
            Person newPerson = new Person();
            Employee newEmployee = new Employee();
            newLogin.Username = username.ToLower();
            newLogin.Password = password;
            newLogin.Role = "Admin";
            newPerson.Name = name;
            newPerson.DOB = dob;
            newPerson.Email = email.ToLower();
            newLogin.Person = newPerson;

            context.People.Add(newPerson);
            context.Logins.Add(newLogin);
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }

        public static bool addLoginEmployee(string name, DateTime dob, string email, DateTime StartDate, DateTime? EndDate, int RoleID, string Username, string Password)
        {
            MyContext entities = new MyContext();
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Person newPerson = new Person();
                    newPerson.Name = name;
                    newPerson.DOB = dob;
                    newPerson.Email = email.ToLower();
                    entities.People.Add(newPerson);

                    Employee newEmp = new Employee();
                    newEmp.StartDate = StartDate;
                    newEmp.EndDate = EndDate;
                    newEmp.RoleID = RoleID;
                    newEmp.Person = newPerson;
                    entities.Employees.Add(newEmp);

                    Login newLogin = new Login();
                    newLogin.Username = Username.ToLower();
                    newLogin.Password = Password;
                    if (RoleID == 1)
                        newLogin.Role = "Employee";
                    else if (RoleID == 2)
                        newLogin.Role = "Admin";
                    entities.Logins.Add(newLogin);


                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

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

        public static bool editLoginEmployee(int ID, string name, DateTime dob, string email, DateTime StartDate, DateTime? EndDate, int RoleID, string Username, string Password)
        {
            MyContext entities = new MyContext();
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Login empLogin = entities.Logins.Find(ID);
                    Person empPer = empLogin.Person;
                    Employee emp = empPer.Employee;

                    empPer.Name = name;
                    empPer.DOB = dob;
                    empPer.Email = email.ToLower();

                    emp.StartDate = StartDate;
                    emp.EndDate = EndDate;
                    emp.RoleID = RoleID;

                    empLogin.Username = Username.ToLower();
                    if (Password != null)
                        empLogin.Password = Password;
                    if (RoleID == 1)
                        empLogin.Role = "Employee";
                    else if (RoleID == 2)
                        empLogin.Role = "Admin";

                    entities.ChangeTracker.DetectChanges(); //Phát hiện những thay đổi
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

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

        public static bool editLoginEmployee(int ID, string name, DateTime dob, string email, DateTime StartDate, DateTime? EndDate, int RoleID, string Username)
        {
            MyContext entities = new MyContext();
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Login empLogin = entities.Logins.Find(ID);
                    Person empPer = empLogin.Person;
                    Employee emp = empPer.Employee;

                    empPer.Name = name;
                    empPer.DOB = dob;
                    empPer.Email = email.ToLower();

                    emp.StartDate = StartDate;
                    emp.EndDate = EndDate;
                    emp.RoleID = RoleID;

                    empLogin.Username = Username.ToLower();
                    if (RoleID == 1)
                        empLogin.Role = "Employee";
                    else if (RoleID == 2)
                        empLogin.Role = "Admin";

                    entities.ChangeTracker.DetectChanges(); //Phát hiện những thay đổi
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

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
        public static List<Login> getLoginAdmin()
        {
            MyContext context = new MyContext();
            return context.Logins.Where(l => l.Role == "Admin").ToList();
        }

        public static Login getLogin(string username)
        {
            MyContext context = new MyContext();
            return context.Logins.Where(l => l.Username.Equals(username)).FirstOrDefault();
        }

        public static List<Login> getLoginEmployees()
        {
            MyContext context = new MyContext();
            return context.Logins.Where(l => l.Person.Employee != null).ToList();
        }

        public static Login getLoginEmployee(int ID)
        {
            MyContext context = new MyContext();
            return context.Logins.Find(ID);
        }


        public static List<Login> getLoginEmployees(string keyword, DateTime? dob, DateTime? StartDate, bool? isEmployeed)
        {
            MyContext context = new MyContext();
            keyword = keyword.ToLower();
            List<Login> logins = context.Logins.Where(l => l.Person.Employee != null).ToList();
            if (keyword != "")
                logins = logins.Where(l => l.Person.Name.ToLower().Contains(keyword) || l.Person.Email.Contains(keyword) || l.Username.Contains(keyword)).ToList();
            if (dob != null)
                logins = logins.Where(l => l.Person.DOB == dob).ToList();

            if (StartDate != null)
                logins = logins.Where(l => l.Person.Employee.StartDate == StartDate).ToList();
            if (isEmployeed != null)
                logins = logins.Where(l => l.Person.Employee._isEmployed == isEmployeed).ToList();
            return logins;
        }

    }
}
