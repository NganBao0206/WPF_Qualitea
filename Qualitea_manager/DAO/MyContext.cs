using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class MyContext: DbContext
    {
        public MyContext() : base("name = conn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CouponRedemption>().HasOptional(cr => cr.OrderHeader).WithOptionalPrincipal(oh => oh.CouponRedemption);

            modelBuilder.Entity<OrderHeader>()
                    .HasOptional(m => m.CustomerLogin)
                    .WithMany(t => t.CustomerOrderHeaders)
                    .HasForeignKey(m => m.CustomerLoginID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderHeader>()
                        .HasRequired(m => m.StaffLogin)
                        .WithMany(t => t.StaffOrderHeaders)
                        .HasForeignKey(m => m.StaffLoginID)
                        .WillCascadeOnDelete(false);

        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponRedemption> CouponRedemptions { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ShipInfo> ShipInfos { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Work> Works { get; set; }
    }
}
