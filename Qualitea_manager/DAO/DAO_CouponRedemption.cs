using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_CouponRedemption
    {
        public static bool addCouponRedemption(string CouponRedemptionID, Coupon coupon, Login login, DateTime RedemptionDate)
        {
            MyContext entities = new MyContext();
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    CouponRedemption myCoupon = new CouponRedemption();
                    myCoupon.CouponRedemptionID = CouponRedemptionID;
                    myCoupon.CouponID = coupon.CouponID;
                    myCoupon.LoginID = login.LoginID;
                    myCoupon.RedemptionDate = RedemptionDate;
                    entities.CouponRedemptions.Add(myCoupon);
                    Login thisLogin = entities.Logins.Find(login.LoginID);

                    thisLogin.Score -= coupon.RedemptionPoints;
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

        public static List<CouponRedemption> GetCouponRedemptions(Login user)
        {
            MyContext entities = new MyContext();
            return entities.CouponRedemptions.Where(c => c.LoginID == user.LoginID && c.OrderHeader == null).ToList();
        }
    }
}
