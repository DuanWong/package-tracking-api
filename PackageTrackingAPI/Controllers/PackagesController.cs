using Microsoft.AspNetCore.Mvc;
using PackageTrackingAPI.BLL;
using PackageTrackingAPI.DTOs;

namespace PackageTrackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // Base route: /api/packages
    public class PackagesController : ControllerBase
    {
        private readonly PackageService _packageService;  // Core business logic service

        public PackagesController(PackageService packageService)  // Dependency injection
        {
            _packageService = packageService;
        }

        [HttpGet("{id}")]  // GET /api/packages/1
        public async Task<ActionResult<PackageDto>> GetPackageById(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package == null) return NotFound();  // 404 if not found
            return Ok(package);  // 200 with package data
        }

        [HttpGet("tracking/{trackingNumber}")]  // GET /api/packages/tracking/ABC123
        public async Task<ActionResult<PackageDto>> GetPackageByTrackingNumber(string trackingNumber)
        {
            var package = await _packageService.GetPackageByTrackingNumberAsync(trackingNumber);
            if (package == null) return NotFound();
            return Ok(package);
        }

        [HttpPost]  // POST /api/packages
        public async Task<ActionResult<PackageDto>> CreatePackage([FromBody] PackageDto packageDto)
        {
            var createdPackage = await _packageService.CreatePackageAsync(packageDto);
            return CreatedAtAction(nameof(GetPackageById),  // 201 Created
                new { id = createdPackage.PackageID },
                createdPackage);
        }

        [HttpPut("{id}")]  // PUT /api/packages/1
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageDto packageDto)
        {
            await _packageService.UpdatePackageAsync(id, packageDto);
            return NoContent();  // 204 No Content
        }

        [HttpDelete("{id}")]  // DELETE /api/packages/1
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _packageService.DeletePackageAsync(id);
            return NoContent();  // 204 No Content
        }
    }
}