using Firebase.Database;
using Firebase.Database.Query;
using heraguard.Application.Chat.Interfaces;
using heraguard.Application.Chat.DTOs;
using Microsoft.Extensions.Configuration;

namespace heraguard.Infrastructure.Repositories;

public class FirebaseChatRepository : IChatRepository
{
    private readonly FirebaseClient _firebaseClient;

    public FirebaseChatRepository(IConfiguration configuration)
    {
        var firebaseUrl = configuration["Firebase:RealtimeDatabaseUrl"];
        _firebaseClient = new FirebaseClient(firebaseUrl);
    }

    public string GenerateConversationId(string user1Id, string user2Id)
    {
        var sortedIds = new[] { user1Id, user2Id }.OrderBy(id => id).ToArray();
        return $"{sortedIds[0]}_{sortedIds[1]}";
    }

    public async Task SendMessageAsync(MessageDto message)
    {
        var conversationId = message.ConversationId;

        await _firebaseClient
            .Child("chats")
            .Child(conversationId)
            .PostAsync(new
            {
                senderId = message.SenderId,
                receiverId = message.ReceiverId,
                message = message.Message,
                timestamp = message.Timestamp,
                type = message.Type
            });
    }

    public async Task<List<MessageDto>> GetConversationMessagesAsync(string user1Id, string user2Id)
    {
        var conversationId = GenerateConversationId(user1Id, user2Id);

        var messages = await _firebaseClient
            .Child("chats")
            .Child(conversationId)
            .OnceAsync<dynamic>();

        return messages
            .Select(msg => new MessageDto
            {
                Id = msg.Key,
                ConversationId = conversationId,
                SenderId = msg.Object.senderId,
                ReceiverId = msg.Object.receiverId,
                Message = msg.Object.message,
                Timestamp = long.Parse(msg.Object.timestamp.ToString()),
                Type = msg.Object.type
            })
            .OrderBy(msg => msg.Timestamp)
            .ToList();
    }
}