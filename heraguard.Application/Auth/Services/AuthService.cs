using AutoMapper;
using heraguard.Application.Auth.Dtos;
using heraguard.Application.Auth.Interfaces;
using heraguard.Application.Users.DTOs;
using heraguard.Domain.Entities;

namespace heraguard.Application.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly Supabase.Client _supabase;

    public AuthService(IAuthRepository authRepository, IMapper mapper, Supabase.Client supabase)
    {
        _authRepository = authRepository;
        _mapper = mapper;
        _supabase = supabase;
    }

    public async Task<AuthResponseDto?> LoginAsync(string email, string password)
    {
        try
        {
            var session = await _supabase.Auth.SignIn(email, password);
            
            if (session?.User == null)
                return null;

            var user = await _authRepository.GetUserByEmailAsync(email);

            if (user == null)
                return null;

            return new AuthResponseDto
            {
                AccessToken = session.AccessToken ?? "",
                RefreshToken = session.RefreshToken ?? "",
                User = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception)
        {
            return null;
        }
    }
    

    public async Task<AuthResponseDto> RegisterAsync(string email, string password, string name, string lastName, int roleId)
    {
        // Registrar en Supabase
        var authResponse = await _supabase.Auth.SignUp(email, password);
        
        if (authResponse?.User == null)
            throw new InvalidOperationException("Error al registrar usuario en Supabase");
        
        var user = new User
        {
            Id = Guid.Parse(authResponse.User.Id),
            Email = email,
            Name = name,
            LastName = lastName,
            RoleId = roleId
        };

        var createdUser = await _authRepository.CreateUserAsync(user);
        var userWithRole = await _authRepository.GetUserByEmailAsync(email);

        return new AuthResponseDto
        {
            AccessToken = authResponse.AccessToken ?? "",
            RefreshToken = authResponse.RefreshToken ?? "",
            User = _mapper.Map<UserDto>(userWithRole)
        };
    }

    
}
