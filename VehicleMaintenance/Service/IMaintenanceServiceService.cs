public interface IMaintenanceServiceService
{
    Task<IEnumerable<MaintenanceService>> GetAllMaintenanceServices();
    Task<MaintenanceService> GetMaintenanceServiceById(int id);
    Task<MaintenanceService> AddMaintenanceService(MaintenanceService maintenanceService);
    Task UpdateMaintenanceService(MaintenanceService maintenanceService);
    Task DeleteMaintenanceService(int id);
}
