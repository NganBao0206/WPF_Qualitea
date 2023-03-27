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
        public int RedemptionPoints { get; set; }
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

        [NotMapped]
        public int? _remainingAmountNum
        {
            get
            {
                if (Amount != null)
                {
                    return Amount - this.CouponRedemptions.Count;
                }

                else
                    return null;
            }
        }

        [NotMapped]
        public string _remainingAmount
        {
            get
            {
                if (Amount != null)
                {
                    return (Amount - this.CouponRedemptions.Count).ToString();
                }

                else
                    return "Không giới hạn";
            }
        }

        [NotMapped]
        public string info
        {
            get
            {
                string s = this.Name + " - Giảm " + this.PercentageDiscount.ToString("P0") + " ";
                if (this.MaxDiscount != null)
                {
                    s += "tối đa " + ((decimal)_MaxDiscount).ToString("N0") + "đ";
                }
                return s;
            }
        }
        public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; }
    }
}
