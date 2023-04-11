using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    

    public class DateTimeSales
    {
        public int Value { get; set; }
        public double OnlineSales { get; set; }
        public double OfflineSales { get; set; }

    }

    public class TypeSales
    {
        public String Value { get; set; }
        public int TotalSales { get; set; }

    }
}
