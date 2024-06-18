public interface IServiceTypeRepository
{
    Task<IEnumerable<ServiceType>> GetAll();
    Task<ServiceType> GetById(int id);
    Task<ServiceType> Add(ServiceType serviceType);
    Task Update(ServiceType serviceType);
    Task Delete(int id);
}
