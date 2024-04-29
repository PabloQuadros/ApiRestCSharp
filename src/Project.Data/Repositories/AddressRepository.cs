using Microsoft.EntityFrameworkCore;
using Project.Business.Interfaces.Repositories;
using Project.Business.Models;
using Project.Data.Contexts;

namespace Project.Data.Repositories;

public class AddressRepository: Repository<Address>, IAddressRepository
{
    public AddressRepository(MyDbContext context) : base(context) { }

    public async Task<Address> GetAddressByProvider(Guid providerId)
    {
        return await dbContext.Enderecos.AsNoTracking()
            .FirstOrDefaultAsync(f => f.ProviderId == providerId);
    }
}
