using MyProject.DataAccess.Data
    ;
using MyProject.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{

    
    
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> CategoryList = _db.categories.ToList();
             
             return View(CategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category c)
        {
            if(ModelState.IsValid)
            {
                _db.categories.Add(c);
                _db.SaveChanges();
                TempData["success"] = "Category Is Created";
                return RedirectToAction("Index");
            }
            return View();
           
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? editCat = _db.categories.Find(id);
             if(editCat == null)
            {
                return NotFound();
            }
            return View(editCat);
        }

        [HttpPost]
        public IActionResult Edit(Category c)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Update(c);
                _db.SaveChanges();
                TempData["success"] = "Category Is Edited";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? editCat = _db.categories.Find(id);
            if (editCat == null)
            {
                return NotFound();
            }
            return View(editCat);
        }

        [HttpPost,ActionName("Delete") ]   
        public IActionResult DeletPost(Category c)
        {
            Category? cat = _db.categories.Find(c.Id);

            if(cat != null)
            {
                _db.categories.Remove(cat);
                _db.SaveChanges();
                TempData["success"] = "Category Is Deleted";
                return RedirectToAction("Index");
            }
           
            return NotFound();

        }
    }
}
