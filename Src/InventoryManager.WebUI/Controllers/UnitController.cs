using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;
        private const int pageSize = 1;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        public IActionResult Index(string? searchTerm,int page=1)
        {
            AssignToViewBag(searchTerm, page);
            var units=_unitService.GetAllUnits(searchTerm,page:page,pageSize:pageSize);

            return View(units);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitService.AddUnit(unit);

                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var unit = _unitService.GetUnitById(id);

            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitService.UpdateUnit(unit);

                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var unit = _unitService.GetUnitById(id);

            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitService.RemoveUnit(unit);

                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }
        public void AssignToViewBag(string? searchTerm, int page)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = _unitService.TotalPages(searchTerm, pageSize: pageSize);
            (ViewBag.StartPage, ViewBag.EndPage) = _unitService.GetStartAndEnd(searchTerm, page,pageSize);

        }
    }
}
