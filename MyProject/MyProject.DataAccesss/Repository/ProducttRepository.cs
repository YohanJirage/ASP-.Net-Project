using MyProject.DataAccess.Data;
using MyProject.DataAccess.Repository.IRepository;
using MyProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccess.Repository
{
    public class ProducttRepository : Repository<Product> ,IProductRepository
    {
        private ApplicationDbContext db;
        public ProducttRepository(ApplicationDbContext context) : base(context)
        {
            db = context;
        }



        public void Update(Product product)
        {
            db.products.Update(product);
        }
    }
}
