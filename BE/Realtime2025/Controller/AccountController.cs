using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Realtime2025.Controller
{
    [ApiController]
    [Route("api/users")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;


        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}