using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_Role
    {
        private static MyContext entities = new MyContext();
        public static List<Role> getRoles ()
        {
            return entities.Roles.ToList();
        }
    }
}
