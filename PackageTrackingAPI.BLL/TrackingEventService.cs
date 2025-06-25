using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.BLL
{
    public class TrackingEventService
    {
        private readonly TrackingEventRepository _repository;  // Data access
        private readonly IMapper _mapper;  // Object mapping

        // Constructor with DI
        public TrackingEventService(TrackingEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get single event by ID → returns null if not found
        public async Task<TrackingEventDto> GetTrackingEventByIdAsync(int id)
            => _mapper.Map<TrackingEventDto>(await _repository.GetByIdAsync(id));

        // Get all events for a package → empty list if none
        public async Task<List<TrackingEventDto>> GetTrackingEventsByPackageIdAsync(int packageId)
            => _mapper.Map<List<TrackingEventDto>>(await _repository.GetByPackageIdAsync(packageId));

        // Get most recent event for package → returns null if none
        public async Task<TrackingEventDto> GetLatestTrackingEventAsync(int packageId)
            => _mapper.Map<TrackingEventDto>(await _repository.GetLatestEventAsync(packageId));

        // Create new event → returns created DTO
        public async Task<TrackingEventDto> CreateTrackingEventAsync(TrackingEventDto dto)
        {
            var trackingEvent = _mapper.Map<TrackingEvent>(dto);
            await _repository.AddAsync(trackingEvent);
            return _mapper.Map<TrackingEventDto>(trackingEvent);
        }

        // Update existing event → no-op if not found
        public async Task UpdateTrackingEventAsync(int id, TrackingEventDto dto)
        {
            var trackingEvent = await _repository.GetByIdAsync(id);
            if (trackingEvent != null)
            {
                _mapper.Map(dto, trackingEvent);
                await _repository.UpdateAsync(trackingEvent);
            }
        }

        // Delete event 
        public async Task DeleteTrackingEventAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}