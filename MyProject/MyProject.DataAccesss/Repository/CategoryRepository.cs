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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            db =context;
        }

       

        public void Update(Category category)
        {
             db.categories.Update(category);
        }
    }
}
