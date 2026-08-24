namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wwe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Country", c => c.String());
            AddColumn("dbo.Branches", "Governate", c => c.String());
            AddColumn("dbo.Branches", "RegionCity", c => c.String());
            AddColumn("dbo.Branches", "Street", c => c.String());
            AddColumn("dbo.Branches", "BuildingNumber", c => c.String());
            AddColumn("dbo.Branches", "PostalCode", c => c.String());
            AddColumn("dbo.Branches", "Floor", c => c.String());
            AddColumn("dbo.Branches", "Room", c => c.String());
            AddColumn("dbo.Branches", "Landmark", c => c.String());
            AddColumn("dbo.Branches", "AdditionalInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "AdditionalInformation");
            DropColumn("dbo.Branches", "Landmark");
            DropColumn("dbo.Branches", "Room");
            DropColumn("dbo.Branches", "Floor");
            DropColumn("dbo.Branches", "PostalCode");
            DropColumn("dbo.Branches", "BuildingNumber");
            DropColumn("dbo.Branches", "Street");
            DropColumn("dbo.Branches", "RegionCity");
            DropColumn("dbo.Branches", "Governate");
            DropColumn("dbo.Branches", "Country");
        }
    }
}
