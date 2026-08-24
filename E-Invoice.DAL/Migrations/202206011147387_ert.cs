namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.settings", "Signer", c => c.Boolean(nullable: false));
            AddColumn("dbo.settings", "UseStoreBalance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.settings", "UseStoreBalance");
            DropColumn("dbo.settings", "Signer");
        }
    }
}
