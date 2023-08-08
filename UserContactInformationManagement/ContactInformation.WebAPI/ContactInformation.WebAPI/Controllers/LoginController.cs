using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = Authenticate(userDto);
            if(user != null)
            {
                //var token = Generate(user);
                //return Ok(token);
            }

            return NotFound("User not found / Invalid User Credentials");
        }

        private async Task<string> Generate(User user)
        {
            throw new NotImplementedException();
        }

        private async Task<User> Authenticate (UserDto userDto)
        {
            throw new NotImplementedException();

        }
    }
}
