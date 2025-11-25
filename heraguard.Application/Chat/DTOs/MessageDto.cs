namespace heraguard.Application.Chat.DTOs;

public class MessageDto
{
    public string Id { get; set; }
    public string ConversationId { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
    public string Type { get; set; } = "text";
}