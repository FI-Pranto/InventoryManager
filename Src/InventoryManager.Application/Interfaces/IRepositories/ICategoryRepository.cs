using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Interfaces.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        int Count(string? searchTerm=null);

    }
}
