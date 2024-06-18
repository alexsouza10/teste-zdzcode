using Microsoft.EntityFrameworkCore;

public class ServiceTypeRepository : IServiceTypeRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceType>> GetAll()
    {
        return await _context.ServiceTypes.ToListAsync();
    }

    public async Task<ServiceType> GetById(int id)
    {
        return await _context.ServiceTypes.FindAsync(id);
    }

    public async Task<ServiceType> Add(ServiceType serviceType)
    {
        _context.ServiceTypes.Add(serviceType);
        await _context.SaveChangesAsync();
        return serviceType;
    }

    public async Task Update(ServiceType serviceType)
    {
        _context.Entry(serviceType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var serviceType = await _context.ServiceTypes.FindAsync(id);
        if (serviceType != null)
        {
            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();
        }
    }
}
