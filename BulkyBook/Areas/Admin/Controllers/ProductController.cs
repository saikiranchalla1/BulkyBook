using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objCategoryList = _unitOfWork.Product.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            
            if (ModelState.IsValid)
            {
                // _db.Categories.Add(obj);
                _unitOfWork.Product.Add(obj);
                // _db.SaveChanges();
                _unitOfWork.Save();
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<SelectListItem> CategoryItemsList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            IEnumerable<SelectListItem> CoverTypeItemsList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            if (id == null || id == 0)
            {
                
                ViewBag.CategoryList = CategoryItemsList;

                ViewData["CoverTypeList"] = CoverTypeItemsList;
                return View(product);
            }
            else
            {
                
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {
           
            if (ModelState.IsValid)
            {
                /* _db.Categories.Update(obj);
                 _db.SaveChanges();*/

                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id); ; // or use the following
            /*var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
