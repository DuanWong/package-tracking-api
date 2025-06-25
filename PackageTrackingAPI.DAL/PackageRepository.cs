using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class PackageRepository
    {
        private readonly PackageTrackingContext _context;  // DB context

        public PackageRepository(PackageTrackingContext context) => _context = context;

        // Get package with related events and sender
        public async Task<Package> GetByIdAsync(int id)
            => await _context.Packages
                .Include(p => p.TrackingEvents)
                .Include(p => p.Sender)
                .FirstOrDefaultAsync(p => p.PackageID == id);

        // Get package by tracking number with relations
        public async Task<Package> GetByTrackingNumberAsync(string trackingNumber)
            => await _context.Packages
                .Include(p => p.TrackingEvents)
                .Include(p => p.Sender)
                .FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);

        // Add new package
        public async Task AddAsync(Package package)
        {
            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        // Update existing package
        public async Task UpdateAsync(Package package)
        {
            _context.Entry(package).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete package by ID (if exists)
        public async Task DeleteAsync(int id)
        {
            var package = await GetByIdAsync(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }

        // Check if package exists
        public async Task<bool> ExistsAsync(int id)
            => await _context.Packages.AnyAsync(p => p.PackageID == id);
    }
}