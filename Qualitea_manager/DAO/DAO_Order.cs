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
    }
}
