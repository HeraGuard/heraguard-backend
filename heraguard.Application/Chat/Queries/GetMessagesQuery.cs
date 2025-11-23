using heraguard.Application.Chat.DTOs;
using MediatR;
using heraguard.Domain.Common;

namespace heraguard.Application.Chat.Queries;

public record GetMessagesQuery : IRequest<Result<List<MessageDto>>>
{
    public string User1Id { get; init; }
    public string User2Id { get; init; }
}