using System;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;

namespace E_Invoice.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
	ICmd<Product> Products { get; }

	ICmd<Unit> Units { get; }

	ICmd<Store> Stores { get; }

	ICmd<Category> Categories { get; }

	ICmd<Invintory> Invintory { get; }

	ICmd<OrderDetail> OrderDetails { get; }

	ICmd<Order> Orders { get; }

	ICmd<ProductUnites> ProductUnites { get; }

	ICmd<User> Users { get; }

	ICmd<Account> Accounts { get; }

	ICmd<Branch> Branchs { get; }

	ICmd<Company> Companies { get; }

	ICmd<setting> Settings { get; }

	ICmd<PosDevice> PosDevices { get; }

	ICmd<T> GetData<T>() where T : class;

	int complete();
}
