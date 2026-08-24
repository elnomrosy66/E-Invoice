namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerNakdy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "CustumerName", c => c.String());
            AddColumn("dbo.Orders", "CustumerCity", c => c.String());
            AddColumn("dbo.Orders", "CustumerGovernate", c => c.String());
            AddColumn("dbo.Orders", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerId");
            DropColumn("dbo.Orders", "CustumerGovernate");
            DropColumn("dbo.Orders", "CustumerCity");
            DropColumn("dbo.Orders", "CustumerName");
            DropColumn("dbo.Orders", "CustomerNakdy");
        }
    }
}
