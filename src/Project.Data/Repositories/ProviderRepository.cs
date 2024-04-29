using Microsoft.EntityFrameworkCore;
using Project.Business.Interfaces.Repositories;
using Project.Business.Models;
using Project.Data.Contexts;
using Project.Data.Repositories;

namespace Project.Data.Repositories;

public class ProviderRepository: Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await dbContext.Fornecedores.AsNoTracking()
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Provider> GetProviderProductAddress(Guid id)
        {
            return await dbContext.Fornecedores.AsNoTracking()
                .Include(c => c.Products)
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }