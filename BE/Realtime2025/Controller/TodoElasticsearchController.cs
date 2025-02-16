using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtime.Application.Queries.TodoElasticsearch;
using Realtime.Domain.Entity;
using Realtime.Share.Helpers;

namespace Realtime2025.Controller
{
    [ApiController]
    [Route("api/elasticsearch")]
    public class TodoElasticsearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TodoElasticsearchController> _logger;
        public TodoElasticsearchController(IMediator mediator, ILogger<TodoElasticsearchController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search([FromRoute] string keyword)
        {
            try
            {
                var res = await _mediator.Send(new SearchTodoWithElasticsearchQuery(keyword));
                return StatusCode(200, new MethodCommon.ResponseData<List<Todo>>
                {
                    Status = 200,
                    Message = "Search successFull",
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