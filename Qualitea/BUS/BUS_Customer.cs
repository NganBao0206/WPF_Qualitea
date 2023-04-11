using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_Customer
    {
        private DAO_Customer dc = new DAO_Customer();
        public bool addCustomer(Customer c)
        {
            c.Password = PasswordManager.HashPassword(c.Password);
            return dc.addCustomer(c);
        }

        public Customer getCustomerByUsername(String username)
        {
            return dc.getCustomerByUsername(username);
        }

        public bool authentication(String username,String password)
        {
            Customer c = getCustomerByUsername(username);
            if (c != null && PasswordManager.VerifyPassword(password, c.Password))
            {
                CurrentLogin.Instance.LoginID = c.CustomerID;
                CurrentLogin.Instance.LoginType = "customer";
                return true;
            }
            else
                return false;
        }

        public Customer getCustomerByID(int ID)
        {
            return dc.getCustomerByID(ID);
        }
    }
}
