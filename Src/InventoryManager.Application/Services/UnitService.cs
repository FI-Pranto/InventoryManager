using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(IUnitRepository unitRepository,IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddUnit(Unit unit)
        {
            _unitRepository.Add(unit);
            _unitOfWork.Commit();
        }

        public IEnumerable<Unit> GetAllUnits(string? includeProp = null)
        {
            return _unitRepository.GetAll(includeProp: includeProp);
        }

        public Unit? GetUnitById(int? id, string? includeProp = null)
        {
            return _unitRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemoveUnit(Unit unit)
        {
            _unitRepository.Remove(unit);
            _unitOfWork.Commit();
        }

        public void UpdateUnit(Unit unit)
        {
            _unitRepository.Update(unit);
            _unitOfWork.Commit();
        }
    }
}
