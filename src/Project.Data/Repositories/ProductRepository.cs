using Microsoft.EntityFrameworkCore;
using Project.Business.Interfaces.Repositories;
using Project.Business.Models;
using Project.Data.Contexts;
using Project.Data.Repositories;

namespace Project.Data.Repositories;

public class ProductRepository: Repository<Product>, IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context) { }

    public async Task<Product> GetProductProvider(Guid id)
    {
        return await dbContext.Produtos.AsNoTracking().Include(f => f.Provider)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsProvider()
    {
        return await dbContext.Produtos.AsNoTracking().Include(f => f.Provider)
            .OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
    {
        return await Find(p => p.ProviderId == providerId);
    }
}
