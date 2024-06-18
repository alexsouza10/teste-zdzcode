public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAll();
    Task<Vehicle> GetById(int id);
    Task<Vehicle> Add(Vehicle vehicle);
    Task Update(Vehicle vehicle);
    Task Delete(int id);
}
