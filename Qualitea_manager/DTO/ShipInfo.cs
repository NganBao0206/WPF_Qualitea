using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ShipInfo
    {
        public ShipInfo()
        {
            this.OrderHeaders = new HashSet<OrderHeader>();
        }
        [Key]
        public int ShipInfoID { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string Address { get; set; }


        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }

    }
}
