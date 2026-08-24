using E_Invoice.DAL.Data;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
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
            this._context = new ApplicationDBContext();
            Products = new Cmd<Product>();
            Stores = new Cmd<Store>();
            Units = new Cmd<Unit>();
            Categories = new Cmd<Category>();
            Invintory = new Cmd<Invintory>();
            OrderDetails = new Cmd<OrderDetail>();
            Orders = new Cmd<Order>();
            ProductUnites = new Cmd<ProductUnites>();
            Categories = new Cmd<Category>();
            Accounts = new Cmd<Account>();
            Branchs = new Cmd<Branch>();
            Companies = new Cmd<Company>();
            Users = new Cmd<User>();
            Accounts = new Cmd<Account>();
            Accounts = new Cmd<Account>();
            Settings = new Cmd<setting>();

        }

        public ICmd<T> GetData<T>() where T : class
        {

            return new Cmd<T>();
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
}
