using Project.Business.Interfaces;
using Project.Business.Interfaces.Repositories;
using Project.Business.Interfaces.Services;
using Project.Business.Models;
using Project.Business.Models.Validations;

namespace Project.Business.Services;

public class ProviderService: BaseService, IProviderService
{
    private readonly IProviderRepository _providerRepository;
    private readonly IAddressRepository _addressRepository;

    public ProviderService(IProviderRepository providerRepository, 
                            IAddressRepository addressRepository,
                            INotifier notifier) : base(notifier)
    {
        _providerRepository = providerRepository;
        _addressRepository = addressRepository;
    }

    public async Task Insert(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider) 
            || !ExecuteValidation(new AddressValidation(), provider.Address)) return;

        if (_providerRepository.Find(f => f.Document == provider.Document).Result.Any())
        {
            Notify("Já existe um fornecedor com este documento infomado.");
            return;
        }

        await _providerRepository.Insert(provider);
    }

    public async Task Update(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider)) return;

        if (_providerRepository.Find(f => f.Document == provider.Document && f.Id != provider.Id).Result.Any())
        {
            Notify("Já existe um fornecedor com este documento infomado.");
            return;
        }

        await _providerRepository.Update(provider);
    }

    public async Task UpdateAddress(Address address)
    {
        if (!ExecuteValidation(new AddressValidation(), address)) return;

        await _addressRepository.Update(address);
    }

    public async Task Remove(Guid id)
    {
        if (_providerRepository.GetProviderProductAddress(id).Result.Products.Any())
        {
            Notify("O fornecedor possui produtos cadastrados!");
            return;
        }

        var address = await _addressRepository.GetAddressByProvider(id);

        if (address != null)
        {
            await _addressRepository.Delete(address.Id);
        }

        await _providerRepository.Delete(id);
    }

    public void Dispose()
    {
        _providerRepository?.Dispose();
        _addressRepository?.Dispose();
    }
}