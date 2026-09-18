using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;

namespace E_Invoice.DAL.Data;

public class ApplicationDBContext : DbContext
{
	public DbSet<User> users { get; set; }

	public DbSet<Category> categories { get; set; }

	public DbSet<Unit> units { get; set; }

	public DbSet<Store> stores { get; set; }

	public DbSet<Product> products { get; set; }

	public DbSet<Account> accounts { get; set; }

	public DbSet<Branch> branches { get; set; }

	public DbSet<Order> orders { get; set; }

	public DbSet<OrderDetail> orderDetails { get; set; }

	public DbSet<Invintory> invintories { get; set; }

	public DbSet<ProductUnites> productUnites { get; set; }

	public DbSet<Company> companies { get; set; }

	public DbSet<setting> settings { get; set; }

	public DbSet<PosDevice> PosDevices { get; set; }

	public ApplicationDBContext()
		: base("name=LocafflDb")
	{
		Database.SetInitializer<ApplicationDBContext>(null);
	}

	public override int SaveChanges()
	{
		IEnumerable<DbEntityEntry> enumerable = from x in base.ChangeTracker.Entries()
			where x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified)
			select x;
		foreach (DbEntityEntry item in enumerable)
		{
			if (item.State == EntityState.Added)
			{
				((Base)item.Entity).CreatedDate = DateTime.Now;
				((Base)item.Entity).CreatedBy = ((Info.CurrentUser == null) ? 1 : Info.CurrentUser.Id);
				((Base)item.Entity).IsDelete = IsDelete.Active;
				((Base)item.Entity).IsActive = true;
				((Base)item.Entity).BranchId = ((Info.CurrenBranch == null) ? 1 : Info.CurrenBranch.Id);
			}
		}
		return base.SaveChanges();
	}
}
