namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4545454 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "ActivityCode", c => c.String());
            AddColumn("dbo.Companies", "ActivityCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "ActivityCode");
            DropColumn("dbo.Accounts", "ActivityCode");
        }
    }
}
