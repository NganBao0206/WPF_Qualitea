using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Product
    {
        public Product()
        {
            this.ProductOptions = new HashSet<ProductOption>();
            this.IsActive = true;
        }
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public double MinPrice
        {
            get
            {
                if (IsActive == true)
                    return ProductOptions.Where(po => po.IsActive == true).Min(po => po.Price) * 1000;
                else
                    return ProductOptions.Min(po => po.Price) * 1000;
            }
            
        }

        [NotMapped]
        public ICollection<ProductOption> ProductOptionsActive
        {
            get
            {
                if (IsActive == true)
                    return ProductOptions.Where(po => po.IsActive == true).ToList();
                else
                    return null;
            }

        }


        public virtual Category Category { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }

    }
}
