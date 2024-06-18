using Microsoft.AspNetCore.Mvc;

namespace VehicleMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRecordsController : ControllerBase
    {
        private readonly IMaintenanceRecordService _maintenanceRecordService;

        public MaintenanceRecordsController(IMaintenanceRecordService maintenanceRecordService)
        {
            _maintenanceRecordService = maintenanceRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaintenanceRecords()
        {
            var maintenanceRecords = await _maintenanceRecordService.GetAllMaintenanceRecords();
            return Ok(maintenanceRecords);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaintenanceRecord(int id)
        {
            var maintenanceRecord = await _maintenanceRecordService.GetMaintenanceRecordById(id);
            if (maintenanceRecord == null)
            {
                return NotFound();
            }
            return Ok(maintenanceRecord);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            if (maintenanceRecord == null)
            {
                return BadRequest();
            }
            var createdMaintenanceRecord = await _maintenanceRecordService.AddMaintenanceRecord(maintenanceRecord);
            return CreatedAtAction(nameof(GetMaintenanceRecord), new { id = createdMaintenanceRecord.Id }, createdMaintenanceRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaintenanceRecord(int id, MaintenanceRecord maintenanceRecord)
        {
            if (id != maintenanceRecord.Id)
            {
                return BadRequest();
            }
            await _maintenanceRecordService.UpdateMaintenanceRecord(maintenanceRecord);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceRecord(int id)
        {
            await _maintenanceRecordService.DeleteMaintenanceRecord(id);
            return NoContent();
        }
    }
}