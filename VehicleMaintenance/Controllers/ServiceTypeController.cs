using Microsoft.AspNetCore.Mvc;

namespace VehicleMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypesController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceTypes()
        {
            var serviceTypes = await _serviceTypeService.GetAllServiceTypes();
            return Ok(serviceTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceType(int id)
        {
            var serviceType = await _serviceTypeService.GetServiceTypeById(id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return Ok(serviceType);
        }

        [HttpPost]
        public async Task<IActionResult> PostServiceType(ServiceType serviceType)
        {
            if (serviceType == null)
            {
                return BadRequest();
            }
            var createdServiceType = await _serviceTypeService.AddServiceType(serviceType);
            return CreatedAtAction(nameof(GetServiceType), new { id = createdServiceType.Id }, createdServiceType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(int id, ServiceType serviceType)
        {
            if (id != serviceType.Id)
            {
                return BadRequest();
            }
            await _serviceTypeService.UpdateServiceType(serviceType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceType(int id)
        {
            await _serviceTypeService.DeleteServiceType(id);
            return NoContent();
        }
    }
}