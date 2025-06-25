using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class TrackingEventRepository
    {
        private readonly PackageTrackingContext _context;

        // Constructor injection of DB context
        public TrackingEventRepository(PackageTrackingContext context) => _context = context;

        // Get single event by ID with package details
        public async Task<TrackingEvent> GetByIdAsync(int id)
            => await _context.TrackingEvents
                .Include(te => te.Package)
                .FirstOrDefaultAsync(te => te.EventID == id);

        // Get all events for a specific package
        public async Task<List<TrackingEvent>> GetByPackageIdAsync(int packageId)
            => await _context.TrackingEvents
                .Where(te => te.PackageID == packageId)
                .ToListAsync();

        // Get most recent event for a package (null if none exist)
        public async Task<TrackingEvent> GetLatestEventAsync(int packageId)
            => await _context.TrackingEvents
                .Where(te => te.PackageID == packageId)
                .OrderByDescending(te => te.Timestamp)
                .FirstOrDefaultAsync();

        // Add new tracking event
        public async Task AddAsync(TrackingEvent trackingEvent)
        {
            await _context.TrackingEvents.AddAsync(trackingEvent);
            await _context.SaveChangesAsync();
        }

        // Update existing event
        public async Task UpdateAsync(TrackingEvent trackingEvent)
        {
            _context.Entry(trackingEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete event by ID (no-op if not found)
        public async Task DeleteAsync(int id)
        {
            var trackingEvent = await GetByIdAsync(id);
            if (trackingEvent != null)
            {
                _context.TrackingEvents.Remove(trackingEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}