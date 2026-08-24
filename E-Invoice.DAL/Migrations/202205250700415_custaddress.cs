namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class custaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "TaxReg", c => c.String());
            AddColumn("dbo.Accounts", "CommercialRegNo", c => c.String());
            AddColumn("dbo.Accounts", "Country", c => c.String());
            AddColumn("dbo.Accounts", "Governate", c => c.String());
            AddColumn("dbo.Accounts", "RegionCity", c => c.String());
            AddColumn("dbo.Accounts", "Street", c => c.String());
            AddColumn("dbo.Accounts", "BuildingNumber", c => c.String());
            AddColumn("dbo.Accounts", "PostalCode", c => c.String());
            AddColumn("dbo.Accounts", "Floor", c => c.String());
            AddColumn("dbo.Accounts", "Room", c => c.String());
            AddColumn("dbo.Accounts", "Landmark", c => c.String());
            AddColumn("dbo.Accounts", "AdditionalInformation", c => c.String());
            DropColumn("dbo.Accounts", "Phone");
            DropColumn("dbo.Accounts", "Phone2");
            DropColumn("dbo.Accounts", "Address");
            DropColumn("dbo.Accounts", "email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "email", c => c.String());
            AddColumn("dbo.Accounts", "Address", c => c.String());
            AddColumn("dbo.Accounts", "Phone2", c => c.String());
            AddColumn("dbo.Accounts", "Phone", c => c.String());
            DropColumn("dbo.Accounts", "AdditionalInformation");
            DropColumn("dbo.Accounts", "Landmark");
            DropColumn("dbo.Accounts", "Room");
            DropColumn("dbo.Accounts", "Floor");
            DropColumn("dbo.Accounts", "PostalCode");
            DropColumn("dbo.Accounts", "BuildingNumber");
            DropColumn("dbo.Accounts", "Street");
            DropColumn("dbo.Accounts", "RegionCity");
            DropColumn("dbo.Accounts", "Governate");
            DropColumn("dbo.Accounts", "Country");
            DropColumn("dbo.Accounts", "CommercialRegNo");
            DropColumn("dbo.Accounts", "TaxReg");
        }
    }
}
