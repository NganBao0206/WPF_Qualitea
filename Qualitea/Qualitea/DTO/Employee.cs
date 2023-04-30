using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Employee
    {
        public Employee()
        {
            StartDate = DateTime.Now;
            IsEmployed = true;
        }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }   
        public DateTime StartDate { get; set; }

        public bool IsEmployed { get; set; }

        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
        public virtual Role Role { get; set; }
    }
}
