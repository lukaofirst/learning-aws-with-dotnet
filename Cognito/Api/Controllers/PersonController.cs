using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(ICognitoService cognitoService) : ControllerBase
    {
        private readonly ICognitoService _cognitoService = cognitoService;

        [HttpPost]
        public async Task<IActionResult> RegisterUser(Person person)
        {
            try
            {
                var result = await _cognitoService.Register(person);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmRegistration(ConfirmRegistration confirmation)
        {
            try
            {
                var result = await _cognitoService.ConfirmRegistration(confirmation);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var result = await _cognitoService.Login(login);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
