using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private const int pageSize = 1;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index(string? searchTerm,int page=1,bool desc=false)
        {
            AssignToViewBag(searchTerm, page,desc);
            var products=_productService.GetAllProducts(searchTerm,page:page,pageSize:pageSize,descending:desc);

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.RemoveProduct(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }



        public void AssignToViewBag(string? searchTerm, int page, bool desc = false)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Desc = desc;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = _productService.TotalPages(searchTerm, pageSize: pageSize);
            (ViewBag.StartPage, ViewBag.EndPage) = _productService.GetStartAndEnd(searchTerm, page,pageSize);

        }
    }
}
