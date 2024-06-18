public interface IVehicleService
{
    Task<IEnumerable<Vehicle>> GetAllVehicles();
    Task<Vehicle> GetVehicleById(int id);
    Task<Vehicle> AddVehicle(Vehicle vehicle);
    Task UpdateVehicle(Vehicle vehicle);
    Task DeleteVehicle(int id);
}