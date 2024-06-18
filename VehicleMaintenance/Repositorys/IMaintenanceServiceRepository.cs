public interface IMaintenanceServiceRepository
{
    Task<IEnumerable<MaintenanceService>> GetAll();
    Task<MaintenanceService> GetById(int id);
    Task<MaintenanceService> Add(MaintenanceService maintenanceService);
    Task Update(MaintenanceService maintenanceService);
    Task Delete(int id);
}
