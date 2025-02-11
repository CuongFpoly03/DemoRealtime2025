using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtime.Infrastructure.DTOs.Auth;
using Realtime.Infrastructure.Interfaces;

namespace Realtime2025.Controller
{
    [ApiController]
    [Route("api/users")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRepository _repo;

        public AccountController(IMediator mediator, ILogger<AccountController> logger, IUserRepository repo)
        {
            _mediator = mediator;
            _logger = logger;
            _repo = repo;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO req)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Problem(errorMessage);
                }
                var res = await _repo.Register(req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto req)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Problem(errorMessage);
                }
                var res = await _repo.Login(req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout ([FromBody] RefreshTokenDTO req) {
            try
            {
                if (ModelState.IsValid == false)
                {
                    string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Problem(errorMessage);
                }
                var res = await _repo.Logout(req);
                return Ok(res);
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken ([FromBody] RefreshTokenDTO req) {
            try
            {
                if (ModelState.IsValid == false)
                {
                    string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Problem(errorMessage);
                }
                var res = await _repo.RefreshToken(req);
                return Ok(res);
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}