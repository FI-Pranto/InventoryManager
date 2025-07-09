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

        public IEnumerable<Unit> GetAllUnits(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _unitRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,u=>u.Name,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _unitRepository.GetAll(u=>u.Name.ToLower().Contains(searchTerm),includeProp: includeProp,page,pageSize:pageSize, u => u.Name, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _unitRepository.Count(searchTerm);
            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }
        public (int startPage, int endPage) GetStartAndEnd(string? searchTerm,int page=1,int pageSize=1)
        {
            int totalPages = TotalPages(searchTerm,pageSize: pageSize);


            int startPage = Math.Max(1, page - 1);
            int endPage = Math.Min(totalPages, startPage + 2);

            if (endPage - startPage < 2)
            {
                startPage = Math.Max(1, endPage - 2);
            }
            return (startPage, endPage);
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
