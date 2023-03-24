using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_Employee
    {
        private static MyContext entities = new MyContext();
        public static List<Employee> GetEmployees()
        {
            return entities.Employees.ToList();
        }

        public static Employee GetEmployee(int ID)
        {
            return entities.Employees.Find(ID);
        }
    }
}
