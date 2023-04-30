using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_Role
    {
        private DAO_Role dr;
        public BUS_Role()
        {
            dr = new DAO_Role();
        }
        
        public List<Role> getRoles()
        {
            return dr.getRoles();
        }
    }
}
