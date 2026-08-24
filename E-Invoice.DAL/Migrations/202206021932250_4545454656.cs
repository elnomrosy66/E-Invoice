namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4545454656 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Address", c => c.String());
            AddColumn("dbo.Accounts", "Phone", c => c.String());
            AddColumn("dbo.Accounts", "Email", c => c.String());
            AddColumn("dbo.Companies", "Address", c => c.String());
            AddColumn("dbo.Companies", "Phone", c => c.String());
            AddColumn("dbo.Companies", "Email", c => c.String());
            AddColumn("dbo.Products", "IsService", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsService");
            DropColumn("dbo.Companies", "Email");
            DropColumn("dbo.Companies", "Phone");
            DropColumn("dbo.Companies", "Address");
            DropColumn("dbo.Accounts", "Email");
            DropColumn("dbo.Accounts", "Phone");
            DropColumn("dbo.Accounts", "Address");
        }
    }
}
