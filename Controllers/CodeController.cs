using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Models.DTO;
using omission.api.Services;

namespace omission.api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        private CodeService _codeService;

        public CodeController(CodeService codeService)
        {
            _codeService = codeService;
        }

       
        [HttpPost]
        [Route("addCode")]
        [AllowAnonymous]
        public IActionResult AddCode([FromBody] CodeDTO codeDTO)
        {
            try
            {
                _codeService.CodeValidation(codeDTO);
                _codeService.AddCode(codeDTO);   
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