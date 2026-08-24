namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gpc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "GPCCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "GPCCode");
        }
    }
}
