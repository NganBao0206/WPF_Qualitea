using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_Order
    {
        public static bool addNewOrder(DateTime OrderDate, decimal Total, decimal DiscountTotal, List<OrderDetail> OrderDetails, int StaffLoginID)
        {
            return DAO_Order.addNewOrder(OrderDate, Total, DiscountTotal, OrderDetails, StaffLoginID);
        }

        public static int addNewCusOrder(DateTime OrderDate, decimal Total, decimal DiscountTotal, List<OrderDetail> OrderDetails, Login user, CouponRedemption couponRedemption, string receiverName, string receiverAddress, string receiverPhone)
        {
            return DAO_Order.addNewCusOrder(OrderDate, Total/1000, DiscountTotal/1000, OrderDetails, user, couponRedemption, receiverName, receiverAddress, receiverPhone);
        }

        public static List<OrderHeader> GetOrders(Login user)
        {
            return DAO_Order.GetOrders(user);
        }
    }
}
