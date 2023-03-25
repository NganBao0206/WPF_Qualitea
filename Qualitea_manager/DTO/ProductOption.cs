using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int ProductOptionID { get; set; }

        public int ProductID { get; set; }
        public int SizeID { get; set; }

        public decimal Price { get; set; }


        public bool IsActive { get; set; }

        [NotMapped]
        public decimal _price
        {
            get
            {
                return Price * 1000;
            }
        }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }

    }
}
