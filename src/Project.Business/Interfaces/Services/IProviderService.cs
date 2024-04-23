using Project.Business.Models;

namespace Project.Business.Interfaces.Services;

public interface IProviderService : IDisposable
{
    Task Insert(Provider provider);
    Task Update(Provider provider);
    Task Remove(Guid id);

    Task UpdateAddress(Address address);
}