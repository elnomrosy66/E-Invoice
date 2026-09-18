using System;
using E_Invoice.DAL.Data;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;

namespace E_Invoice.DAL.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDBContext _context;

	public ICmd<Product> Products { get; private set; }

	public ICmd<Unit> Units { get; private set; }

	public ICmd<Store> Stores { get; private set; }

	public ICmd<Category> Categories { get; private set; }

	public ICmd<Invintory> Invintory { get; private set; }

	public ICmd<OrderDetail> OrderDetails { get; private set; }

	public ICmd<Order> Orders { get; private set; }

	public ICmd<ProductUnites> ProductUnites { get; private set; }

	public ICmd<User> Users { get; private set; }

	public ICmd<Account> Accounts { get; private set; }

	public ICmd<Branch> Branchs { get; private set; }

	public ICmd<Company> Companies { get; private set; }

	public ICmd<setting> Settings { get; private set; }

	public UnitOfWork()
	{
		_context = new ApplicationDBContext();
		Products = new Cmd<Product>(_context);
		Stores = new Cmd<Store>(_context);
		Units = new Cmd<Unit>(_context);
		Categories = new Cmd<Category>(_context);
		Invintory = new Cmd<Invintory>(_context);
		OrderDetails = new Cmd<OrderDetail>(_context);
		Orders = new Cmd<Order>(_context);
		ProductUnites = new Cmd<ProductUnites>(_context);
		Accounts = new Cmd<Account>(_context);
		Branchs = new Cmd<Branch>(_context);
		Companies = new Cmd<Company>(_context);
		Users = new Cmd<User>(_context);
		Settings = new Cmd<setting>(_context);
	}

	public ICmd<T> GetData<T>() where T : class
	{
		return new Cmd<T>(_context);
	}

	public int complete()
	{
		return _context.SaveChanges();
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
