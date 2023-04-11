using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductOption
    {
        public ProductOption()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.IsActive = true;
        }
        public int ProductOptionID { get; set; }

        public int ProductID { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public double _price
        {
            get
            {
                return Price * 1000;
            }
            set
            {
                Price = value / 1000;
            } 
                
        }

        public virtual Product Product { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
