using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Services;

namespace omission.api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                _userService.RegisterValidation(registerDTO);
                // Validation 
                _userService.Register(registerDTO);
                
                return Ok();
            }

            catch (AuthenticationException)
            {
                return Forbid();
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }

        }


    }
}