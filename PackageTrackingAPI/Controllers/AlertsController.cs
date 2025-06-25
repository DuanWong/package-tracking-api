using Microsoft.AspNetCore.Mvc;
using PackageTrackingAPI.BLL;
using PackageTrackingAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PackageTrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly AlertService _alertService;

        public AlertController(AlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlerts()
        {
            try
            {
                var alerts = await _alertService.GetAllAlertsAsync();
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlert(int id)
        {
            try
            {
                var alert = await _alertService.GetAlertByIdAsync(id);
                return Ok(alert);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { status = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = 400, message = "Invalid input data.", errors = ModelState.Values });
            }

            try
            {
                var alert = await _alertService.CreateAlertAsync(alertDto);
                return CreatedAtAction(nameof(GetAlert), new { id = alert.AlertID }, alert);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { status = 400, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            try
            {
                await _alertService.DeleteAlertAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { status = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
