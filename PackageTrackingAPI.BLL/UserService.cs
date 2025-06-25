using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserCreateDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        if (await _userRepository.EmailExistsAsync(userCreateDto.Email))
        {
            throw new ArgumentException("A user with this email already exists.");
        }

        var user = _mapper.Map<User>(userCreateDto);
        await _userRepository.CreateUserAsync(user);
        return _mapper.Map<UserCreateDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        _mapper.Map(userDto, user);
        await _userRepository.UpdateUserAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        await _userRepository.DeleteUserAsync(id);
    }
}
