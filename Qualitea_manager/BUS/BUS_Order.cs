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
    }
}
