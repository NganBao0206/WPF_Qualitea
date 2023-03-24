using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_Size
    {
        public static List<Size> getSizes()
        {
            MyContext entities = new MyContext();
            return entities.Sizes.OrderBy(s => s.Capactiy).ToList();
        }
    }
}
