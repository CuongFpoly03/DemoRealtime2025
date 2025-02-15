using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtime.Infrastructure.Interfaces;

namespace Realtime2025.Controller
{
    [Route("api/webhook")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        private readonly ILogger<WebHookController> _logger;
        private readonly IMediator _mediator;
        private readonly IWebhookRepository _webhookRepository;
        public WebHookController(ILogger<WebHookController> logger, IMediator mediator, IWebhookRepository webhookRepository)
        {
            _webhookRepository = webhookRepository;
            _logger = logger;
            _mediator = mediator;
        }

    }
}