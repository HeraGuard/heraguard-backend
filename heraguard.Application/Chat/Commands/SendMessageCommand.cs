using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Chat.Commands;

public record SendMessageCommand : IRequest<Result>
{
    public string Content { get; init; }
    public string SenderId { get; init; }
    public string ReceiverId { get; init; }
}