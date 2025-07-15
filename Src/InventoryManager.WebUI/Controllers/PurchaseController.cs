using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Application.Services;
using InventoryManager.Domain.Entities;
using InventoryManager.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace InventoryManager.WebUI.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseItemService _purchaseItemService;
        private const bool pagination = true;
        private const int pageSize = 1;

        public PurchaseController(IPurchaseService purchaseService,IPurchaseItemService purchaseItemService)
        {
            _purchaseService = purchaseService;
            _purchaseItemService = purchaseItemService;
        }
        public IActionResult Index(string? searchTerm, int page = 1, bool desc = false)
        {
            AssignToViewBag(searchTerm, page, desc);
            var purchases = _purchaseService.GetAllPurchases(searchTerm,includeProp:"Supplier", page: page, pageSize: pageSize, pagination: pagination, descending: desc);

            return View(purchases);
        }

        public IActionResult Create()
        {
            (ViewBag.Suppliers,ViewBag.Products)=_purchaseService.GetSupplierAndProduct();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PurchaseVM purchaseVM)
        {
            if (ModelState.IsValid)
            {

                var purchase = new Purchase()
                {
                    SupplierId = purchaseVM.SupplierId,
                    PurchaseDate = purchaseVM.PurchaseDate,
                    TotalAmount = purchaseVM.PurchaseItems.Sum(u=>u.UnitPrice*u.Quantity),
                    PurchaseItems=purchaseVM.PurchaseItems.Select(u=> new PurchaseItem
                    {
                        ProductId=u.ProductId,
                        Quantity = u.Quantity,
                        UnitPrice=u.UnitPrice,


                    }).ToList()

                };
                _purchaseService.AddPurchase(purchase);
                return RedirectToAction("Index");
            }
            (ViewBag.Suppliers, ViewBag.Products) = _purchaseService.GetSupplierAndProduct();
            return View(purchaseVM);
        }

        public IActionResult Edit(int? id)
        {
            var purchase=_purchaseService.GetPurchaseById(id,includeProp:"PurchaseItems");

            if(purchase == null) return NotFound();

            var purchaseVM = new PurchaseVM()
            {
                Id=purchase.Id,
                SupplierId=purchase.SupplierId,
                PurchaseDate=purchase.PurchaseDate,
                PurchaseItems=purchase.PurchaseItems.Select(u=> new PurchaseItemVM
                {
                    ProductId=u.ProductId,
                    Id=u.Id,
                    Quantity=u.Quantity,
                    UnitPrice=u.UnitPrice,

                }).ToList()

            };
            (ViewBag.Suppliers, ViewBag.Products) = _purchaseService.GetSupplierAndProduct();

            return View(purchaseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PurchaseVM purchaseVM)
        {
            if (ModelState.IsValid)
            {

                var purchase = new Purchase()
                {
                    Id = purchaseVM.Id,
                    SupplierId = purchaseVM.SupplierId,
                    PurchaseDate = purchaseVM.PurchaseDate,
                    TotalAmount = purchaseVM.PurchaseItems.Sum(u => u.UnitPrice * u.Quantity),
                    PurchaseItems = purchaseVM.PurchaseItems.Select(u => new PurchaseItem
                    {
                        Id = u.Id,
                        ProductId = u.ProductId,
                        Quantity = u.Quantity,
                        UnitPrice = u.UnitPrice,


                    }).ToList()

                };
                _purchaseService.UpdatePurchase(purchase);
                return RedirectToAction("Index");

            }
            (ViewBag.Suppliers, ViewBag.Products) = _purchaseService.GetSupplierAndProduct();
            return View(purchaseVM);
        }

        public IActionResult Delete(int? id)
        {
            var purchase = _purchaseService.GetPurchaseById(id, includeProp: "PurchaseItems");

            if (purchase == null) return NotFound();

            var purchaseVM = new PurchaseVM()
            {
                Id = purchase.Id,
                SupplierId = purchase.SupplierId,
                PurchaseDate = purchase.PurchaseDate,
                PurchaseItems = purchase.PurchaseItems.Select(u => new PurchaseItemVM
                {
                    ProductId = u.ProductId,
                    Id = u.Id,
                    Quantity = u.Quantity,
                    UnitPrice = u.UnitPrice,

                }).ToList()

            };
            (ViewBag.Suppliers, ViewBag.Products) = _purchaseService.GetSupplierAndProduct();

            return View(purchaseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PurchaseVM purchaseVM)
        {
            if (ModelState.IsValid)
            {

                var purchase = new Purchase()
                {
                    Id = purchaseVM.Id,
                    SupplierId = purchaseVM.SupplierId,
                    PurchaseDate = purchaseVM.PurchaseDate,
                    TotalAmount = purchaseVM.PurchaseItems.Sum(u => u.UnitPrice * u.Quantity),
                    PurchaseItems = purchaseVM.PurchaseItems.Select(u => new PurchaseItem
                    {
                        Id = u.Id,
                        ProductId = u.ProductId,
                        Quantity = u.Quantity,
                        UnitPrice = u.UnitPrice,


                    }).ToList()

                };
                _purchaseService.RemovePurchase(purchase);
                return RedirectToAction("Index");

            }
            (ViewBag.Suppliers, ViewBag.Products) = _purchaseService.GetSupplierAndProduct();
            return View(purchaseVM);
        }







        public void AssignToViewBag(string? searchTerm, int page, bool desc = false)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Desc = desc;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = _purchaseService.TotalPages(searchTerm, pageSize: pageSize);
            (ViewBag.StartPage, ViewBag.EndPage) = _purchaseService.GetStartAndEnd(searchTerm, page, pageSize);

        }
    }
}
