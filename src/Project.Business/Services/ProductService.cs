using System.Net.Http.Headers;
using Project.Business.Interfaces;
using Project.Business.Interfaces.Repositories;
using Project.Business.Interfaces.Services;
using Project.Business.Models;
using Project.Business.Models.Validations;

namespace Project.Business.Services;

public class ProductService: BaseService, IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository,
                            INotifier notifier) : base(notifier)
    {
        _productRepository = productRepository;
    }

    public async Task Insert(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Insert(product);
    }

    public async Task Update(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Update(product);
    }

    public async Task Remove(Guid id)
    {
        await _productRepository.Delete(id);
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
    }
}