using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomMatchError", "The Name must not be same as Display order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Created category successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var categoryObjFromDb = _db.Categories.Find(id);
            // var categoryObjFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);
            // var categoryObjFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);

            if (categoryObjFromDb == null)
            {
                return NotFound();
            }

            return View(categoryObjFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomMatchError", "The Name must not be same as Display order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Updated category successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var categoryObjFromDb = _db.Categories.Find(id);

            if (categoryObjFromDb == null)
            {
                return NotFound();
            }

            return View(categoryObjFromDb);
        }

        //POST
        [HttpPost]
        //[ActionName("Delete")] //if asp-action is not given in form of Delete view. To remove method name conflicts
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryObjFromDb = _db.Categories.Find(id);

            if (categoryObjFromDb == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryObjFromDb);
            _db.SaveChanges();
            TempData["success"] = "Deleted category successfully";
            return RedirectToAction("Index");
        }
    }
}