using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CouponRedemption
    {
        [Key]
        [Index(IsUnique = true)]
        [MaxLength(6)]
        public string CouponRedemptionID { get; set; }
        public int CouponID { get; set; }
        public int LoginID { get; set; }
        public DateTime RedemptionDate { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Login Login { get; set; }

        
        public virtual OrderHeader OrderHeader {get; set;}
    }
}
