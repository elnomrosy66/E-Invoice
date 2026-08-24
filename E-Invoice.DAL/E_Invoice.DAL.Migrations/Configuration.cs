using System.Data.Entity.Migrations;
using E_Invoice.DAL.Data;

namespace E_Invoice.DAL.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDBContext>
{
	public Configuration()
	{
		base.AutomaticMigrationsEnabled = false;
	}

	protected override void Seed(ApplicationDBContext context)
	{
	}
}
