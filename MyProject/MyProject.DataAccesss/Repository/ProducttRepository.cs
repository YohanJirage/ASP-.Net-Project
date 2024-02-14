using MyProject.DataAccess.Data;
using MyProject.DataAccess.Repository.IRepository;
using MyProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
            var prod = db.products.FirstOrDefault(u =>  u.Id == product.Id);
            if(prod != null)
            {
                prod.Title = product.Title;
                prod.Description = product.Description;
                prod.CategoryId = product.CategoryId;
                prod.Price = product.Price;
                prod.ListPrice = product.ListPrice;
                prod.Price50 = product.Price50;
                prod.Price100 = product.Price100;
                prod.ISBN = product.ISBN;
                prod.Author = product.Author;

               if(product.ImageUrl != null )
                {
                    prod.ImageUrl = product.ImageUrl;
                }

            }
            db.products.Update(prod);
        }
    }
}
