using Microsoft.AspNetCore.Mvc;
using MediatR;
using heraguard.Application.Chat.Commands;
using heraguard.Application.Chat.Queries;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(SendMessageCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Error);
    }

    [HttpGet("conversation/{user1Id}/{user2Id}")]
    public async Task<IActionResult> GetConversation(string user1Id, string user2Id)
    {
        var query = new GetMessagesQuery { User1Id = user1Id, User2Id = user2Id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }
}