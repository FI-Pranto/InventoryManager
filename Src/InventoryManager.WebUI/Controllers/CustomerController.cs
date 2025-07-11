using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private const bool pagination = true;
        private const int pageSize = 1;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index(string? searchTerm,int page=1,bool desc=false)
        {
            AssignToViewBag(searchTerm, page,desc);
            var customers=_customerService.GetAllCustomers(searchTerm,page:page,pageSize:pageSize, pagination: pagination,descending:desc);

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(customer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.RemoveCustomer(customer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public void AssignToViewBag(string? searchTerm, int page, bool desc = false)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Desc = desc;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = _customerService.TotalPages(searchTerm, pageSize: pageSize);
            (ViewBag.StartPage, ViewBag.EndPage) = _customerService.GetStartAndEnd(searchTerm, page,pageSize);

        }
    }
}
