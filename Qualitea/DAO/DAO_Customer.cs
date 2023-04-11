using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_Customer
    {
        private MyContext context;

        public DAO_Customer()
        {
            context = new MyContext();
        }
        public bool addCustomer(Customer c)
        {
            context.Customers.Add(c);
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }

        public Customer getCustomerByUsername(String username)
        {
            return context.Customers.Where(c => c.Username == username).FirstOrDefault();
        }

        public Customer getCustomerByID(int ID)
        {
            MyContext context = new MyContext();
            return context.Customers.Find(ID);
        }

        //public static bool addLoginAdmin(string username, string password, string name, DateTime dob, string email)
        //{
        //    MyContext context = new MyContext();
        //    Login newLogin = new Login();
        //    Person newPerson = new Person();
        //    newLogin.Role = context.Roles.Where(r => r.Name == "manager").FirstOrDefault();
        //    newLogin.Username = username.ToLower();
        //    newLogin.Password = password;
        //    newLogin.StartDate = DateTime.Now;
        //    newPerson.Name = name;
        //    newPerson.DOB = dob;
        //    newPerson.Email = email.ToLower();
        //    newLogin.Person = newPerson;

        //    context.People.Add(newPerson);
        //    context.Logins.Add(newLogin);
        //    if (context.SaveChanges() > 0)
        //        return true;
        //    return false;
        //}

        //public static bool addLoginEmployee(string name, DateTime dob, string email, DateTime StartDate, int RoleID, string Username, string Password)
        //{
        //    MyContext entities = new MyContext();
        //    using (var transaction = entities.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            entities.Configuration.AutoDetectChangesEnabled = false;
        //            Person newPerson = new Person();
        //            newPerson.Name = name;
        //            newPerson.DOB = dob;
        //            newPerson.Email = email.ToLower();


        //            Login newLogin = new Login();
        //            newLogin.Username = Username.ToLower();
        //            newLogin.Password = Password;
        //            newLogin.StartDate = DateTime.Now;

        //            newLogin.Role = entities.Roles.Where(r => r.Name == "employee").FirstOrDefault();

        //            entities.People.Add(newPerson);
        //            entities.Logins.Add(newLogin);
        //            int result = entities.SaveChanges();
        //            entities.Configuration.AutoDetectChangesEnabled = true;

        //            transaction.Commit();

        //            return result > 0;
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            return false;
        //        }
        //    }
        //}

        //public static bool editLoginEmployee(int ID, string name, DateTime dob, string email, int RoleID, string Username, string Password)
        //{
        //    MyContext entities = new MyContext();
        //    using (var transaction = entities.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            entities.Configuration.AutoDetectChangesEnabled = false;
        //            Login empLogin = entities.Logins.Find(ID);
        //            Person empPer = empLogin.Person;
        //            empPer.Name = name;
        //            empPer.DOB = dob;
        //            empPer.Email = email.ToLower();

        //            empLogin.Username = Username.ToLower();
        //            if (Password != null)
        //                empLogin.Password = Password;

        //            empLogin.Role = entities.Roles.Find(RoleID);

        //            entities.ChangeTracker.DetectChanges(); //Phát hiện những thay đổi
        //            int result = entities.SaveChanges();
        //            entities.Configuration.AutoDetectChangesEnabled = true;

        //            transaction.Commit();

        //            return result > 0;
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            return false;
        //        }
        //    }
        //}

        //public static bool editLoginEmployee(int ID, string name, DateTime dob, string email, int RoleID, string Username)
        //{
        //    MyContext entities = new MyContext();
        //    using (var transaction = entities.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            entities.Configuration.AutoDetectChangesEnabled = false;
        //            Login empLogin = entities.Logins.Find(ID);
        //            Person empPer = empLogin.Person;

        //            empPer.Name = name;
        //            empPer.DOB = dob;
        //            empPer.Email = email.ToLower();

        //            empLogin.Role = entities.Roles.Find(RoleID);

        //            empLogin.Username = Username.ToLower();


        //            entities.ChangeTracker.DetectChanges(); //Phát hiện những thay đổi
        //            int result = entities.SaveChanges();
        //            entities.Configuration.AutoDetectChangesEnabled = true;

        //            transaction.Commit();

        //            return result > 0;
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            return false;
        //        }
        //    }
        //}
        //public static List<Employee> getLoginAdmin()
        //{
        //    MyContext context = new MyContext();
        //    return context.Employees.Where(l => l.Role.Name == "manager").ToList();
        //}

        //public static Employee getLoginEmployee(string username)
        //{
        //    MyContext context = new MyContext();
        //    return context.Employees.Where(l => l.Username.Equals(username)).FirstOrDefault();
        //}

        //public static Customer getLoginCustomer(string username)
        //{
        //    MyContext context = new MyContext();
        //    return context.Customers.Where(l => l.Username.Equals(username)).FirstOrDefault();
        //}

        //public static Employee getLoginEmployee(int ID)
        //{
        //    MyContext context = new MyContext();
        //    return context.Employees.Find(ID);
        //}




        //public static List<Login> getLoginEmployees(string keyword, DateTime? dob, DateTime? StartDate, bool? isEmployeed)
        //{
        //    MyContext context = new MyContext();
        //    keyword = keyword.ToLower();
        //    List<Login> logins = context.Logins.Where(l => l.Role.Name == "employee").ToList();
        //    if (keyword != "")
        //        logins = logins.Where(l => l.Person.Name.ToLower().Contains(keyword) || l.Person.Email.Contains(keyword) || l.Username.Contains(keyword)).ToList();
        //    if (dob != null)
        //        logins = logins.Where(l => l.Person.DOB == dob).ToList();

        //    if (StartDate != null)
        //        logins = logins.Where(l => l.StartDate == StartDate).ToList();
        //    if (isEmployeed != null)
        //        logins = logins.Where(l => l.isActive == isEmployeed).ToList();
        //    return logins;
        //}

        //public static Login getUserName(string username)
        //{
        //    MyContext context = new MyContext();
        //    return context.Logins.Where(l => l.Username == username).FirstOrDefault();
        //}
    }
}
