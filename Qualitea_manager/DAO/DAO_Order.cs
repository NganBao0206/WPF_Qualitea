using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_Order
    {
        private static MyContext entities = new MyContext();
        public static bool addNewOrder(DateTime OrderDate, decimal Total, decimal DiscountTotal, List<OrderDetail> OrderDetails, int StaffLoginID)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    OrderHeader newOrder = new OrderHeader();
                    newOrder.OrderDate = OrderDate;
                    newOrder.Total = Total;
                    newOrder.DiscountTotal = DiscountTotal;
                    newOrder.StaffLoginID = StaffLoginID;
                    entities.OrderHeaders.Add(newOrder);
                    List<OrderDetail> ods = new List<OrderDetail>();
                    foreach (OrderDetail od in OrderDetails)
                    {
                        OrderDetail newod = new OrderDetail();
                        newod.OrderHeader = newOrder;
                        newod.ProductOptionID = od.ProductOptionID;
                        newod.Quantity = od.Quantity;
                        newod.TotalLine = od.TotalLine;
                        ods.Add(newod);
                    }
                    entities.OrderDetails.AddRange(ods);

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

        public static ShipInfo GetShipInfo(string receiverName, string receiverAddress, string receiverPhone)
        {
            return entities.ShipInfos.Where(s => s.ReceiverName == receiverName && s.Address == receiverAddress && s.ReceiverPhone == receiverPhone).FirstOrDefault();
        }

        public static List<OrderHeader> GetOrders(Login user)
        {
            return entities.OrderHeaders.Where(o => o.CustomerLoginID == user.LoginID).OrderByDescending(o => o.OrderDate).ToList();
        }

        public static int addNewCusOrder(DateTime OrderDate, decimal Total, decimal DiscountTotal, List<OrderDetail> OrderDetails, Login user , CouponRedemption couponRedemption, string receiverName, string receiverAddress, string receiverPhone)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    OrderHeader newOrder = new OrderHeader();
                    newOrder.OrderDate = OrderDate;
                    newOrder.Total = Total;
                    newOrder.DiscountTotal = DiscountTotal;
                    newOrder.CustomerLoginID = user.LoginID;
                    newOrder.IsOnlineOrder = true;
                    ShipInfo shipIF = GetShipInfo(receiverName, receiverAddress, receiverPhone);
                    if (shipIF != null)
                        newOrder.ShipInfoID = shipIF.ShipInfoID;
                    else
                    {
                        shipIF = new ShipInfo();
                        shipIF.ReceiverName = receiverName;
                        shipIF.Address = receiverAddress;
                        shipIF.ReceiverPhone = receiverPhone;
                        entities.ShipInfos.Add(shipIF);
                        newOrder.ShipInfo = shipIF;
                    }    
                    if (couponRedemption != null)
                    {
                        CouponRedemption cp = entities.CouponRedemptions.Find(couponRedemption.CouponRedemptionID);
                        if (couponRedemption != null)
                        {
                            newOrder.CouponRedemption = cp;
                        }
                    }    
                    
                    Login u = entities.Logins.Find(user.LoginID);
                    int newScore = (int)Math.Round(Total - DiscountTotal);
                    u.Score += newScore;
                    entities.OrderHeaders.Add(newOrder);
                    List<OrderDetail> ods = new List<OrderDetail>();
                    foreach (OrderDetail od in OrderDetails)
                    {
                        OrderDetail newod = new OrderDetail();
                        newod.OrderHeader = newOrder;
                        newod.ProductOptionID = od.ProductOptionID;
                        newod.Quantity = od.Quantity;
                        newod.TotalLine = od.TotalLine;
                        ods.Add(newod);
                    }
                    entities.OrderDetails.AddRange(ods);
                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;
                    transaction.Commit();
                    if (result > 0)
                        return newScore;
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}
