using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IServices
{
    public interface IUnitService
    {
        void AddUnit(Unit unit);
        void UpdateUnit(Unit unit);

        void RemoveUnit(Unit unit);

        Unit? GetUnitById(int? id,string? includeProp=null);

        IEnumerable<Unit> GetAllUnits(string? searchTerm,string? includeProp=null,int page=1, int pageSize = 1);

        int TotalPages(string? searchTerm, int pageSize);

        (int startPage, int endPage) GetStartAndEnd(string? searchTerm, int page = 1,int pageSize=1);
    }
}
