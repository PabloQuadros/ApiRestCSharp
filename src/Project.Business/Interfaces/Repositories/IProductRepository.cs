using Project.Business.Models;

namespace Project.Business.Interfaces.Repositories;

public interface IProductRepository: IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByProvider(Guid fornecedorId);
    Task<IEnumerable<Product>> GetProductsProvider();
    Task<Product> GetProductProvider(Guid id);
}
