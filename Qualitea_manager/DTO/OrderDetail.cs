using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        [ForeignKey("OrderHeader")]
        public int OrderHeaderID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalLine { get; set; }
        public int ProductOptionID { get; set; }
        [NotMapped]
        public decimal _totalLine
        {
            get { return TotalLine * 1000; }
        }
        public virtual ProductOption ProductOption { get; set; }
        public virtual OrderHeader OrderHeader {get; set;} 
    }
}