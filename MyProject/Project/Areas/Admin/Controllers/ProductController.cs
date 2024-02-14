using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.DataAccess.Repository.IRepository;
using MyProject.Models.Models;
using MyProject.Models.ViewModel;
using System.Collections.Generic;

namespace Project.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public readonly IWebHostEnvironment webHostEnvironment;
        private object objProductList;

        public ProductController(IUnitOfWork db,IWebHostEnvironment webHost)
        {
         
            this.unitOfWork = db;
            webHostEnvironment = webHost;
        }
        public IActionResult Index()
        {
            List<Product> products = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
          
            return View(products);
        }

        public IActionResult Create()
        {
            
           
                ProductVM productVM = new ProductVM()
                {
                    CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),

                    }),
                    Product = new Product()
                };
            
            
           

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM p)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Add(p.Product);
                unitOfWork.Save();
                TempData["success"] = "Product Is Added";
                return RedirectToAction("Index");
            }
            else
            {
                p.CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                });
                return View(p);

            }
           

        }

        public IActionResult UpSert(int? id)  // Update+Insert
        {


            ProductVM productVM = new ProductVM()
            {
                CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = unitOfWork.Product.Get(u => u.Id == id);
                //if (productVM.Product == null)
                //{
                //    return NotFound();
                //}
                return View(productVM);

            }
          
        }

        [HttpPost]
        public IActionResult UpSert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");


                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldlmagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldlmagePath))
                        {
                            System.IO.File.Delete(oldlmagePath);
                        }
                    }
 

                   
                    using ( var fileStream  = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\Images\Product\" + fileName; 
                }
                //else
                //{
                //    productVM.Product.ImageUrl = unitOfWork.Product.Get(u => u.Id == productVM.Product.Id).ImageUrl;
                //}

                if(productVM.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product Is Added";
                }
                else
                {
                    unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "Product Is Edited";
                }

             
                unitOfWork.Save();
              
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                });

                return View(productVM);

            }
            return View();
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? editProd = unitOfWork.Product.Get(u => u.Id == id);
        //    if (editProd == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(editProd);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product p)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        unitOfWork.Product.Update(p);
        //        unitOfWork.Save();
        //        TempData["success"] = "Product Is Edited";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

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


        #region API CALL
        [HttpGet]
        public IActionResult GetAll( )
        {
            
                List<Product> products = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

                return Json(new {data = products });
            
        }

        #endregion
    }
}
