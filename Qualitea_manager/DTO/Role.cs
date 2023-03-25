using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Role
    {
        public Role()
        {
            this.Employees = new HashSet<Employee>();
        }
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set;  }
        public double Salary { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}
