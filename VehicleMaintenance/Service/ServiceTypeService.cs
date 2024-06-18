public class ServiceTypeService : IServiceTypeService
{
    private readonly IServiceTypeRepository _serviceTypeRepository;

    public ServiceTypeService(IServiceTypeRepository serviceTypeRepository)
    {
        _serviceTypeRepository = serviceTypeRepository;
    }

    public async Task<IEnumerable<ServiceType>> GetAllServiceTypes()
    {
        return await _serviceTypeRepository.GetAll();
    }

    public async Task<ServiceType> GetServiceTypeById(int id)
    {
        return await _serviceTypeRepository.GetById(id);
    }

    public async Task<ServiceType> AddServiceType(ServiceType serviceType)
    {
        return await _serviceTypeRepository.Add(serviceType);
    }

    public async Task UpdateServiceType(ServiceType serviceType)
    {
        await _serviceTypeRepository.Update(serviceType);
    }

    public async Task DeleteServiceType(int id)
    {
        await _serviceTypeRepository.Delete(id);
    }
}
