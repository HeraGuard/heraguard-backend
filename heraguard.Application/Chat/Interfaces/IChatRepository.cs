using heraguard.Application.Chat.DTOs;

namespace heraguard.Application.Chat.Interfaces;

public interface IChatRepository
{
    Task SendMessageAsync(MessageDto message);
    Task<List<MessageDto>> GetConversationMessagesAsync(string user1Id, string user2Id);
    string GenerateConversationId(string user1Id, string user2Id);
}