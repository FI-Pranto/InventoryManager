using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private const bool pagination = true;
        private const int pageSize = 5;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public IActionResult Index(string? searchTerm,int page=1,bool desc=false)
        {
            AssignToViewBag(searchTerm, page,desc);
            var suppliers=_supplierService.GetAllSuppliers(searchTerm,page:page,pageSize:pageSize, pagination: pagination,descending:desc);

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _supplierService.AddSupplier(supplier);

                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _supplierService.UpdateSupplier(supplier);

                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _supplierService.RemoveSupplier(supplier);

                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }
        public void AssignToViewBag(string? searchTerm, int page, bool desc = false)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Desc = desc;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = _supplierService.TotalPages(searchTerm, pageSize: pageSize);
            (ViewBag.StartPage, ViewBag.EndPage) = _supplierService.GetStartAndEnd(searchTerm, page,pageSize);

        }
    }
}
