﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Customer
    {
        public Customer()
        {
            Score = 0;
            StartDate = DateTime.Now;
        }
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
    }
}
