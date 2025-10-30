using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Auth.Commands;

public record LogoutCommand(string? UserId) : IRequest<Result>;