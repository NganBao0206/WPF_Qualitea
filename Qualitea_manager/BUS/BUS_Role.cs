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
        public static List<Role> getRoles()
        {
            return DAO_Role.getRoles();
        }
    }
}
