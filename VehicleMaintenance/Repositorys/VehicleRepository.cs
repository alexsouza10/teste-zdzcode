using Microsoft.EntityFrameworkCore;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetAll()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<Vehicle> GetById(int id)
    {
        return await _context.Vehicles.FindAsync(id);
    }

    public async Task<Vehicle> Add(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }

    public async Task Update(Vehicle vehicle)
    {
        _context.Entry(vehicle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
