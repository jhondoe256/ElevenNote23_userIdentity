using ElevenNote.Models.UserModels;
using ElevenNote.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Accountscontroller : ControllerBase
    {
        private readonly IAuthenticationManager _authManager;

        public Accountscontroller(IAuthenticationManager authManager)
        {
            _authManager = authManager;
        }

        //Route : api/Accounts/Register
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserEntityVM userEntityVM)
        {
            //check for errors
            var errors = await _authManager.Register(userEntityVM);

            if(errors.Any())
            {
                foreach (IdentityError error in errors)
                {
                    ModelState.AddModelError(error.Code,error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("User Registered.");
        }

        //Route : api/Accounts/Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            var authResponse = await _authManager.Login(loginVM);

            if(authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
        }
    }
}