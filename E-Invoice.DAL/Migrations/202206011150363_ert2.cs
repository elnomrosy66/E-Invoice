namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ert2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.settings", "Code", c => c.String());
            AddColumn("dbo.settings", "BranchId", c => c.Int(nullable: false));
            AddColumn("dbo.settings", "CreatedBy", c => c.Int());
            AddColumn("dbo.settings", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.settings", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.settings", "IsDelete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.settings", "IsDelete");
            DropColumn("dbo.settings", "IsActive");
            DropColumn("dbo.settings", "CreatedDate");
            DropColumn("dbo.settings", "CreatedBy");
            DropColumn("dbo.settings", "BranchId");
            DropColumn("dbo.settings", "Code");
        }
    }
}
