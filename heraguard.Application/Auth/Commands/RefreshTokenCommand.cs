using heraguard.Application.Auth.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Auth.Commands;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthResponseDto>>;
