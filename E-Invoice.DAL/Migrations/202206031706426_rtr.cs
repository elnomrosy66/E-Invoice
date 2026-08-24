namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rtr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DiscountRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DiscountRate");
        }
    }
}
