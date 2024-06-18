public class MaintenanceServiceService : IMaintenanceServiceService
{
    private readonly IMaintenanceServiceRepository _maintenanceServiceRepository;

    public MaintenanceServiceService(IMaintenanceServiceRepository maintenanceServiceRepository)
    {
        _maintenanceServiceRepository = maintenanceServiceRepository;
    }

    public async Task<IEnumerable<MaintenanceService>> GetAllMaintenanceServices()
    {
        return await _maintenanceServiceRepository.GetAll();
    }

    public async Task<MaintenanceService> GetMaintenanceServiceById(int id)
    {
        return await _maintenanceServiceRepository.GetById(id);
    }

    public async Task<MaintenanceService> AddMaintenanceService(MaintenanceService maintenanceService)
    {
        return await _maintenanceServiceRepository.Add(maintenanceService);
    }

    public async Task UpdateMaintenanceService(MaintenanceService maintenanceService)
    {
        await _maintenanceServiceRepository.Update(maintenanceService);
    }

    public async Task DeleteMaintenanceService(int id)
    {
        await _maintenanceServiceRepository.Delete(id);
    }
}
