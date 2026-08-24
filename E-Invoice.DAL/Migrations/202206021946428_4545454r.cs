namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4545454r : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "CanonicalType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "CanonicalType");
        }
    }
}
