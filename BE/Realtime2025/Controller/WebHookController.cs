using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtime.Application.Commands.Webhook;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;
using Realtime.Share.Helpers;

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

        [HttpPost("create/subscription")]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionCommand request)
        {
            try
            {
                var res = await _mediator.Send(request);
                return StatusCode(201, new MethodCommon.ResponseData<WebhookSubscription>
                {
                    Status = 201,
                    Message = "Create Subscription successFull",
                    data = res
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create/event")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateWebhookEventCommand request)
        {
            try
            {
                var res = await _mediator.Send(request);
                return StatusCode(201, new MethodCommon.ResponseData<WebhookEvent>
                {
                    Status = 201,
                    Message = "Create Event successFull",
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