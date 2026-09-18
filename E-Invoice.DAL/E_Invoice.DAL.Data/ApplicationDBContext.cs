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

	private static bool _tablesChecked = false;
	private static readonly object _checkLock = new object();

	public ApplicationDBContext()
		: base("name=LocafflDb")
	{
		Database.SetInitializer<ApplicationDBContext>(null);
		EnsureDatabaseSchema();
	}

	public static void EnsureDatabaseSchema()
	{
		if (_tablesChecked) return;
		lock (_checkLock)
		{
			if (_tablesChecked) return;
			try
			{
				using (var ctx = new ApplicationDBContext(true))
				{
					string sql = @"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PosDevices]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[PosDevices] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Code] NVARCHAR(50) NULL,
        [Name] NVARCHAR(100) NULL,
        [PosName] NVARCHAR(100) NOT NULL,
        [PosCode] NVARCHAR(50) NOT NULL,
        [BranchId] INT NOT NULL DEFAULT(1),
        [DeviceSerialNumber] NVARCHAR(100) NULL,
        [DeviceOSVersion] NVARCHAR(50) NULL,
        [DeviceModel] NVARCHAR(50) NULL,
        [ActivityCode] NVARCHAR(50) NULL,
        [ClientId] NVARCHAR(150) NULL,
        [ClientSecret] NVARCHAR(150) NULL,
        [CurrentSequence] BIGINT NOT NULL DEFAULT(1),
        [ReceiptPrefix] NVARCHAR(20) NOT NULL DEFAULT('REC-'),
        [IsProduction] BIT NOT NULL DEFAULT(0),
        [CreatedDate] DATETIME NULL,
        [CreatedBy] INT NULL,
        [IsDelete] INT NOT NULL DEFAULT(0),
        [IsActive] BIT NOT NULL DEFAULT(1),
        CONSTRAINT [PK_dbo.PosDevices] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'PosDeviceId')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [PosDeviceId] INT NULL;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'ReceiptNumber')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [ReceiptNumber] BIGINT NULL;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'FullReceiptNumber')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [FullReceiptNumber] NVARCHAR(100) NULL;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'PaymentMethod')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [PaymentMethod] NVARCHAR(50) NULL;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'PreviousUUID')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [PreviousUUID] NVARCHAR(150) NULL;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'ReferenceOldUUID')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [ReferenceOldUUID] NVARCHAR(150) NULL;
END
";
					ctx.Database.ExecuteSqlCommand(sql);
				}
			}
			catch
			{
			}
			finally
			{
				_tablesChecked = true;
			}
		}
	}

	private ApplicationDBContext(bool internalInit)
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
