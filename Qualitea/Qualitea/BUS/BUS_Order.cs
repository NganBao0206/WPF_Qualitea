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
        private DAO_Order daoOrder;
        public BUS_Order()
        {
            daoOrder = new DAO_Order();
        }
        public List<OrderDetail> GetOrderDetailsByProductOption(int productOptionId)
        {
            return daoOrder.GetOrderDetailsByProductOption(productOptionId);
        }

        public List<OrderHeader> GetOrderHeadersByCustomerID(int ID)
        {
            return daoOrder.GetOrderHeadersByCustomerID(ID);
        }

        public OrderHeader GetOrderHeadersByID(int ID)
        {
            return daoOrder.GetOrderHeadersByID(ID);
        }

        public int addNewCusOrder(double Total, double DiscountTotal, List<OrderDetail> OrderDetails, int userID, int ScoreApply, string receiverName, string receiverAddress, string receiverPhone)
        {
            return daoOrder.addNewCusOrder(Total / 1000, DiscountTotal / 1000, OrderDetails, userID, ScoreApply, receiverName, receiverAddress, receiverPhone);
        }

        public List<OrderHeader> getAllOrders()
        {
            return daoOrder.getAllOrders();
        }

        public List<OrderHeader> getOrdersByDate(DateTime date)
        {
            return daoOrder.getOrdersByDate(date);
        }

        public List<OrderHeader> getOrdersByMonth(DateTime month)
        {
            return daoOrder.getOrdersByMonth(month);
        }


        public List<OrderHeader> getOrdersFilter(string keyword, DateTime? minDate, DateTime? maxDate, int? status)
        {
            return daoOrder.getOrdersFilter(keyword, minDate, maxDate, status);
        }

        public double getTotal(List<OrderHeader> orders)
        {
            return daoOrder.getTotal(orders);
        }

        public int getCount(List<OrderHeader> orders)
        {
            return daoOrder.getCount(orders);
        }

        public List<DateTimeSales> getSalesInDate()
        {
            return daoOrder.getSalesInDate();
        }

        public List<TypeSales> getCategorySalesInDate()
        {
            return daoOrder.getCategorySalesInDate();
        }

        public List<TypeSales> getProductSalesInDate()
        {
            return daoOrder.getProductSalesInDate();
        }

        public List<DateTimeSales> getSalesInMonth()
        {

            return daoOrder.getSalesInMonth();
        }

        public List<TypeSales> getCategorySalesInMonth()
        {
            return daoOrder.getCategorySalesInMonth();
        }
        public List<TypeSales> getProductSalesInMonth()
        {
            return daoOrder.getProductSalesInMonth();
        }

        public List<DateTimeSales> getSalesInYear(int Year)
        {
            return daoOrder.getSalesInYear(Year);
        }

        public List<TypeSales> getCategorySalesInYear(int Year)
        {
            return daoOrder.getCategorySalesInYear(Year);
        }

        public List<TypeSales> getProductSalesInYear(int Year)
        {
            return daoOrder.getProductSalesInYear(Year);
        }

        public bool addNewEmpOrder(double Total, double DiscountTotal, List<OrderDetail> OrderDetails, int? userID, int ScoreApply, int staffID)
        {
            return daoOrder.addNewEmpOrder(Total, DiscountTotal, OrderDetails, userID, ScoreApply, staffID);
        }

        public List<OrderHeader> getEmployeeOrders(int StaffID)
        {
            return daoOrder.getEmployeeOrders(StaffID);
        }

        public List<OrderHeader> getEmployeeOrders(int StaffID, DateTime? date, bool isOnline, int Status)
        {
            return daoOrder.getEmployeeOrders(StaffID, date, isOnline, Status);
        }

        public List<OrderHeader> getUnconfimredOrders()
        {
            return daoOrder.getUnconfimredOrders();
        }

        public bool ChangeStatus(OrderHeader orderHeader, int Status)
        {
            return daoOrder.ChangeStatus(orderHeader, Status);
        }

        public bool confirmOrder(OrderHeader orderHeader, int StaffID)
        {
            return daoOrder.confirmOrder(orderHeader, StaffID);
        }

        public int countOrderEmployeeToday(int StaffID)
        {

            return daoOrder.countOrderEmployeeToday(StaffID);
        }

        public double TotalOrderEmployeeToday(int StaffID)
        {
            return daoOrder.TotalOrderEmployeeToday(StaffID);
        }

        public bool cancelOrder(int OrderHeaderID)
        {
            return daoOrder.cancelOrder(OrderHeaderID);
        }

        public List<OrderDetail> GetOrderDetailsByProductID(int ProductID)
        {
            return daoOrder.GetOrderDetailsByProductID(ProductID);
        }
    }
}
