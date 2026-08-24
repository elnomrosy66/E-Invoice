namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductUnitId", "dbo.ProductUnites");
            DropIndex("dbo.Products", new[] { "ProductUnitId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Products", "ProductUnitId");
            AddForeignKey("dbo.Products", "ProductUnitId", "dbo.ProductUnites", "Id", cascadeDelete: true);
        }
    }
}
