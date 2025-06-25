using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.BLL
{
    public class AlertService
    {
        private readonly AlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public AlertService(AlertRepository alertRepository, IMapper mapper)
        {
            _alertRepository = alertRepository;
            _mapper = mapper;
        }

        public async Task<List<AlertDto>> GetAllAlertsAsync()
        {
            var alerts = await _alertRepository.GetAllAlertsAsync();
            return _mapper.Map<List<AlertDto>>(alerts);
        }

        public async Task<AlertDto> GetAlertByIdAsync(int id)
        {
            var alert = await _alertRepository.GetAlertByIdAsync(id);
            if (alert == null)
            {
                throw new KeyNotFoundException($"Alert with ID {id} not found.");
            }

            return _mapper.Map<AlertDto>(alert);
        }

        public async Task<AlertDto> CreateAlertAsync(AlertDto alertDto)
        {
            if (alertDto.UserID <= 0 || alertDto.PackageID <= 0)
            {
                throw new ArgumentException("UserID and PackageID must be valid.");
            }

            var alert = _mapper.Map<Alert>(alertDto);
            await _alertRepository.CreateAlertAsync(alert);
            return _mapper.Map<AlertDto>(alert);
        }

        public async Task DeleteAlertAsync(int id)
        {
            var existingAlert = await _alertRepository.GetAlertByIdAsync(id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"Alert with ID {id} not found.");
            }

            await _alertRepository.DeleteAlertAsync(id);
        }
    }
}
