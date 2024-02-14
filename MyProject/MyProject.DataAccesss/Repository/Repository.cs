using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyProject.DataAccess.Data;
using MyProject.DataAccess.Repository.IRepository;

namespace MyProject.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        public DbSet<T> dbSet;

        public Repository(ApplicationDbContext context) 
        {  
            _context = context;
            this.dbSet = _context.Set<T>();
            _context.products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate, string? includeProperties = null) 
        {
            IQueryable<T> query = dbSet;
            query = query.Where(predicate);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }


            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           dbSet.RemoveRange(entities);
        }
    }
}
