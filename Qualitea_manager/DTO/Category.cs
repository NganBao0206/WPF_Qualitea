using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Category
    {
        public Category() {
            this.Products = new HashSet<Product>();
        }
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}
