namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm222 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invintories", "BranchId", "dbo.Branches");
            DropIndex("dbo.Invintories", new[] { "BranchId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Invintories", "BranchId");
            AddForeignKey("dbo.Invintories", "BranchId", "dbo.Branches", "Id", cascadeDelete: true);
        }
    }
}
