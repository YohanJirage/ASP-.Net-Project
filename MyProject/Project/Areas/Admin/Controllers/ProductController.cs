using Microsoft.AspNetCore.Mvc;
using MyProject.DataAccess.Repository.IRepository;
using MyProject.Models.Models;

namespace Project.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork db)
        {
         
            this.unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Product> products = unitOfWork.Product.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Add(p);
                unitOfWork.Save();
                TempData["success"] = "Product Is Added";
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
            Product? editProd = unitOfWork.Product.Get(u => u.Id == id);
            if (editProd == null)
            {
                return NotFound();
            }
            return View(editProd);
        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Update(p);
                unitOfWork.Save();
                TempData["success"] = "Product Is Edited";
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
            Product? delProd = unitOfWork.Product.Get(u => u.Id == id);
            if (delProd == null)
            {
                return NotFound();
            }
            return View(delProd);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletPost(Product c)
        {
            Product? product = unitOfWork.Product.Get(u => u.Id == c.Id);

            if (product != null)
            {
                unitOfWork.Product.Remove(product);
                unitOfWork.Save();
                TempData["success"] = "Product Is Deleted";
                return RedirectToAction("Index");
            }

            return NotFound();

        }
    }
}
