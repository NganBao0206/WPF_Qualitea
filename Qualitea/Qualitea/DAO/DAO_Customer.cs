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

        
    }
}
