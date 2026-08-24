namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffinal2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "sent", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductUnites", "ProductId");
            AddForeignKey("dbo.ProductUnites", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "ProductUnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductUnitId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductUnites", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductUnites", new[] { "ProductId" });
            AlterColumn("dbo.Orders", "sent", c => c.Boolean(nullable: false));
        }
    }
}
