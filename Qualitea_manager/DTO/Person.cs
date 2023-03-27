using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Person
    {
        public Person() {
            this.Logins = new HashSet<Login>();
        }
        [Key]
        public int PersonID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Login> Logins { get; set; }

    }
}
