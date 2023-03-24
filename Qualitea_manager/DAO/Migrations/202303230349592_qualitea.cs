namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qualitea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductOptions",
                c => new
                    {
                        ProductOptionID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        SizeID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductOptionID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.SizeID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalLine = c.Double(nullable: false),
                        ProductOptionID = c.Int(nullable: false),
                        OrderHeader_OrderHeaderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.OrderHeaders", t => t.OrderHeader_OrderHeaderID)
                .ForeignKey("dbo.ProductOptions", t => t.ProductOptionID, cascadeDelete: true)
                .Index(t => t.ProductOptionID)
                .Index(t => t.OrderHeader_OrderHeaderID);
            
            CreateTable(
                "dbo.OrderHeaders",
                c => new
                    {
                        OrderHeaderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsOnlineOrder = c.Boolean(nullable: false),
                        ShipInfoID = c.Int(),
                        CustomerLoginID = c.Int(),
                        StaffLoginID = c.Int(nullable: false),
                        CouponRedemption_CouponRedemptionID = c.String(maxLength: 6),
                    })
                .PrimaryKey(t => t.OrderHeaderID)
                .ForeignKey("dbo.CouponRedemptions", t => t.CouponRedemption_CouponRedemptionID)
                .ForeignKey("dbo.Logins", t => t.CustomerLoginID)
                .ForeignKey("dbo.ShipInfoes", t => t.ShipInfoID)
                .ForeignKey("dbo.Logins", t => t.StaffLoginID)
                .Index(t => t.ShipInfoID)
                .Index(t => t.CustomerLoginID)
                .Index(t => t.StaffLoginID)
                .Index(t => t.CouponRedemption_CouponRedemptionID);
            
            CreateTable(
                "dbo.CouponRedemptions",
                c => new
                    {
                        CouponRedemptionID = c.String(nullable: false, maxLength: 6),
                        CouponID = c.Int(nullable: false),
                        LoginID = c.Int(nullable: false),
                        RedemptionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CouponRedemptionID)
                .ForeignKey("dbo.Coupons", t => t.CouponID, cascadeDelete: true)
                .ForeignKey("dbo.Logins", t => t.LoginID, cascadeDelete: true)
                .Index(t => t.CouponRedemptionID, unique: true)
                .Index(t => t.CouponID)
                .Index(t => t.LoginID);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        CouponID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        PercentageDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxDiscount = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Int(),
                        RedemptionPoints = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.CouponID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        Score = c.Int(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.LoginID)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        PersonID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.People", t => t.PersonID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        PersonID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.People", t => t.PersonID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        ShiftID = c.Int(nullable: false),
                        DateWork = c.DateTime(nullable: false),
                        Employee_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Employees", t => t.Employee_PersonID)
                .ForeignKey("dbo.Shifts", t => t.ShiftID, cascadeDelete: true)
                .Index(t => t.ShiftID)
                .Index(t => t.Employee_PersonID);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftID = c.Int(nullable: false, identity: true),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        TotalHours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ShiftID);
            
            CreateTable(
                "dbo.ShipInfoes",
                c => new
                    {
                        ShipInfoID = c.Int(nullable: false, identity: true),
                        ReceiverName = c.String(),
                        ReceiverPhone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ShipInfoID);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        SizeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Capactiy = c.Double(nullable: false),
                        Unit = c.String(),
                    })
                .PrimaryKey(t => t.SizeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductOptions", "SizeID", "dbo.Sizes");
            DropForeignKey("dbo.ProductOptions", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "ProductOptionID", "dbo.ProductOptions");
            DropForeignKey("dbo.OrderHeaders", "StaffLoginID", "dbo.Logins");
            DropForeignKey("dbo.OrderHeaders", "ShipInfoID", "dbo.ShipInfoes");
            DropForeignKey("dbo.OrderDetails", "OrderHeader_OrderHeaderID", "dbo.OrderHeaders");
            DropForeignKey("dbo.OrderHeaders", "CustomerLoginID", "dbo.Logins");
            DropForeignKey("dbo.OrderHeaders", "CouponRedemption_CouponRedemptionID", "dbo.CouponRedemptions");
            DropForeignKey("dbo.Logins", "PersonID", "dbo.People");
            DropForeignKey("dbo.Works", "ShiftID", "dbo.Shifts");
            DropForeignKey("dbo.Works", "Employee_PersonID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Employees", "PersonID", "dbo.People");
            DropForeignKey("dbo.Customers", "PersonID", "dbo.People");
            DropForeignKey("dbo.CouponRedemptions", "LoginID", "dbo.Logins");
            DropForeignKey("dbo.CouponRedemptions", "CouponID", "dbo.Coupons");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Works", new[] { "Employee_PersonID" });
            DropIndex("dbo.Works", new[] { "ShiftID" });
            DropIndex("dbo.Employees", new[] { "RoleID" });
            DropIndex("dbo.Employees", new[] { "PersonID" });
            DropIndex("dbo.Customers", new[] { "PersonID" });
            DropIndex("dbo.Logins", new[] { "PersonID" });
            DropIndex("dbo.CouponRedemptions", new[] { "LoginID" });
            DropIndex("dbo.CouponRedemptions", new[] { "CouponID" });
            DropIndex("dbo.CouponRedemptions", new[] { "CouponRedemptionID" });
            DropIndex("dbo.OrderHeaders", new[] { "CouponRedemption_CouponRedemptionID" });
            DropIndex("dbo.OrderHeaders", new[] { "StaffLoginID" });
            DropIndex("dbo.OrderHeaders", new[] { "CustomerLoginID" });
            DropIndex("dbo.OrderHeaders", new[] { "ShipInfoID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_OrderHeaderID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductOptionID" });
            DropIndex("dbo.ProductOptions", new[] { "SizeID" });
            DropIndex("dbo.ProductOptions", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.Sizes");
            DropTable("dbo.ShipInfoes");
            DropTable("dbo.Shifts");
            DropTable("dbo.Works");
            DropTable("dbo.Roles");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.People");
            DropTable("dbo.Logins");
            DropTable("dbo.Coupons");
            DropTable("dbo.CouponRedemptions");
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.ProductOptions");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
