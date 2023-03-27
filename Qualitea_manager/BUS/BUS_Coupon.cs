using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_Coupon
    {
        public static bool addCoupon(string name, decimal percentage, decimal? max, int? amount, int point, DateTime start, DateTime? end)
        {
            return DAO_Coupon.addCoupon(name, percentage, max, amount, point, start, end);
        }

        public static bool editCoupon(int ID, string name, decimal percentage, decimal? max, int? amount, int point, DateTime start, DateTime? end)
        {
            return DAO_Coupon.editCoupon(ID, name, percentage, max, amount, point, start, end);
        }

        public static List<Coupon> GetCoupons()
        {
            return DAO_Coupon.GetCoupons();
        }

        public static List<Coupon> GetCoupons(string keyword, decimal? minPer, decimal? maxPer, int? minAmount, int? maxAmount, int? minPoint, int? maxPoint, DateTime? minDate, DateTime? maxDate)
        {
            return DAO_Coupon.GetCoupons(keyword, minPer, maxPer, minAmount, maxAmount, minPoint, maxPoint, minDate, maxDate);
        }

        public static bool DelCoupon(Coupon c)
        {
            return DAO_Coupon.delCoupon(c);
        }

        public static List<Coupon> GetCurrentCoupons(string keyword, decimal? minPer, decimal? maxPer, int? minPoint, int? maxPoint)
        {
            return DAO_Coupon.GetCurrentCoupons(keyword, minPer, maxPer, minPoint, maxPoint);
        }
    }
}
