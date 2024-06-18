public interface IMaintenanceRecordRepository
{
    Task<IEnumerable<MaintenanceRecord>> GetAll();
    Task<MaintenanceRecord> GetById(int id);
    Task<MaintenanceRecord> Add(MaintenanceRecord maintenanceRecord);
    Task Update(MaintenanceRecord maintenanceRecord);
    Task Delete(int id);
}
