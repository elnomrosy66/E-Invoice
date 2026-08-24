namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateSent", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "DateSent");
        }
    }
}
