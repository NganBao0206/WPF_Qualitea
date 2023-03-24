using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_Employee
    {
        public static List<Employee> GetEmployees()
        {
            return DAO_Employee.GetEmployees();
        }

        public static Employee GetEmployee(int ID)
        {
            return DAO_Employee.GetEmployee(ID);
        }
    }
}
