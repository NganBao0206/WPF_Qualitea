using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderHeader
    {
        public OrderHeader()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.OrderDate = DateTime.Now;
        }
        [Key]
        public int OrderHeaderID { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public double DiscountTotal { get; set; }
        public bool IsOnlineOrder { get; set; }
        public int? ShipInfoID { get; set; }
        public int Status { get; set; }
        public int? CustomerID { get; set; }
        public int? EmployeeID { get; set; }

        [NotMapped]
        public double Cash
        {
            get
            {
                return (Total - DiscountTotal) * 1000;
            }
        }

        [NotMapped]
        public double _Total
        {
            get
            {
                return Total * 1000;
            }
        }

        [NotMapped]
        public double _DiscountTotal
        {
            get
            {
                return DiscountTotal * 1000;
            }
        }

        

        [NotMapped]
        public string CustomerName
        {
            get
            {
                return Customer.Name;
            }
        }

        [NotMapped]
        public string EmployeeName
        {
            get
            {
                return Employee.Name;
            }
        }

        [NotMapped]
        public string StatusString 
        {
            get
            {
                if (Status == 0)
                    return "Đơn hàng chưa xác nhận";
                if (Status == 1)
                    return "Đơn hàng đang xử lý";
                return "Đơn hàng hoàn tất";
            }
        }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ShipInfo ShipInfo { get; set; }
    }
}
