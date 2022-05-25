using Microsoft.AspNetCore.Mvc;
using stock_management.Models;

namespace stock_management.Data.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //GET action CREATE
        public IActionResult Create()
        {
             return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot match the Name!");
            }
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The DisplayOrder cannot match the Name!");
            }
            if (ModelState.IsValid) { 
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET action EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot match the Name!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update (obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET action DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }


    } 
}
