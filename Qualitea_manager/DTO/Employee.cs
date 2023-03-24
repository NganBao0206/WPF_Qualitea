using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Employee
    {
        public Employee()
        {
            this.Works = new HashSet<Work>();
        }
        [Key]
        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RoleID { get; set; }

        [NotMapped]
        public bool _isEmployed { 
            get 
            {
                if (EndDate == null)
                    return true;
                else
                    return false;
            } 
        }
        public virtual Person Person { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
