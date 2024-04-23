using Project.Business.Models;

namespace Project.Business.Interfaces.Services;

public interface IProductService: IDisposable
{
    Task Insert(Product product);
    Task Update(Product product);
    Task Remove(Guid id);
}