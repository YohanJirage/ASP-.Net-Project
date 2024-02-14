using MyProject.DataAccess.Data;
using MyProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        private ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext context)  
        {
            db = context;
            Category = new CategoryRepository(db);
            Product = new ProducttRepository(db);
        }
        public void Save()
        {
            db.SaveChanges();
        }  
    }
}
