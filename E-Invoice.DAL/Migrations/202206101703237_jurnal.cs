namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jurnal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Cost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Cost");
        }
    }
}
