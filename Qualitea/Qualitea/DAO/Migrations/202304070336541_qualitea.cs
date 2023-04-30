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
                        Size = c.String(),
                        Price = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductOptionID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        OrderHeaderID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalLine = c.Double(nullable: false),
                        ProductOptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.OrderHeaders", t => t.OrderHeaderID, cascadeDelete: true)
                .ForeignKey("dbo.ProductOptions", t => t.ProductOptionID, cascadeDelete: true)
                .Index(t => t.OrderHeaderID)
                .Index(t => t.ProductOptionID);
            
            CreateTable(
                "dbo.OrderHeaders",
                c => new
                    {
                        OrderHeaderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                        DiscountTotal = c.Double(nullable: false),
                        IsOnlineOrder = c.Boolean(nullable: false),
                        ShipInfoID = c.Int(),
                        Status = c.Int(nullable: false),
                        CustomerID = c.Int(),
                        EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderHeaderID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.ShipInfoes", t => t.ShipInfoID)
                .Index(t => t.ShipInfoID)
                .Index(t => t.CustomerID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Email = c.String(),
                        Score = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Email = c.String(),
                        RoleID = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        IsEmployed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
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
                "dbo.ShipInfoes",
                c => new
                    {
                        ShipInfoID = c.Int(nullable: false, identity: true),
                        ReceiverName = c.String(),
                        ReceiverPhone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ShipInfoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductOptions", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "ProductOptionID", "dbo.ProductOptions");
            DropForeignKey("dbo.OrderHeaders", "ShipInfoID", "dbo.ShipInfoes");
            DropForeignKey("dbo.OrderDetails", "OrderHeaderID", "dbo.OrderHeaders");
            DropForeignKey("dbo.Employees", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.OrderHeaders", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.OrderHeaders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Employees", new[] { "RoleID" });
            DropIndex("dbo.OrderHeaders", new[] { "EmployeeID" });
            DropIndex("dbo.OrderHeaders", new[] { "CustomerID" });
            DropIndex("dbo.OrderHeaders", new[] { "ShipInfoID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductOptionID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderHeaderID" });
            DropIndex("dbo.ProductOptions", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.ShipInfoes");
            DropTable("dbo.Roles");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.ProductOptions");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
