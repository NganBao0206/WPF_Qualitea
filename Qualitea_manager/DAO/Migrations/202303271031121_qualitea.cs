namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qualitea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderHeader_OrderHeaderID", "dbo.OrderHeaders");
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_OrderHeaderID" });
            RenameColumn(table: "dbo.OrderDetails", name: "OrderHeader_OrderHeaderID", newName: "OrderHeaderID");
            AlterColumn("dbo.OrderDetails", "OrderHeaderID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderHeaderID");
            AddForeignKey("dbo.OrderDetails", "OrderHeaderID", "dbo.OrderHeaders", "OrderHeaderID", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "OrderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "OrderID", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "OrderHeaderID", "dbo.OrderHeaders");
            DropIndex("dbo.OrderDetails", new[] { "OrderHeaderID" });
            AlterColumn("dbo.OrderDetails", "OrderHeaderID", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "OrderHeaderID", newName: "OrderHeader_OrderHeaderID");
            CreateIndex("dbo.OrderDetails", "OrderHeader_OrderHeaderID");
            AddForeignKey("dbo.OrderDetails", "OrderHeader_OrderHeaderID", "dbo.OrderHeaders", "OrderHeaderID");
        }
    }
}
