using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class AlertRepository
    {
        private readonly PackageTrackingContext _context;

        public AlertRepository(PackageTrackingContext context)
        {
            _context = context;
        }

        public async Task<List<Alert>> GetAllAlertsAsync()
        {
            return await _context.Alerts.ToListAsync();
        }

        public async Task<Alert> GetAlertByIdAsync(int id)
        {
            return await _context.Alerts.FindAsync(id);
        }

        public async Task CreateAlertAsync(Alert alert)
        {
            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlertAsync(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            if (alert != null)
            {
                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }
    }
}
