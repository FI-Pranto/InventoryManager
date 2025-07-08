using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.WebUI.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        public IActionResult Index()
        {
            var units=_unitService.GetAllUnits();

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
    }
}
