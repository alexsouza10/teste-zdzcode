using Microsoft.EntityFrameworkCore;

public class MaintenanceRecordRepository : IMaintenanceRecordRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetAll()
    {
        return await _context.MaintenanceRecords.ToListAsync();
    }

    public async Task<MaintenanceRecord> GetById(int id)
    {
        return await _context.MaintenanceRecords.FindAsync(id);
    }

    public async Task<MaintenanceRecord> Add(MaintenanceRecord maintenanceRecord)
    {
        _context.MaintenanceRecords.Add(maintenanceRecord);
        await _context.SaveChangesAsync();
        return maintenanceRecord;
    }

    public async Task Update(MaintenanceRecord maintenanceRecord)
    {
        _context.Entry(maintenanceRecord).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var maintenanceRecord = await _context.MaintenanceRecords.FindAsync(id);
        if (maintenanceRecord != null)
        {
            _context.MaintenanceRecords.Remove(maintenanceRecord);
            await _context.SaveChangesAsync();
        }
    }
}
