using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.Business.Interfaces.Repositories;
using Project.Business.Models;
using Project.Data.Contexts;

namespace Project.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly MyDbContext dbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(MyDbContext db)
    {
        dbContext = db;
        DbSet = db.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task Insert(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Update(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Delete(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        dbContext?.Dispose();
    }
}