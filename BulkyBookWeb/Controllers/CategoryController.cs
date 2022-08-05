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
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        //POST; [ValidateAntiForgeryToken]-預防跨域攻擊
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // [TryAddModelError] keyParam: 可制定或對應到 CategoryModel 的 Key Name
                ModelState.TryAddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            var category = _db.Categories.Find(id);
            if (id == null || id == 0 || category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // [TryAddModelError] keyParam: 可制定或對應到 CategoryModel 的 Key Name
                ModelState.TryAddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edit successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        // GET
        public IActionResult Delete(int? id)
        {
            var category = _db.Categories.Find(id);
            if (id == null || id == 0 || category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
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
