using Microsoft.AspNetCore.Mvc;

namespace VehicleMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> PostVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest();
            }
            var createdVehicle = await _vehicleService.AddVehicle(vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.Id }, createdVehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }
            await _vehicleService.UpdateVehicle(vehicle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            await _vehicleService.DeleteVehicle(id);
            return NoContent();
        }
    }
}
