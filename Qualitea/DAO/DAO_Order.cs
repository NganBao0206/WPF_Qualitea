using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_Order
    {
        private MyContext entities = new MyContext();
        public List<OrderDetail> GetOrderDetailsByProductOption(int productOptionId)
        {
            return entities.OrderDetails.Where(od => od.ProductOptionID == productOptionId).ToList();
        }

        public List<OrderHeader> GetOrderHeadersByCustomerID(int ID)
        {
            return entities.OrderHeaders.Where(oh => oh.CustomerID == ID).OrderByDescending(oh => oh.OrderDate).ToList();
        }

        public OrderHeader GetOrderHeadersByID(int ID)
        {
            return entities.OrderHeaders.Where(oh => oh.OrderHeaderID == ID).FirstOrDefault();
        }

        public ShipInfo GetShipInfo(String receiverName, String receiverAddress, String receiverPhone)
        {
            return entities.ShipInfos.Where(si => si.ReceiverName == receiverName && si.Address == receiverAddress && si.ReceiverPhone == receiverPhone).FirstOrDefault();
        }
        public int addNewCusOrder(double Total, double DiscountTotal, List<OrderDetail> OrderDetails, int userID, int ScoreApply, string receiverName, string receiverAddress, string receiverPhone)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;

                    OrderHeader newOrder = new OrderHeader();
                    newOrder.Total = Total;
                    newOrder.DiscountTotal = DiscountTotal;
                    newOrder.CustomerID = userID;

                    newOrder.IsOnlineOrder = true;
                    newOrder.Status = 0;

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

                    Customer c = entities.Customers.Find(userID);

                    int newScore = (int)Math.Round(Total - DiscountTotal);
                    c.Score -= ScoreApply;
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
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public bool addNewEmpOrder(double Total, double DiscountTotal, List<OrderDetail> OrderDetails, int? userID, int ScoreApply, int staffID)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;

                    OrderHeader newOrder = new OrderHeader();
                    newOrder.Total = Total;
                    newOrder.DiscountTotal = DiscountTotal;

                    newOrder.CustomerID = userID;

                    newOrder.IsOnlineOrder = false;
                    newOrder.Status = 1;
                    newOrder.EmployeeID = staffID;

                    Customer c = entities.Customers.Find(userID);

                    if (userID != null)
                    {
                        int newScore = (int)Math.Round(Total - DiscountTotal);
                        c.Score += newScore - ScoreApply;
                    }
                    
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
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<OrderHeader> getAllOrders()
        {
            return entities.OrderHeaders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public List<OrderHeader> getOrdersByDate(DateTime date)
        {
            return entities.OrderHeaders.Where(o => DbFunctions.TruncateTime(o.OrderDate) == DbFunctions.TruncateTime(date)).OrderByDescending(o => o.OrderDate).ToList();
        }

        public List<OrderHeader> getOrdersByMonth(DateTime month)
        {
            return entities.OrderHeaders.Where(o => o.OrderDate.Month == month.Month).OrderByDescending(o => o.OrderDate).ToList();
        }


        public List<OrderHeader> getOrdersFilter(string keyword, DateTime? minDate, DateTime? maxDate, int? status)
        {
            var orders = getAllOrders();
            if (!String.IsNullOrEmpty(keyword))
                orders = orders.Where(o => (o.Customer != null && o.Customer.Name.ToUpper().Contains(keyword.ToUpper())) || (o.Employee != null && o.Employee.Name.ToUpper().Contains(keyword.ToUpper()))).ToList();
            if (minDate != null)
                orders = orders.Where(o => o.OrderDate.Date >= minDate.Value.Date).ToList();
            if (maxDate != null)
                orders = orders.Where(o => o.OrderDate.Date <= maxDate.Value.Date).ToList();
            if (status != null)
                orders = orders.Where(o => o.Status == status).ToList();
            return orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public double getTotal(List<OrderHeader> orders)
        {
            return orders.Sum(order => order.Total - order.DiscountTotal);
        }

        public int getCount(List<OrderHeader> orders)
        {
            return orders.Count();
        }

        public List<DateTimeSales> getSalesInDate()
        {
            var hoursInDate = Enumerable.Range(1, 24)
                .Select(hours => new DateTimeSales { Value = hours, OnlineSales = 0, OfflineSales = 0 })
                .ToList();

            var salesByHours = entities.OrderHeaders
                .Where(o => DbFunctions.TruncateTime(o.OrderDate) == DateTime.Today)
                .GroupBy(o => o.OrderDate.Hour)
                .Select(g => new DateTimeSales
                {
                    Value = g.Key,
                    OnlineSales = g.Where(o => o.IsOnlineOrder).Sum(o => (double?)(o.Total - o.DiscountTotal)) ?? 0,
                    OfflineSales = g.Where(o => !o.IsOnlineOrder).Sum(o => (double?)(o.Total - o.DiscountTotal)) ?? 0
                })
                .ToList();

            var result = hoursInDate.GroupJoin(salesByHours,
                d => d.Value,
                s => s.Value,
                (d, s) => new DateTimeSales
                {
                    Value = d.Value,
                    OnlineSales = s.Sum(x => x.OnlineSales),
                    OfflineSales = s.Sum(x => x.OfflineSales)
                })
                .ToList();

            return result;
        }

        public List<TypeSales> getCategorySalesInDate()
        {



            var salesByCategory = entities.OrderDetails
                .Where(od => DbFunctions.TruncateTime(od.OrderHeader.OrderDate) == DateTime.Today)
                .GroupBy(od => od.ProductOption.Product.Category.Name)
                .Select(g => new TypeSales
                {
                    Value = g.Key,
                    TotalSales = g.Sum(od => od.Quantity)
                })
                .ToList();



            return salesByCategory;
        }

        public List<TypeSales> getProductSalesInDate()
        {


            var salesByProduct = entities.OrderDetails
                .Where(od => DbFunctions.TruncateTime(od.OrderHeader.OrderDate) == DateTime.Today)
                .GroupBy(od => od.ProductOption.Product.Name)
                .Select(g => new TypeSales
                {
                    Value = g.Key,
                    TotalSales = g.Sum(od => od.Quantity)
                })
                .ToList();



            return salesByProduct;
        }

        public List<DateTimeSales> getSalesInMonth()
        {
            var daysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))
                .Select(day => new DateTimeSales { Value = day, OnlineSales = 0, OfflineSales = 0 })
                .ToList();

            var salesByDay = entities.OrderHeaders
                .Where(o => o.OrderDate.Month == DateTime.Today.Month && o.OrderDate.Year == DateTime.Today.Year)
                .GroupBy(o => new { o.OrderDate.Day })
                .Select(g => new DateTimeSales
                {
                    Value = g.Key.Day,
                    OnlineSales = g.Where(o => o.IsOnlineOrder).Sum(o => (double?)(o.Total - o.DiscountTotal)) ?? 0,
                    OfflineSales = g.Where(o => !o.IsOnlineOrder).Sum(o => (double?)(o.Total - o.DiscountTotal)) ?? 0
                })
                .ToList();

            var result = daysInMonth.GroupJoin(salesByDay,
                d => d.Value,
                s => s.Value,
                (d, s) => new DateTimeSales
                {
                    Value = d.Value,
                    OnlineSales = s.Sum(x => x.OnlineSales),
                    OfflineSales = s.Sum(x => x.OfflineSales)
                })
                .ToList();

            return result;
        }

        public List<TypeSales> getCategorySalesInMonth()
        {
            DateTime startDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var salesByCategory = entities.OrderDetails
                .Where(od => od.OrderHeader.OrderDate >= startDateOfMonth && od.OrderHeader.OrderDate <= endDateOfMonth)
                .GroupBy(od => od.ProductOption.Product.Category.Name)
                .Select(g => new TypeSales
                {
                    Value = g.Key,
                    TotalSales = g.Sum(od => od.Quantity)
                })
                .ToList();

            return salesByCategory;
        }

        public List<TypeSales> getProductSalesInMonth()
        {
            DateTime startDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var salesByCategory = entities.OrderDetails
                .Where(od => od.OrderHeader.OrderDate >= startDateOfMonth && od.OrderHeader.OrderDate <= endDateOfMonth)
                .GroupBy(od => od.ProductOption.Product.Name)
                .Select(g => new TypeSales
                {
                    Value = g.Key,
                    TotalSales = g.Sum(od => od.Quantity)
                })
                .ToList();


            return salesByCategory;
        }


        public List<DateTimeSales> getSalesInYear(int Year)
        {
            var months = Enumerable.Range(1, 12)
                .Select(month => new DateTimeSales { Value = month, OnlineSales = 0, OfflineSales = 0 })
                .ToList();

            var salesByDay = entities.OrderHeaders
                .Where(o => o.OrderDate.Year == Year)
                .GroupBy(o => new { o.OrderDate.Month })
                .Select(g => new DateTimeSales
                {
                    Value = g.Key.Month,
                    OnlineSales = g.Where(o => o.IsOnlineOrder).Sum(o => (double?)(o.Total - o.DiscountTotal)) ?? 0,
                    OfflineSales = g.Where(o => !o.IsOnlineOrder).Sum(o => (double?)(o.Total - o.DiscountTotal)) ?? 0
                })
                .ToList();

            var result = months.GroupJoin(salesByDay,
                d => d.Value,
                s => s.Value,
                (d, s) => new DateTimeSales
                {
                    Value = d.Value,
                    OnlineSales = s.Sum(x => x.OnlineSales),
                    OfflineSales = s.Sum(x => x.OfflineSales)
                })
                .ToList();

            return result;
        }

        public List<TypeSales> getCategorySalesInYear(int Year)
        {


            var salesByCategory = entities.OrderDetails
                .Where(od => od.OrderHeader.OrderDate.Year == Year)
                .GroupBy(od => od.ProductOption.Product.Category.Name)
                .Select(g => new TypeSales
                {
                    Value = g.Key,
                    TotalSales = g.Sum(od => od.Quantity)
                })
                .ToList();

            return salesByCategory;
        }

        public List<TypeSales> getProductSalesInYear(int Year)
        {
            var salesByCategory = entities.OrderDetails
                .Where(od => od.OrderHeader.OrderDate.Year == Year)
                .GroupBy(od => od.ProductOption.Product.Name)
                .Select(g => new TypeSales
                {
                    Value = g.Key,
                    TotalSales = g.Sum(od => od.Quantity)
                })
                .ToList();


            return salesByCategory;
        }

        public List<OrderHeader> getEmployeeOrders(int StaffID)
        {
            return entities.OrderHeaders.Where(oh => oh.EmployeeID == StaffID).ToList();
        }

        public List<OrderHeader> getEmployeeOrders(int StaffID, DateTime? date, bool isOnline, int Status)
        {
            List<OrderHeader> orderheaders = getEmployeeOrders(StaffID).Where(o => o.IsOnlineOrder == isOnline && o.Status == Status).ToList();
            if (date != null)
                orderheaders = orderheaders.Where(o => o.OrderDate.Date == date.Value.Date).ToList();

            return orderheaders;
        }


        public List<OrderHeader> getUnconfimredOrders()
        {
            return entities.OrderHeaders.Where(so => so.Status == 0).ToList();
        }

        public bool ChangeStatus(OrderHeader orderHeader, int Status)
        {
            OrderHeader oh = entities.OrderHeaders.Find(orderHeader.OrderHeaderID);
            oh.Status = Status;
            return entities.SaveChanges() > 0;
        }

        public bool confirmOrder(OrderHeader orderHeader, int StaffID)
        {
            OrderHeader oh = entities.OrderHeaders.Find(orderHeader.OrderHeaderID);
            if (oh.Status == 0)
            {
                DTO.Customer c = entities.Customers.Find(oh.CustomerID);
                int newScore = (int)Math.Round(oh.Total - oh.DiscountTotal);
                c.Score += newScore;
            }
            oh.Status = 1;
            oh.EmployeeID = StaffID;
            return entities.SaveChanges() > 0;
        }

        public int countOrderEmployeeToday(int StaffID)
        {
            List<OrderHeader> oh = getEmployeeOrders(StaffID);
            return oh.Where(o => o.OrderDate.Date == DateTime.Now.Date).Count();
        }

        public double TotalOrderEmployeeToday(int StaffID)
        {
            List<OrderHeader> oh = getEmployeeOrders(StaffID);
            return oh.Where(o => o.OrderDate.Date == DateTime.Now.Date).Sum(o => o.Total - o.DiscountTotal);
        }

        public bool cancelOrder(int OrderHeaderID)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    OrderHeader o = entities.OrderHeaders.Find(OrderHeaderID);
                    if (o.Status != 0)
                        return false;
                    if (o.CustomerID != null)
                    {
                        int score = (int)Math.Round(o.DiscountTotal);
                        DTO.Customer c = entities.Customers.Find(o.CustomerID);
                        c.Score += score;
                    }

                    entities.OrderHeaders.Remove(o);
                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<OrderDetail> GetOrderDetailsByProductID(int ProductID)
        {
            return entities.OrderDetails.Where(o => o.ProductOption.ProductID == ProductID).ToList();
        }
    }
}
