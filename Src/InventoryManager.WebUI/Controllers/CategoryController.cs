using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private const int pageSize = 1;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index(string? searchTerm,int page=1,bool desc=false)
        {
            AssignToViewBag(searchTerm, page,desc);
            var categories=_categoryService.GetAllCategories(searchTerm,page:page,pageSize:pageSize,descending:desc);

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.RemoveCategory(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public void AssignToViewBag(string? searchTerm, int page, bool desc = false)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Desc = desc;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = _categoryService.TotalPages(searchTerm, pageSize: pageSize);
            (ViewBag.StartPage, ViewBag.EndPage) = _categoryService.GetStartAndEnd(searchTerm, page,pageSize);

        }
    }
}
