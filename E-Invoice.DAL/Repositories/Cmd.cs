using E_Invoice.DAL.Data;
using E_Invoice.Domain;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.DAL.Repositories
{
    public class Cmd<T> : ICmd<T> where T : class
    {
        private readonly ApplicationDBContext _context;//

        private readonly DbSet<T> dbSet;

        public Cmd()
        {
            _context = new ApplicationDBContext();
            dbSet =_context.Set<T>();

        }

        public T GetTBy(Expression<Func<T, bool>> expression)
        {
            return dbSet.SingleOrDefault(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable().Cast<Base>().Where(GetActive()).Cast<T>().ToList();
        }

        public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> expression , string[] includes = null)
        {
            IQueryable<T> query = dbSet;
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.Where(expression).ToList();
        }

        public T GetTById(int id)
        {
            return dbSet.Find(id);
        }



        private Func<Base, bool> GetActive()
        {
            return x => x.IsDelete == IsDelete.Active;
            //&& x.CorporationId == Info.Corporation.Id;
        }

        public T Add(T entity)
        {
            dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public T Update(T entity)
        {
            //if (entity == null) return false;
            var x = entity as Base;
            var existingEntity = dbSet.Find(x.Id);
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public int count()
        {
            return dbSet.Count();
        }

        public int count(Expression<Func<T, bool>> expression)
        {
            return dbSet.Count(expression);
        }

        public long GetMaxInv(Expression<Func<T, bool>> expression , Func<T, long> columnSelector)
        {
            var GetMaxId = _context.Set<T>().Where(expression).Max(columnSelector);
            return GetMaxId;
        }
        
    }
}
