using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace proyecto.asociacionsolidarista.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly AsociacionSolidaristaDbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(AsociacionSolidaristaDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }
    }
}