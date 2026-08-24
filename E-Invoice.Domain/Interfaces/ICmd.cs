using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace E_Invoice.Domain.Interfaces
{
    public interface ICmd<T> where T : class
    {
        T GetTById(int id);
        IEnumerable<T> GetAll();
        T GetTBy(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAllBy(Expression<Func<T, bool>> expression , string[] includes = null);

        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);

        T Update(T entity);
        void Delete(T entity);
        T Attach(T entity);
        int count();
        int count(Expression<Func<T, bool>> expression);

        long GetMaxInv(Expression<Func<T, bool>> expression , Func<T, long> columnSelector);

    }
}
