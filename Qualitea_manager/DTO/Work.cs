using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Work
    {
        [Key]
        public int PersonID { get; set; }
        public int ShiftID { get; set; }
        public DateTime DateWork { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
