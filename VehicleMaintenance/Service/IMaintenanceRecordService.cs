public interface IMaintenanceRecordService
{
    Task<IEnumerable<MaintenanceRecord>> GetAllMaintenanceRecords();
    Task<MaintenanceRecord> GetMaintenanceRecordById(int id);
    Task<MaintenanceRecord> AddMaintenanceRecord(MaintenanceRecord maintenanceRecord);
    Task UpdateMaintenanceRecord(MaintenanceRecord maintenanceRecord);
    Task DeleteMaintenanceRecord(int id);
}
