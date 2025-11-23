using heraguard.Application.Chat.Interfaces;
using heraguard.Application.Chat.Queries;
using heraguard.Application.Chat.DTOs;
using MediatR;
using heraguard.Domain.Common;

namespace heraguard.Application.Chat.Handlers;

public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, Result<List<MessageDto>>>
{
    private readonly IChatRepository _repository;

    public GetMessagesHandler(IChatRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _repository.GetConversationMessagesAsync(request.User1Id, request.User2Id);
        return Result<List<MessageDto>>.Success(messages);
    }
}