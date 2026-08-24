namespace E_Invoice.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountType = c.Int(nullable: false),
                        CridetLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InitialCridet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Phone = c.String(),
                        Phone2 = c.String(),
                        Address = c.String(),
                        email = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Phone2 = c.String(),
                        Address = c.String(),
                        email = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxReg = c.String(),
                        CommercialRegNo = c.String(),
                        Country = c.String(),
                        Governate = c.String(),
                        RegionCity = c.String(),
                        Street = c.String(),
                        BuildingNumber = c.String(),
                        PostalCode = c.String(),
                        Floor = c.String(),
                        Room = c.String(),
                        Landmark = c.String(),
                        AdditionalInformation = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invintories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        StoreId = c.Int(nullable: false),
                        StoreToId = c.Int(),
                        OrderNumber = c.Int(nullable: false),
                        ProductUnitId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        ExpierDate = c.DateTime(),
                        ProductionDate = c.DateTime(),
                        Qty = c.Double(nullable: false),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.ProductId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        itemType = c.String(),
                        itemCode = c.String(),
                        requestLimit = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ProductUnitId = c.Int(nullable: false),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ProductUnites", t => t.ProductUnitId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ProductUnitId);
            
            CreateTable(
                "dbo.ProductUnites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        UnitConvert = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QtySmallUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Barcode = c.String(),
                        Avg = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(),
                        ProdcutUnitId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        QtyConvert = c.Double(nullable: false),
                        QtySum = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Extra = c.Double(nullable: false),
                        NetBeforeTax = c.Double(nullable: false),
                        Vat = c.Double(nullable: false),
                        VatPrice = c.Double(nullable: false),
                        NetAfterTax = c.Double(nullable: false),
                        AvgPrice = c.Double(nullable: false),
                        TotalAvgPrice = c.Double(nullable: false),
                        Profits = c.Double(nullable: false),
                        ExpireDate = c.DateTime(),
                        ProductionDate = c.DateTime(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.ProductUnites", t => t.ProdcutUnitId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId)
                .Index(t => t.ProdcutUnitId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderBarcode = c.String(),
                        OrderNumber = c.Long(nullable: false),
                        OrderType = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        OrderPayment = c.Int(nullable: false),
                        bankTreasuryId = c.Long(),
                        AccountId = c.Int(),
                        CurrencyId = c.Long(nullable: false),
                        CurrencyRate = c.Double(nullable: false),
                        StoreId = c.Int(nullable: false),
                        ToStoreId = c.Long(),
                        NetBeforeTax = c.Double(nullable: false),
                        TotalVat = c.Double(nullable: false),
                        TotalDiscount = c.Double(nullable: false),
                        TotalExtra = c.Double(nullable: false),
                        NetInvoice = c.Double(nullable: false),
                        Paid = c.Double(nullable: false),
                        Rest = c.Double(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        TotalProfit = c.Double(nullable: false),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        IsAdmin = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        Code = c.String(),
                        BranchId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProdcutUnitId", "dbo.ProductUnites");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Invintories", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Invintories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductUnitId", "dbo.ProductUnites");
            DropForeignKey("dbo.ProductUnites", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Invintories", "BranchId", "dbo.Branches");
            DropIndex("dbo.Orders", new[] { "StoreId" });
            DropIndex("dbo.Orders", new[] { "AccountId" });
            DropIndex("dbo.OrderDetails", new[] { "ProdcutUnitId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.ProductUnites", new[] { "UnitId" });
            DropIndex("dbo.Products", new[] { "ProductUnitId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Invintories", new[] { "BranchId" });
            DropIndex("dbo.Invintories", new[] { "ProductId" });
            DropIndex("dbo.Invintories", new[] { "StoreId" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Stores");
            DropTable("dbo.Units");
            DropTable("dbo.ProductUnites");
            DropTable("dbo.Products");
            DropTable("dbo.Invintories");
            DropTable("dbo.Companies");
            DropTable("dbo.Categories");
            DropTable("dbo.Branches");
            DropTable("dbo.Accounts");
        }
    }
}
