using Project.Business.Models;

namespace Project.Business.Interfaces.Repositories;

public interface IAddressRepository : IRepository<Address>
{
    Task<Address> GetAddressByProvider(Guid fornecedorId);
}
