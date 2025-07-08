using InventoryManager.Application.Interfaces.IServices;
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
    }
}
