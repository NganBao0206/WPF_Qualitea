using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Work
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Employee")]
        public int PersonID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ShiftID { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime DateWork { get; set; }

        public Boolean isAccepted { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
