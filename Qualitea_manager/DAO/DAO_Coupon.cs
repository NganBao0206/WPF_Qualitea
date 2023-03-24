using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_Coupon
    {
        private static MyContext entities = new MyContext();

        public static List<Coupon> GetCoupons()
        {
            return entities.Coupons.ToList();
        }

        public static List<Coupon> GetCoupons(string keyword, decimal? minPer, decimal? maxPer, int? minAmount, int? maxAmount, int? minPoint, int? maxPoint, DateTime? minDate, DateTime? maxDate)
        {
            List<Coupon> coupons = entities.Coupons.ToList();
            if (keyword != "")
                coupons = coupons.Where(c => c.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            if (minPer != null)
                coupons = coupons.Where(c => c.PercentageDiscount >= minPer).ToList();
            if (maxPer != null)
                coupons = coupons.Where(c => c.PercentageDiscount <= maxPer).ToList();
            if (minAmount != null)
                coupons = coupons.Where(c => c.Amount >= minAmount || c.Amount == null).ToList();
            if (maxAmount != null)
                coupons = coupons.Where(c => c.Amount <= maxAmount || c.Amount == null).ToList();
            if (minPoint != null)
                coupons = coupons.Where(c => c.RedemptionPoints >= minPoint).ToList();
            if (maxPoint != null)
                coupons = coupons.Where(c => c.RedemptionPoints <= maxPoint).ToList();
            if (minDate != null)
                coupons = coupons.Where(c => c.StartDate >= minDate).ToList();
            if (maxDate != null)
                coupons = coupons.Where(c => c.EndDate <= maxDate || c.EndDate == null).ToList();
            return coupons;
        }

        public static bool addCoupon(string name, decimal percentage, decimal? max, int? amount, int point, DateTime start, DateTime? end)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Coupon newCoupon = new Coupon();
                    newCoupon.Name = name;
                    newCoupon.PercentageDiscount = percentage;
                    newCoupon.MaxDiscount = max;
                    newCoupon.Amount = amount;
                    newCoupon.RedemptionPoints = point;
                    newCoupon.StartDate = start;
                    newCoupon.EndDate = end;
                    entities.Coupons.Add(newCoupon);

                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public static bool editCoupon(int ID, string name, decimal percentage, decimal? max, int? amount, int point, DateTime start, DateTime? end)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Coupon cp = entities.Coupons.Find(ID);
                    cp.Name = name;
                    cp.PercentageDiscount = percentage;
                    cp.MaxDiscount = max;
                    cp.Amount = amount;
                    cp.RedemptionPoints = point;
                    cp.StartDate = start;
                    cp.EndDate = end;
                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public static bool delCoupon(Coupon coupon)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Coupon cp = entities.Coupons.Find(coupon.CouponID);
                    if (cp.CouponRedemptions.Count > 0)
                        return false;
                    entities.Coupons.Remove(cp);
                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
