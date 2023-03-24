using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Coupon
    {
        public Coupon() {
            this.CouponRedemptions = new HashSet<CouponRedemption>();
        }
        [Key]
        public int CouponID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PercentageDiscount { get; set; }
        public decimal? MaxDiscount { get; set; }
        public int? Amount { get; set; }
        public float RedemptionPoints { get; set; }
        [NotMapped]
        public decimal? _MaxDiscount
        {
            get
            {
                if (MaxDiscount != null)
                {
                    return MaxDiscount * 1000;
                }

                else
                    return null;
            }
        }
        public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; }
    }
}
