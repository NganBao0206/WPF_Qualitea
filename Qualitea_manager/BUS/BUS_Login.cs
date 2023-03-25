using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BUS
{
    public class BUS_Login
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

        public static bool addLogin(string username, string password, string name, DateTime dob, string email)
        {
            var hasher = new PasswordHasher<string>();
            var hashedPassword = hasher.HashPassword(null, password);
            username = formatString(username);
            password = hashedPassword;
            name = formatString(name);
            email = formatString(email);
            return DAO_Login.addLogin(username, password, name, dob, email);
        }

        public static int Authentication(string username, string password)
        {

            var hasher = new PasswordHasher<string>();
            Login LoginUser = DAO_Login.getLogin(username);
            if (LoginUser != null)
            {
                if (hasher.VerifyHashedPassword(null, LoginUser.Password, password) == PasswordVerificationResult.Success)
                {
                    CurrentLogin user = CurrentLogin.Instance;
                    user.Login = LoginUser;
                    if (LoginUser.Role == "Admin")
                        return 1;
                    else if (LoginUser.Role == "Employee")
                        return 2;
                    else
                        return 0;
                }
                else
                    return -1;
            }
            return -1;
        }

        public static List<Login> getLoginAdmin()
        {
            return DAO_Login.getLoginAdmin();
        }

        public static bool addLoginAdmin(string username, string password, string name, DateTime dob, string email)
        {
            var hasher = new PasswordHasher<string>();
            var hashedPassword = hasher.HashPassword(null, password);
            username = formatString(username);
            password = hashedPassword;
            name = formatString(name);
            email = formatString(email);
            return DAO_Login.addLoginAdmin(username, password, name, dob, email);
        }

        public static bool addLoginEmployee(string name, DateTime dob, string email, DateTime StartDate, DateTime? EndDate, int RoleID, string Username, string Password)
        {
            var hasher = new PasswordHasher<string>();
            var hashedPassword = hasher.HashPassword(null, Password);
            Username = formatString(Username);
            Password = hashedPassword;
            name = formatString(name);
            email = formatString(email);
            return DAO_Login.addLoginEmployee(name, dob, email, StartDate, EndDate, RoleID, Username, Password);
        }

        public static bool editLoginEmployee(int ID, string name, DateTime dob, string email, DateTime StartDate, DateTime? EndDate, int RoleID, string Username, string Password, bool isChangePass)
        {
            Username = formatString(Username);
            name = formatString(name);
            email = formatString(email);
            if (isChangePass)
            {
                var hasher = new PasswordHasher<string>();
                var hashedPassword = hasher.HashPassword(null, Password);
                Password = hashedPassword;
                return DAO_Login.editLoginEmployee(ID, name, dob, email, StartDate, EndDate, RoleID, Username, Password);

            }
            else
                return DAO_Login.editLoginEmployee(ID, name, dob, email, StartDate, EndDate, RoleID, Username);

        }

        public static List<Login> getLoginEmployees()
        {
            return DAO_Login.getLoginEmployees();
        }

        public static Login getLoginEmployee(int ID)
        {
            return DAO_Login.getLoginEmployee(ID);
        }

        public static List<Login> getLoginEmployees(string keyword, DateTime? dob, DateTime? StartDate, bool isEmployeed, bool isUnemployeed)
        {
            keyword = keyword.ToLower();
            bool? _isEmp;
            if (isEmployeed == true && isUnemployeed == true)
                _isEmp = null;
            else if (isEmployeed == false && isUnemployeed == false)
                return null;
            else
                _isEmp = isEmployeed == true ? true : false;
            return DAO_Login.getLoginEmployees(keyword, dob, StartDate, _isEmp);
        }

    }

}
