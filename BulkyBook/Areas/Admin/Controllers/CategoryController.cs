using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) // Custom Validation
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
                // ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                // _db.Categories.Add(obj);
                _unitOfWork.Category.Add(obj);
                // _db.SaveChanges();
                _unitOfWork.Save();
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // var categoryFromDb = _db.Categories.Find(id); // or use the following
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            /*var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) // Custom Validation
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
                // ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                /* _db.Categories.Update(obj);
                 _db.SaveChanges();*/

                _unitOfWork.Category.Update(obj);
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

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id); ; // or use the following
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

            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
