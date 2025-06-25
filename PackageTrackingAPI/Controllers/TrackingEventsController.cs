using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageTrackingAPI.DAL;

namespace PackageTrackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // Base route: /api/trackingevents
    public class TrackingEventsController : ControllerBase
    {
        private readonly PackageTrackingContext _context;  // DB access
        private readonly IMapper _mapper;  // Object mapping

        public TrackingEventsController(PackageTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]  // Create new tracking event
        public async Task<ActionResult<TrackingEventDto>> CreateTrackingEvent([FromBody] TrackingEventDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();  // Validate input

            if (!await _context.Packages.AnyAsync(p => p.PackageID == dto.PackageID))
                return NotFound();  // Verify package exists

            var trackingEvent = _mapper.Map<TrackingEvent>(dto);
            trackingEvent.Timestamp = DateTime.UtcNow;  // Set current time

            _context.TrackingEvents.Add(trackingEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(  // 201 response with location header
                nameof(GetTrackingEvent),
                new { id = trackingEvent.EventID },
                _mapper.Map<TrackingEventDto>(trackingEvent));
        }

        [HttpGet]  // Get filtered events
        public async Task<ActionResult<IEnumerable<TrackingEventDto>>> GetTrackingEvents(
            [FromQuery] int? packageId = null,  // Optional package filter
            [FromQuery] string status = null)   // Optional status filter
        {
            var query = _context.TrackingEvents.AsQueryable();

            if (packageId.HasValue) query = query.Where(e => e.PackageID == packageId);
            if (!string.IsNullOrEmpty(status)) query = query.Where(e => e.Status == status);

            return _mapper.Map<List<TrackingEventDto>>(await query.ToListAsync());
        }

        [HttpGet("{id}")]  // Get single event by ID
        public async Task<ActionResult<TrackingEventDto>> GetTrackingEvent(int id)
        {
            var trackingEvent = await _context.TrackingEvents.FindAsync(id);
            return trackingEvent == null ? NotFound() : _mapper.Map<TrackingEventDto>(trackingEvent);
        }

        [HttpGet("package/{packageId}/latest")]  // Get most recent event for package
        public async Task<ActionResult<TrackingEventDto>> GetLatestEvent(int packageId)
        {
            var latestEvent = await _context.TrackingEvents
                .Where(e => e.PackageID == packageId)
                .OrderByDescending(e => e.Timestamp)
                .FirstOrDefaultAsync();

            return latestEvent == null ? NotFound() : _mapper.Map<TrackingEventDto>(latestEvent);
        }

        [HttpPut("{id}")]  // Full update
        public async Task<IActionResult> UpdateTrackingEvent(int id, [FromBody] TrackingEventDto dto)
        {
            if (id != dto.EventID) return BadRequest();  // ID mismatch check

            var trackingEvent = await _context.TrackingEvents.FindAsync(id);
            if (trackingEvent == null) return NotFound();

            _mapper.Map(dto, trackingEvent);  // Apply updates
            await _context.SaveChangesAsync();

            return NoContent();  // 204 success
        }

        [HttpDelete("{id}")]  // Delete event
        public async Task<IActionResult> DeleteTrackingEvent(int id)
        {
            var trackingEvent = await _context.TrackingEvents.FindAsync(id);
            if (trackingEvent == null) return NotFound();

            _context.TrackingEvents.Remove(trackingEvent);
            await _context.SaveChangesAsync();

            return NoContent();  // 204 success
        }
    }
}