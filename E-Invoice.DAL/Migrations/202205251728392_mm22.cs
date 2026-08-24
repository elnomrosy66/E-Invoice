namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invintories", "Cost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invintories", "Cost");
        }
    }
}
