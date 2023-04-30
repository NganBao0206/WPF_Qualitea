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
        private MyContext entities;
        public DAO_Role()
        {
            entities = new MyContext();
        }
        
        public List<Role> getRoles()
        {
            return entities.Roles.ToList();
        }
    }
}
