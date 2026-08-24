namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SmallUnitCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SmallUnitCost");
        }
    }
}
