using heraguard.Application.Users.Queries;
using heraguard.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: BaseController
{
    private readonly IMediator _mediator;
    
    public  UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByLinkingCode(string linkingCode)
    {
        var query = new GetUserByLinkingCodeQuery(linkingCode);
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }
}