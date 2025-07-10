using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using InventoryManager.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private const bool pagination=true;
        private const int pageSize = 1;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService productService, IWebHostEnvironment env)
        {
            _productService = productService;
            _env = env;
        }
        public IActionResult Index(string? searchTerm,int page=1,bool desc=false)
        {
            AssignToViewBag(searchTerm, page,desc);
            var products=_productService.GetAllProducts(searchTerm,page:page,pageSize:pageSize, pagination: pagination, descending:desc);

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            (ViewBag.CategoryList,ViewBag.UnitList)=_productService.GetCategoryAndUnit();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = GetProductFromVM(productVM);

                _productService.AddProduct(product);

                return RedirectToAction(nameof(Index));
            }

            (ViewBag.CategoryList, ViewBag.UnitList) = _productService.GetCategoryAndUnit();
            return View(productVM);
        }

        Product GetProductFromVM(ProductVM productVM)
        {
            var imagePath = _productService.ImagePath(productVM.ImageUri, _env.WebRootPath);
            return new Product
            {
                
                Name = productVM.Product.Name,
                Description= productVM.Product.Description,
                Price = productVM.Product.Price,
                CategoryId=productVM.Product.CategoryId,
                UnitId=productVM.Product.UnitId,
                ImagePath= imagePath,
            };
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            (ViewBag.CategoryList, ViewBag.UnitList) = _productService.GetCategoryAndUnit();
            return View(SetVMFromProduct(product));
        }
        ProductVM SetVMFromProduct(Product product)
        {
            return new ProductVM
            {
                Product = product
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Product.ImagePath != null && productVM.ImageUri?.Length > 0)
                {
                    _productService.DeleteImage(productVM.Product.ImagePath,_env.WebRootPath);
                }
                
                var product = GetProductFromVM(productVM);
                product.Id = productVM.Product.Id;

                _productService.UpdateProduct(product);

                return RedirectToAction(nameof(Index));
            }
            (ViewBag.CategoryList, ViewBag.UnitList) = _productService.GetCategoryAndUnit();
            return View(productVM);
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
