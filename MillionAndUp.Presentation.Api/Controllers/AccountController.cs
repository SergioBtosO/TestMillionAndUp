using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Core.Application.DTOs.Users;
using MillionAndUp.Core.Application.Feautures.Users.Commands.AuthenticarUser;
using MillionAndUp.Core.Application.Feautures.Users.Commands.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseAPIController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticationAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateAddress()
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                Name = request.Name,
                UserName = request.UserName,
                LastName = request.LastName,
                ConfirmPassword = request.ConfirmPassword,
                Origin = GenerateAddress()
            }));
        }

        private string GenerateAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return ip;
        }

    }
}
