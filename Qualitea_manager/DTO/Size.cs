using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Size
    {
        public Size()
        {
            this.ProductOptions = new HashSet<ProductOption>();
        }
        [Key]
        public int SizeID { get; set; }
        public string Name { get; set; }
        public double Capactiy { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }

    }
}
