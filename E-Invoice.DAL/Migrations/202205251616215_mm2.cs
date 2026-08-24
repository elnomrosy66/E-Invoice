namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "ProdcutUnitId", "dbo.ProductUnites");
            DropIndex("dbo.OrderDetails", new[] { "ProdcutUnitId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.OrderDetails", "ProdcutUnitId");
            AddForeignKey("dbo.OrderDetails", "ProdcutUnitId", "dbo.ProductUnites", "Id", cascadeDelete: true);
        }
    }
}
