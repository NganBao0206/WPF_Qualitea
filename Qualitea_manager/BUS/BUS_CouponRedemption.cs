using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_CouponRedemption
    {
        private static Random random = new Random();
        public static string GenerateVoucherCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static bool addCouponRedemption(Coupon coupon, Login login, DateTime RedemptionDate)
        {
            string newCode = GenerateVoucherCode(6);
            return DAO_CouponRedemption.addCouponRedemption(newCode, coupon, login, RedemptionDate);
        }
        
        public static List<CouponRedemption> GetCouponRedemptions(Login user)
        {
            return DAO_CouponRedemption.GetCouponRedemptions(user);
        }

    }

}
