using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICmd<T> GetData<T>() where T : class;
        ICmd<Product> Products { get;}
        ICmd<Unit> Units { get; }
        ICmd<Store> Stores { get;}
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

        int complete();


    }
}
