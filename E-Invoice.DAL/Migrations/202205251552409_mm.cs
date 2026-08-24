namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "uuid", c => c.String());
            AddColumn("dbo.Orders", "sent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "userSent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "userSent");
            DropColumn("dbo.Orders", "sent");
            DropColumn("dbo.Orders", "uuid");
        }
    }
}
