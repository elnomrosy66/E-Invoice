namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gpc1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.settings", "UseGpc", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.settings", "UseGpc");
        }
    }
}
