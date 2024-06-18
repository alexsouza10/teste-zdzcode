public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        return await _vehicleRepository.GetAll();
    }

    public async Task<Vehicle> GetVehicleById(int id)
    {
        return await _vehicleRepository.GetById(id);
    }

    public async Task<Vehicle> AddVehicle(Vehicle vehicle)
    {
        return await _vehicleRepository.Add(vehicle);
    }

    public async Task UpdateVehicle(Vehicle vehicle)
    {
        await _vehicleRepository.Update(vehicle);
    }

    public async Task DeleteVehicle(int id)
    {
        await _vehicleRepository.Delete(id);
    }
}