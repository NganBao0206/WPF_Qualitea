using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Shift
    {
        public Shift()
        {
            this.Works = new HashSet<Work>();
        }
        [Key]
        public int ShiftID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double TotalHours { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }


}
