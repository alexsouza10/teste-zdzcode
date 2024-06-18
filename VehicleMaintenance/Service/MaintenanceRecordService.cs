public class MaintenanceRecordService : IMaintenanceRecordService
{
    private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;

    public MaintenanceRecordService(IMaintenanceRecordRepository maintenanceRecordRepository)
    {
        _maintenanceRecordRepository = maintenanceRecordRepository;
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetAllMaintenanceRecords()
    {
        return await _maintenanceRecordRepository.GetAll();
    }

    public async Task<MaintenanceRecord> GetMaintenanceRecordById(int id)
    {
        return await _maintenanceRecordRepository.GetById(id);
    }

    public async Task<MaintenanceRecord> AddMaintenanceRecord(MaintenanceRecord maintenanceRecord)
    {
        return await _maintenanceRecordRepository.Add(maintenanceRecord);
    }

    public async Task UpdateMaintenanceRecord(MaintenanceRecord maintenanceRecord)
    {
        await _maintenanceRecordRepository.Update(maintenanceRecord);
    }

    public async Task DeleteMaintenanceRecord(int id)
    {
        await _maintenanceRecordRepository.Delete(id);
    }
}
