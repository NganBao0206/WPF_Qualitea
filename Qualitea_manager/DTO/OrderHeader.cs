using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderHeader
    {
        public OrderHeader()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int OrderHeaderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountTotal { get; set; }
        public bool IsOnlineOrder { get; set; }
        public int? ShipInfoID { get; set; }
        public int? CustomerLoginID { get; set; }
        public int StaffLoginID { get; set; }


        public virtual CouponRedemption CouponRedemption { get; set; }
        public virtual Login CustomerLogin { get; set; }
        public virtual Login StaffLogin { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ShipInfo ShipInfo { get; set; }
    }
}
