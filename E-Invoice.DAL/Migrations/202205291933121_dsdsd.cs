namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsdsd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(),
                        ClientSecret = c.String(),
                        TokenPass = c.String(),
                        ProdEnv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.settings");
        }
    }
}
