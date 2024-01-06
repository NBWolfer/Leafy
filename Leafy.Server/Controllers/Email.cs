using Leafy.Application.DTOs;
using Leafy.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Email : ControllerBase
    {
        private readonly IEmailService _emailService;

        public Email(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendPassResetMail([FromBody] EmailModel email)
        {
            await _emailService.SendEmailAsync(email.Email, email.Subject, email.HtmlMessage);
            return Ok();
        }
    }
}
