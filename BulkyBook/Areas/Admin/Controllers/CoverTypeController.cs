using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        // private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCategoryList = _unitOfWork.CoverType.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                // _db.Categories.Add(obj);
                _unitOfWork.CoverType.Add(obj);
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
            var categoryFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
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
        public IActionResult Edit(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                /* _db.Categories.Update(obj);
                 _db.SaveChanges();*/

                _unitOfWork.CoverType.Update(obj);
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

            var categoryFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id); ; // or use the following
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

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
