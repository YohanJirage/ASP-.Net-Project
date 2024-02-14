using MyProject.DataAccess.Data;
using MyProject.DataAccess.Repository.IRepository;
using MyProject.Models.Models;
using Microsoft.AspNetCore.Mvc;
using MyProject.DataAccess.Repository;

namespace Project.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> CategoryList = unitOfWork.Category.GetAll().ToList();

            return View(CategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(c);
                unitOfWork.Save();
                TempData["success"] = "Category Is Created";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? editCat = unitOfWork.Category.Get(u => u.Id == id);
            if (editCat == null)
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
                unitOfWork.Category.Update(c);
                unitOfWork.Save();
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
            Category? editCat = unitOfWork.Category.Get(u => u.Id == id);
            if (editCat == null)
            {
                return NotFound();
            }
            return View(editCat);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletPost(Category c)
        {
            Category? cat = unitOfWork.Category.Get(u => u.Id == c.Id);

            if (cat != null)
            {
                unitOfWork.Category.Remove(cat);
                unitOfWork.Save();
                TempData["success"] = "Category Is Deleted";
                return RedirectToAction("Index");
            }

            return NotFound();

        }
    }
}
