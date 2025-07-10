using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Interfaces.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        int Count(string? searchTerm=null);

    }
}
