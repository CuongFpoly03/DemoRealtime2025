using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtime.Application.Commands.TodoCommand;
using Realtime.Domain.Entity;
using Realtime.Share.Helpers;

namespace Realtime2025.Controller
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public TodoController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatedTodoController([FromBody] CreateTodoCommand request)
        {
           try
            {
                var res = await _mediator.Send(request);
                return StatusCode(201, new MethodCommon.ResponseData<Todo>
                {
                    Status = 201,
                    Message = "Create Todo successFull",
                    data = res
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}