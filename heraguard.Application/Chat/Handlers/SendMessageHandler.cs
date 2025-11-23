using heraguard.Application.Chat.Commands;
using heraguard.Application.Chat.Interfaces;
using heraguard.Application.Chat.DTOs;
using MediatR;
using heraguard.Domain.Common;

namespace heraguard.Application.Chat.Handlers;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, Result>
{
    private readonly IChatRepository _repository;

    public SendMessageHandler(IChatRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var conversationId = _repository.GenerateConversationId(request.SenderId, request.ReceiverId);

        var messageDto = new MessageDto
        {
            ConversationId = conversationId,
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
            Message = request.Content,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Type = "text"
        };

        await _repository.SendMessageAsync(messageDto);
        return Result.Success();
    }
}