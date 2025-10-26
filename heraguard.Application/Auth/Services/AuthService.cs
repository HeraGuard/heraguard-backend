using AutoMapper;
using heraguard.Application.Auth.Dtos;
using heraguard.Application.Auth.Interfaces;
using heraguard.Application.Users.DTOs;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using heraguard.Domain.Entities;
using Supabase;
using Supabase.Gotrue.Exceptions;

namespace heraguard.Application.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly Client _supabase;

    public AuthService(IAuthRepository authRepository, IMapper mapper, Client supabase)
    {
        _authRepository = authRepository;
        _mapper = mapper;
        _supabase = supabase;
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(string email, string password)
    {
        try
        {
            var session = await _supabase.Auth.SignIn(email, password);

            if (session?.User == null)
                return Result<AuthResponseDto>.Failure(AuthErrors.InvalidCredentials);

            var user = await _authRepository.GetUserByEmailAsync(email);

            if (user == null)
                return Result<AuthResponseDto>.Failure(AuthErrors.InvalidCredentials);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                AccessToken = session.AccessToken ?? "",
                RefreshToken = session.RefreshToken ?? "",
                User = _mapper.Map<UserDto>(user)
            });
        }
        catch (GotrueException ex)
        {
            return Result<AuthResponseDto>.Failure(MapSupabaseErrorToAuthError(ex));
        }
        catch (Exception)
        {
            return Result<AuthResponseDto>.Failure(AuthErrors.AuthenticationFailed);
        }
    }

    public async Task<Result<AuthResponseDto>> RegisterAsync(string email, string password, string name,
        string lastName, int roleId)
    {
        try
        {
            var authResponse = await _supabase.Auth.SignUp(email, password);

            if (authResponse?.User == null)
                return Result<AuthResponseDto>.Failure(AuthErrors.AuthenticationFailed);

            var user = new User
            {
                Id = Guid.Parse(authResponse.User.Id),
                Email = email,
                Name = name,
                LastName = lastName,
                RoleId = roleId
            };

            await _authRepository.CreateUserAsync(user);
            var userWithRole = await _authRepository.GetUserByEmailAsync(email);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                AccessToken = authResponse.AccessToken ?? "",
                RefreshToken = authResponse.RefreshToken ?? "",
                User = _mapper.Map<UserDto>(userWithRole)
            });
        }
        catch (GotrueException ex)
        {
            return Result<AuthResponseDto>.Failure(MapSupabaseErrorToAuthError(ex));
        }
        catch (Exception)
        {
            return Result<AuthResponseDto>.Failure(AuthErrors.AuthenticationFailed);
        }
    }

    private static Error MapSupabaseErrorToAuthError(GotrueException ex)
    {
        var message = ex.Message.ToLowerInvariant();

        // Errores de credenciales
        if (message.Contains("invalid_credentials") ||
            message.Contains("invalid login credentials") ||
            message.Contains("email not confirmed"))
            return AuthErrors.InvalidCredentials;

        // Errores de email
        if (message.Contains("user already registered") ||
            message.Contains("email_exists"))
            return AuthErrors.EmailExists;

        if (message.Contains("invalid") && message.Contains("email"))
            return AuthErrors.InvalidEmail;

        // Errores de sesión
        if (message.Contains("session") && message.Contains("expired"))
            return AuthErrors.SessionExpired;

        if (message.Contains("bad_jwt") ||
            message.Contains("invalid token") ||
            message.Contains("jwt"))
            return AuthErrors.InvalidToken;

        // Errores de rate limit
        if (message.Contains("over_request_rate_limit") ||
            message.Contains("rate limit") ||
            message.Contains("too many requests"))
            return AuthErrors.RateLimitExceeded;

        // Errores de validación
        if (message.Contains("password") &&
            (message.Contains("weak") || message.Contains("short") || message.Contains("length")))
            return AuthErrors.WeakPassword;

        return AuthErrors.AuthenticationFailed;
    }
}