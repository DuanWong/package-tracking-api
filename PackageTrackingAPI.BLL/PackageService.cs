using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.BLL
{
    public class PackageService
    {
        private readonly PackageRepository _packageRepository;  // Data access layer
        private readonly IMapper _mapper;  // Object mapper

        // Constructor with dependency injection
        public PackageService(PackageRepository packageRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
        }

        // Get package by ID (returns null if not found)
        public async Task<PackageDto> GetPackageByIdAsync(int id)
        {
            var package = await _packageRepository.GetByIdAsync(id);
            return _mapper.Map<PackageDto>(package);
        }

        // Get package by tracking number (returns null if not found)
        public async Task<PackageDto> GetPackageByTrackingNumberAsync(string trackingNumber)
        {
            var package = await _packageRepository.GetByTrackingNumberAsync(trackingNumber);
            return _mapper.Map<PackageDto>(package);
        }

        // Create new package and return created DTO
        public async Task<PackageDto> CreatePackageAsync(PackageDto packageDto)
        {
            var package = _mapper.Map<Package>(packageDto);
            await _packageRepository.AddAsync(package);
            return _mapper.Map<PackageDto>(package);
        }

        // Update existing package (no-op if not found)
        public async Task UpdatePackageAsync(int id, PackageDto packageDto)
        {
            var package = await _packageRepository.GetByIdAsync(id);
            if (package != null)
            {
                _mapper.Map(packageDto, package);  // Apply updates
                await _packageRepository.UpdateAsync(package);
            }
        }

        // Delete package by ID (no-op if not found)
        public async Task DeletePackageAsync(int id)
        {
            await _packageRepository.DeleteAsync(id);
        }
    }
}