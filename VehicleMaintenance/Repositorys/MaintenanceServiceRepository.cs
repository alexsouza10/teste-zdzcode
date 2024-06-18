using Microsoft.EntityFrameworkCore;

public class MaintenanceServiceRepository : IMaintenanceServiceRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceService>> GetAll()
    {
        return await _context.MaintenanceServices.ToListAsync();
    }

    public async Task<MaintenanceService> GetById(int id)
    {
        return await _context.MaintenanceServices.FindAsync(id);
    }

    public async Task<MaintenanceService> Add(MaintenanceService maintenanceService)
    {
        maintenanceService.Id = 0;

        _context.MaintenanceServices.Add(maintenanceService);
        await _context.SaveChangesAsync();
        return maintenanceService;
    }

    public async Task Update(MaintenanceService maintenanceService)
    {
        _context.Entry(maintenanceService).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var maintenanceService = await _context.MaintenanceServices.FindAsync(id);
        if (maintenanceService != null)
        {
            _context.MaintenanceServices.Remove(maintenanceService);
            await _context.SaveChangesAsync();
        }
    }
}
