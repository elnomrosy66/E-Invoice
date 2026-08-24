namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yytrew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "ParentId", c => c.Int(nullable: false));
            AddColumn("dbo.Accounts", "Nature", c => c.Int(nullable: false));
            AddColumn("dbo.Accounts", "HasParent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "HasParent");
            DropColumn("dbo.Accounts", "Nature");
            DropColumn("dbo.Accounts", "ParentId");
        }
    }
}
