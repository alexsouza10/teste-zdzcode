public interface IServiceTypeService
{
    Task<IEnumerable<ServiceType>> GetAllServiceTypes();
    Task<ServiceType> GetServiceTypeById(int id);
    Task<ServiceType> AddServiceType(ServiceType serviceType);
    Task UpdateServiceType(ServiceType serviceType);
    Task DeleteServiceType(int id);
}
