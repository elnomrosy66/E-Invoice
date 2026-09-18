using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using E_Invoice.DAL.Data;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domian.Models;

namespace E_Invoice.DAL.Repositories;

public class Cmd<T> : ICmd<T> where T : class
{
	private readonly ApplicationDBContext _context;

	private readonly DbSet<T> dbSet;

	public Cmd(ApplicationDBContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		dbSet = _context.Set<T>();
	}

	public Cmd()
	{
		_context = new ApplicationDBContext();
		dbSet = _context.Set<T>();
	}

	public T GetTBy(Expression<Func<T, bool>> expression)
	{
		return dbSet.SingleOrDefault(expression);
	}

	public IEnumerable<T> GetAll()
	{
		if (typeof(Base).IsAssignableFrom(typeof(T)))
		{
			var param = Expression.Parameter(typeof(T), "x");
			var prop = Expression.Property(Expression.Convert(param, typeof(Base)), nameof(Base.IsDelete));
			var constVal = Expression.Constant(IsDelete.Active);
			var body = Expression.Equal(prop, constVal);
			var lambda = Expression.Lambda<Func<T, bool>>(body, param);
			return dbSet.Where(lambda).ToList();
		}
		return dbSet.ToList();
	}

	public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> expression, string[] includes = null)
	{
		IQueryable<T> source = dbSet;
		if (includes != null)
		{
			foreach (string path in includes)
			{
				source = source.Include(path);
			}
		}
		return source.Where(expression).ToList();
	}

	public T GetTById(int id)
	{
		return dbSet.Find(id);
	}

	private Func<Base, bool> GetActive()
	{
		return (Base x) => x.IsDelete == IsDelete.Active;
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
		Base obj = entity as Base;
		T entity2 = dbSet.Find(obj.Id);
		_context.Entry(entity2).CurrentValues.SetValues(entity);
		_context.SaveChanges();
		return entity;
	}

	public void Delete(T entity)
	{
		dbSet.Remove(entity);
		_context.SaveChanges();
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

	public long GetMaxInv(Expression<Func<T, bool>> expression, Func<T, long> columnSelector)
	{
		return _context.Set<T>().Where(expression).Max(columnSelector);
	}
}
