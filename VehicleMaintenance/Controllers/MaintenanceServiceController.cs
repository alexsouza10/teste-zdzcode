using Microsoft.AspNetCore.Mvc;

namespace VehicleMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceServicesController : ControllerBase
    {
        private readonly IMaintenanceServiceService _maintenanceServiceService;

        public MaintenanceServicesController(IMaintenanceServiceService maintenanceServiceService)
        {
            _maintenanceServiceService = maintenanceServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaintenanceServices()
        {
            var maintenanceServices = await _maintenanceServiceService.GetAllMaintenanceServices();
            return Ok(maintenanceServices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaintenanceService(int id)
        {
            var maintenanceService = await _maintenanceServiceService.GetMaintenanceServiceById(id);
            if (maintenanceService == null)
            {
                return NotFound();
            }
            return Ok(maintenanceService);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaintenanceService(MaintenanceService maintenanceService)
        {
            if (maintenanceService == null)
            {
                return BadRequest();
            }
            var createdMaintenanceService = await _maintenanceServiceService.AddMaintenanceService(maintenanceService);
            return CreatedAtAction(nameof(GetMaintenanceService), new { id = createdMaintenanceService.Id }, createdMaintenanceService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaintenanceService(int id, MaintenanceService maintenanceService)
        {
            if (id != maintenanceService.Id)
            {
                return BadRequest();
            }
            await _maintenanceServiceService.UpdateMaintenanceService(maintenanceService);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceService(int id)
        {
            await _maintenanceServiceService.DeleteMaintenanceService(id);
            return NoContent();
        }
    }
}