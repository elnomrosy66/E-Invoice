using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace E_Invoice.DAL.Migrations;

[GeneratedCode("EntityFramework.Migrations", "6.4.4")]
public sealed class custst : DbMigration, IMigrationMetadata
{
	private readonly ResourceManager Resources = new ResourceManager(typeof(custst));

	string IMigrationMetadata.Id => "202208231349173_custst";

	string IMigrationMetadata.Source => null;

	string IMigrationMetadata.Target => Resources.GetString("Target");

	public override void Up()
	{
		AddColumn("dbo.Orders", "CustumerStreet", (ColumnBuilder c) => c.String());
		AddColumn("dbo.Orders", "CustumerBuilding", (ColumnBuilder c) => c.String());
	}

	public override void Down()
	{
		DropColumn("dbo.Orders", "CustumerBuilding");
		DropColumn("dbo.Orders", "CustumerStreet");
	}
}
