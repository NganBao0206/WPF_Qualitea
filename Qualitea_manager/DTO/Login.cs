using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Login
    {
        public Login() {
            this.CouponRedemptions = new HashSet<CouponRedemption>();
            this.StaffOrderHeaders = new HashSet<OrderHeader>();
            this.CustomerOrderHeaders = new HashSet<OrderHeader>();

        }
        [Key]
        public int LoginID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Score { get; set; }
        public string Role { get; set; }

        public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<OrderHeader> StaffOrderHeaders { get; set; }
        public virtual ICollection<OrderHeader> CustomerOrderHeaders { get; set; }


    }
}
