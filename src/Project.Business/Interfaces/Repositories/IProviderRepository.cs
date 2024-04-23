using Project.Business.Models;

namespace Project.Business.Interfaces.Repositories;

public interface IProviderRepository: IRepository<Provider>
{
    Task<Provider> GetProviderAddress(Guid id);
    Task<Provider> GetProviderProductAddress(Guid id);
}
